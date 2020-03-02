using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BBC.Core.Domain.Identity;
using BBC.Services.Identity.Dto.RoleDtos;
using BBC.Services.Services.Common.Base;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BBC.Services.Identity.Interfaces;

namespace BBC.Services.Identity
{
    public class RoleService : BaseService, IRoleService
    {

        private readonly RoleManager<Role> _roleManager;

        public RoleService(RoleManager<Role> roleManager)
        {
            this._roleManager = roleManager;
        }
        public async Task<List<RoleDto>> GetRoles()
        {
            IQueryable<Role> roles = await Task.Run(() =>
            {
                return _roleManager.Roles;
            });
            var result = _mapper.Map<List<RoleDto>>(roles);
            return result;

        }

        public async Task<IdentityResult> CreateRole(CreateRoleDto model)
        {
            Role Role = _mapper.Map<Role>(model);
            IdentityResult result = await _roleManager.CreateAsync(Role);
            return result;
        }


        public async Task<IdentityResult> EditRole(int Id, EditRoleDto model)
        {
            Role role = await _roleManager.FindByIdAsync(model.Id.ToString());
            if (role == null)
            {
                return IdentityResult.Failed(new IdentityError[]
                {
                    new IdentityError()
                    {
                        Code="Role",
                        Description = "Not Found Role"
                    }
                });
            }
            role = _mapper.Map<Role>(model);

            IdentityResult result = await _roleManager.UpdateAsync(role);
            return result;
        }

        public async Task<IdentityResult> Delete(string Id)
        {
            Role role = await _roleManager.FindByIdAsync(Id);
            if (role == null)
            {
                return IdentityResult.Failed(new IdentityError[]
                {
                    new IdentityError()
                    {
                        Code="Role",
                        Description = "Not Found Role"
                    }
                });
            }
            IdentityResult result = _roleManager.DeleteAsync(role).Result;
            return result;
        }
    }
}
