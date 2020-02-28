using BBC.Core.Dependency;
using BBC.Core.Domain.Entities;
using BBC.Core.Interfaces;
using BBC.Services.Services.Common.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBC.Services.Services.Common.Base
{
    public interface IBaseService<TDbContext, TEntity, TList, TCreate, TEdit, TView, TKey> : ITransientDI
        where TDbContext : DbContext, IContext
        where TList : BaseDto<TKey>
        where TCreate : BaseDto
        where TEdit : BaseDto<TKey>
        where TView : BaseDto<TKey>
        where TKey : IEquatable<TKey>
        where TEntity : class, IEntityBase<TKey>, new()
    {
        List<TList> GetAll();
        void Create(TCreate input);
        void Edit(TEdit input);
        TView View(TKey id);
        void Delete(TKey id);

    }


    public interface IBaseService : ITransientDI
    {
    }
}
