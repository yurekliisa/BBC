using BBC.Services.Identity.Dto.RoleDtos;
using BBC.Services.Services.Common.Base;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BBC.Services.Identity.Interfaces
{
    public interface IRoleService: IBaseService
    {
        Task<List<RoleDto>> GetRoles();
        Task<IdentityResult> CreateRole(CreateRoleDto model);
        Task<IdentityResult> EditRole(int Id, EditRoleDto model);
        Task<IdentityResult> Delete(string Id);
    }
}
