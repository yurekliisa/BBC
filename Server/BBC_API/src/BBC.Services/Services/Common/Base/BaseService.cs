using AutoMapper;
using BBC.Core.Dependency;
using BBC.Core.Domain.Entities;
using BBC.Core.Interfaces;
using BBC.Core.IoC;
using BBC.Core.Repositories.Base;
using BBC.Services.Services.Common.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBC.Services.Services.Common.Base
{
    public abstract class BaseService<TDbContext, TEntity, TList, TCreate, TEdit, TView, TKey>
         : IBaseService<TDbContext, TEntity, TList, TCreate, TEdit, TView, TKey>, ITransientDI
         where TDbContext : DbContext, IContext
         where TList : BaseDto<TKey>
         where TCreate : BaseDto
         where TEdit : BaseDto<TKey>
         where TView : BaseDto<TKey>
         where TKey : IEquatable<TKey>
         where TEntity : class, IEntityBase<TKey>, new()
    {
        protected readonly IMapper _mapper;
        protected readonly IRepositoryBase<TDbContext, TEntity, TKey> _entityReposiyory;
        protected BaseService()
        {
            _mapper = IoCManager.GetResolve<IMapper>();
            _entityReposiyory = IoCManager.GetResolve<IRepositoryBase<TDbContext, TEntity, TKey>>();

        }

        public virtual void Create(TCreate input)
        {
            TEntity entity = _mapper.Map<TEntity>(input);
            _entityReposiyory.Insert(entity);
            _entityReposiyory.SaveChanges();
        }

        public virtual void Delete(TKey id)
        {
            _entityReposiyory.Delete(id);
            _entityReposiyory.SaveChanges();
        }

        public virtual void Edit(TEdit input)
        {
            TEntity entity = _mapper.Map<TEntity>(input);
            _entityReposiyory.Update(entity);
            _entityReposiyory.SaveChanges();
        }

        public virtual List<TList> GetAll()
        {
            List<TEntity> query = _entityReposiyory.GetList();
            List<TList> result = _mapper.Map<List<TList>>(query);
            return result;
        }

        public virtual TView View(TKey id)
        {
            TEntity entity = _entityReposiyory.Find(id);
            TView result = _mapper.Map<TView>(entity);
            return result;
        }
    }

    public abstract class BaseService
       : IBaseService, ITransientDI
    {
        protected readonly IMapper _mapper;
        protected BaseService()
        {
            _mapper = IoCManager.GetResolve<IMapper>();
        }
    }
}
