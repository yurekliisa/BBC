using BBC.Core.Domain.Entities;
using BBC.Core.Interfaces;
using BBC.Services.Services.Common.Base;
using BBC.Services.Services.Common.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBC.Services.Services.Base
{
  
    public interface IApplicationBaseServices<TUser, TRole, TDbContext, TEntity, TList, TCreate, TEdit, TView, TKey> :
      IBaseService<TDbContext, TEntity, TList, TCreate, TEdit, TView, TKey>
      where TDbContext : DbContext, IContext
      where TUser : class
      where TRole : class
      where TEntity : class, IEntityBase<TKey>, new()
      where TList : BaseDto<TKey>
      where TCreate : BaseDto
      where TEdit : BaseDto<TKey>
      where TView : BaseDto<TKey>
      where TKey : IEquatable<TKey>
    {

    }


    public interface IApplicationBaseServices<TUser, TRole> : IBaseService
        where TUser : class
        where TRole : class
    {

    }
}
