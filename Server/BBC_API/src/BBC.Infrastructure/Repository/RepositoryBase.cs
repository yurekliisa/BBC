using BBC.Core.Domain.Entities;
using BBC.Core.Domain.Helper;
using BBC.Core.Interfaces;
using BBC.Core.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BBC.Infrastructure.Repository
{
    public class RepositoryBase<TDbContext, TEntity, TKey> : IRepositoryBase<TDbContext, TEntity, TKey>
       where TDbContext : DbContext, IContext
       where TKey : IEquatable<TKey>
       where TEntity : class, IEntityBase<TKey>, new()
    {
        protected TDbContext _dbContext;
        protected DbSet<TEntity> _dbSet;

        public RepositoryBase(TDbContext dbContexT)
        {
            _dbContext = dbContexT ?? throw new ArgumentException("An instance of DbContext is required to use this repository.");
            _dbSet = _dbContext.Set<TEntity>();
        }

        #region Insert
        public virtual void InsertGraph(TEntity entity, bool autoSave = false)
        {
            _dbContext.Set<TEntity>().Add(entity);

            if (autoSave)
            {
                _dbContext.SaveChanges();
            }

        }
        public virtual TEntity Insert(TEntity entity, bool autoSave = false)
        {
            TEntity savedEntity = _dbSet.Add(entity).Entity;

            if (autoSave)
            {
                _dbContext.SaveChanges();
            }

            return savedEntity;
        }
        public virtual async Task<TEntity> InsertAsync(TEntity entity, bool autoSave = false)
        {
            TEntity savedEntity = _dbSet.Add(entity).Entity;

            if (autoSave)
            {
                await _dbContext.SaveChangesAsync();
            }

            return savedEntity;
        }
        #endregion

        #region Update
        public virtual TEntity Update(TEntity entity, bool autoSave = false)
        {
            _dbContext.Attach(entity);

            TEntity updatedEntity = _dbContext.Update(entity).Entity;

            if (autoSave)
            {
                _dbContext.SaveChanges();
            }

            return updatedEntity;
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entity, bool autoSave = false)
        {
            _dbContext.Attach(entity);

            TEntity updatedEntity = _dbContext.Update(entity).Entity;

            if (autoSave)
            {
                await _dbContext.SaveChangesAsync();
            }

            return updatedEntity;
        }
        #endregion

        #region Delete
        public virtual void Delete(TEntity entity, bool autoSave = false)
        {
            _dbSet.Remove(entity);

            if (autoSave)
            {
                _dbContext.SaveChanges();
            }
        }

        public virtual async Task DeleteAsync(TEntity entity, bool autoSave = false)
        {
            _dbSet.Remove(entity);

            if (autoSave)
            {
                await _dbContext.SaveChangesAsync();
            }
        }

        public virtual void Delete(Expression<Func<TEntity, bool>> predicate, bool autoSave = false)
        {
            TEntity entities = _dbSet.Where(predicate).FirstOrDefault();

            if (autoSave)
            {
                _dbContext.SaveChanges();
            }
        }

        public virtual async Task DeleteAsync(Expression<Func<TEntity, bool>> predicate, bool autoSave = false)
        {
            List<TEntity> entities = await GetQueryable()
                .Where(predicate)
                .ToListAsync();

            foreach (TEntity entity in entities)
            {
                _dbSet.Remove(entity);
            }

            if (autoSave)
            {
                await _dbContext.SaveChangesAsync();
            }
        }

        public virtual void Delete(TKey id, bool autoSave = false)
        {
            TEntity entity = Find(id, includeDetails: false);
            if (entity == null)
            {
                return;
            }

            Delete(entity, autoSave);
        }

        public virtual async Task DeleteAsync(TKey id, bool autoSave = false)
        {
            TEntity entity = await FindAsync(id, includeDetails: false);
            if (entity == null)
            {
                return;
            }

            await DeleteAsync(entity, autoSave);
        }
        #endregion

        #region GetList with includeDetails
        public virtual List<TEntity> GetList(bool includeDetails = false)
        {
            return _dbSet.ToList();
        }
        public virtual async Task<List<TEntity>> GetListAsync(bool includeDetails = false)
        {
            return await _dbSet.ToListAsync();
        }

        #endregion

        #region Count
        public virtual long GetCount()
        {
            return _dbSet.LongCount();
        }

        public virtual async Task<long> GetCountAsync()
        {
            return await _dbSet.LongCountAsync();
        }
        #endregion

        #region GetQueryable
        public virtual IQueryable<TEntity> GetQueryable()
        {
            return _dbSet.AsQueryable();
        }
        public virtual IQueryable<TEntity> GetQueryableWithInclude(params Expression<Func<TEntity, object>>[] propertySelectors)
        {
            IQueryable<TEntity> query = GetQueryable();

            if (propertySelectors != null)
            {
                foreach (Expression<Func<TEntity, object>> propertySelector in propertySelectors)
                {
                    query = query.Include(propertySelector);
                }
            }

            return query;
        }
        #endregion

        #region Find
        public virtual IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate)
        {
            IQueryable<TEntity> queryable = _dbSet.Where<TEntity>(predicate);
            return queryable;
        }
        public virtual TEntity Find(TKey id, bool includeDetails = true)
        {
            return _dbSet.Find(id);
        }

        public virtual async Task<TEntity> FindAsync(TKey id, bool includeDetails = true)
        {
            return await _dbSet.FindAsync(new object[] { id });
        }
        #endregion

        #region Get
        public virtual TEntity Get(TKey id, bool includeDetails = true)
        {
            TEntity entity = Find(id, includeDetails);

            if (entity == null)
            {
                //throw new EntityNotFoundException(typeof(TEntity), id);
            }

            return entity;
        }

        public virtual async Task<TEntity> GetAsync(TKey id, bool includeDetails = true)
        {
            TEntity entity = await FindAsync(id, includeDetails);

            if (entity == null)
            {
                //throw new EntityNotFoundException(typeof(TEntity), id);
            }

            return entity;
        }
        #endregion

        #region GetSingle
        public virtual TEntity GetSingle(int id)//TKey id)
        {
            IQueryable<TEntity> entities = GetQueryable();
            TEntity entity = entities.SingleOrDefault(x => x.Id.Equals(id));
            return entity;
        }

        public virtual TEntity GetSingleIncluding(int id, params Expression<Func<TEntity, object>>[] includeProperties)
        {

            IQueryable<TEntity> entities = GetQueryableWithInclude(includeProperties);
            TEntity entity = entities.SingleOrDefault(x => x.Id.Equals(id));
            return entity;
        }
        #endregion

        #region SaveChanges
        public virtual async Task SaveChangesAsync()
        {
            OnBeforeSaving();
            await _dbContext.SaveChangesAsync();
        }
        public virtual void SaveChanges()
        {
            OnBeforeSaving();

            _dbContext.SaveChanges();
        }

        private void OnBeforeSaving()
        {
            var bb = EntitiesHelper.GetEntityBases(_dbSet.GetType());
        }
        #endregion

    }
}
