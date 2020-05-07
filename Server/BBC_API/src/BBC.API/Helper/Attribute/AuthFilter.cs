using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BBC.API.Helper.Attribute
{
    public class AuthFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var auth= context.HttpContext.User.Claims.Any(y => y.Type == "uid");
            if (!auth)
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
