using BBC.Core.Permission;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BBC.API.Helper.Attribute
{
    public class RequiredPermission : TypeFilterAttribute
    {
        public RequiredPermission(string _permission) : base(typeof(ClaimRequirementFilter))
        {
            Arguments = new object[] { new Claim(PermissionBase.Permission, _permission) };
        }
    }
}
