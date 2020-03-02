using BBC.Core.Domain.Entities;
using BBC.Core.Interfaces;
using BBC.Core.IoC;
using BBC.Services.Services.Common.Base;
using BBC.Services.Services.Common.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BBC.Services.Services.Base
{
  
    public abstract class ApplicationBaseServices<TUser, TRole, TDbContext, TEntity, TList, TCreate, TEdit, TView, TKey>
        : BaseService<TDbContext, TEntity, TList, TCreate, TEdit, TView, TKey>,
        IApplicationBaseServices<TUser, TRole, TDbContext, TEntity, TList, TCreate, TEdit, TView, TKey>
    where TDbContext : DbContext, IContext
    where TList : BaseDto<TKey>
    where TCreate : BaseDto
    where TEdit : BaseDto<TKey>
    where TView : BaseDto<TKey>
    where TKey : IEquatable<TKey>
    where TEntity : class, IEntityBase<TKey>, new()
    where TUser : class
    where TRole : class
    {
        protected readonly UserManager<TUser> _userManager;
        protected readonly RoleManager<TRole> _roleManager;
        protected readonly SignInManager<TUser> _signInManager;
        protected readonly HttpContext _httpContext;
        protected ApplicationBaseServices()
        {
            _userManager = IoCManager.GetResolve<UserManager<TUser>>();
            _roleManager = IoCManager.GetResolve<RoleManager<TRole>>();
            _signInManager = IoCManager.GetResolve<SignInManager<TUser>>();
            _httpContext = IoCManager.GetResolve<IHttpContextAccessor>().HttpContext;
        }

    }


    public abstract class ApplicationBaseServices<TUser, TRole> : BaseService, IApplicationBaseServices<TUser, TRole>
       where TUser : class
       where TRole : class
    {
        protected readonly UserManager<TUser> _userManager;
        protected readonly RoleManager<TRole> _roleManager;
        protected readonly HttpContext _httpContext;
        protected ApplicationBaseServices()
        {
            _userManager = IoCManager.GetResolve<UserManager<TUser>>();
            _roleManager = IoCManager.GetResolve<RoleManager<TRole>>();
            _httpContext = IoCManager.GetResolve<IHttpContextAccessor>().HttpContext;
        }


        protected virtual async Task<TUser> GetCurrentUserAsync()
        {
            TUser user = await _userManager.FindByIdAsync(_httpContext.User.FindFirst("uid")?.Value);
            if (user != null)
            {
                return user;
            }

            return null;
        }
    }
}
