using BBC.Core.Domain.Identity;
using BBC.Services.Identity.Dto.UserDtos;
using BBC.Services.Identity.Interfaces;
using BBC.Services.Services.Base;
using BBC.Services.Services.Common.Base;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBC.Services.Identity
{
    public class UserService : ApplicationBaseServices<User, Role>, IUserService
    {
        public async Task<List<UserDto>> GetUsers()
        {
            IQueryable<User> roles = await Task.Run(() =>
            {
                return _userManager.Users;
            });
            var result = _mapper.Map<List<UserDto>>(roles);
            return result;
        }

        public async Task<UserDto> GetUser(int Id)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(user => user.Id == Id);
            if (user == null)
                return null;

            var result = _mapper.Map<UserDto>(user);
            return result;
        }

        public async Task<IdentityResult> CreateUser(CreateUserDto model)
        {
            User user = _mapper.Map<User>(model);

            Role role = await _roleManager.FindByIdAsync(model.ApplicationRoleId);
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

            IdentityResult result = await _userManager.CreateAsync(user, model.Password);
            return result;
        }

        public async Task<IdentityResult> EditUser(string Id, EditUserDto model)
        {
            User user = await _userManager.FindByIdAsync(Id);
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError[]
                {
                    new IdentityError()
                    {
                        Code="User",
                        Description = "Not Found User"
                    }
                });
            }
            user = _mapper.Map<User>(model);

            IdentityResult result = await _userManager.UpdateAsync(user);

            return result;
        }

        public async Task<IdentityResult> Delete(string Id)
        {
            User user = await _userManager.FindByIdAsync(Id);
            if (user == null)
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
            IdentityResult result = await _userManager.DeleteAsync(user);

            return result;
        }
    }
}
