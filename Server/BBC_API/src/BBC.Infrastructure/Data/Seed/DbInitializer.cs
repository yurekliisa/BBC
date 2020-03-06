using BBC.Core.Domain;
using BBC.Core.Domain.Identity;
using BBC.Core.Permission;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BBC.Infrastructure.Data.Seed
{
    public static class DbInitializer
    {
     
        public static async Task Seed(this IApplicationBuilder app)
        {

            using (IServiceScope serviceScope = app.ApplicationServices.CreateScope())
            {
                BBCContext _dbContext = serviceScope.ServiceProvider.GetService<BBCContext>();
                UserManager<User> _userManager = serviceScope.ServiceProvider.GetService<UserManager<User>>();
                RoleManager<Role> _roleManager = serviceScope.ServiceProvider.GetService<RoleManager<Role>>();
                Permissions _permissions = serviceScope.ServiceProvider.GetService<Permissions>();

                _dbContext.Database.Migrate();

                #region Full Permission 

                //--- Role
                Role aRole = await _dbContext.Roles.FirstOrDefaultAsync(x => x.Name == "AdminTest");
                if (aRole == null)
                {

                    Role adminRole = new Role()
                    {
                        Name = "AdminTest",
                        NormalizedName = "AdminTest"
                    };

                    IdentityResult adminRoleCheck = await _roleManager.CreateAsync(adminRole);
                    if (adminRoleCheck.Succeeded)
                    {
                        System.Collections.Generic.List<FieldInfo> permissionList = _permissions.GetType().GetFields().ToList();
                        foreach (FieldInfo perm in permissionList)
                        {
                            if (perm.IsStatic && perm.Name != Permissions.Visitor)
                            {
                                object val = perm.GetValue(perm.Name);

                                await _roleManager.AddClaimAsync(adminRole, new Claim(Permissions.Permission, Convert.ToString(val)));
                            }
                        }
                    }



                }
                //--- Account
                User aUser = await _dbContext.Users.FirstOrDefaultAsync(x => x.UserName == "AdminTest");
                if (aUser == null)
                {
                    User adminAccount = new User()
                    {
                        Email = "yurekliisa@admin.com",
                        UserName = "AdminTest"
                    };
                    IdentityResult userCheck = await _userManager.CreateAsync(adminAccount, "123!QweQwe");

                    if (userCheck.Succeeded)
                    {
                        IdentityResult addRole = await _userManager.AddToRoleAsync(adminAccount, "AdminTest");
                    }
                }
                #endregion

                #region Not Full Permission
                //--- Role
                Role uRole = await _dbContext.Roles.FirstOrDefaultAsync(x => x.Name == "UserTest");
                if (uRole == null)
                {
                    Role userRole = new Role()
                    {
                        Name = "UserTest",
                        NormalizedName = "UserTest"
                    };

                    IdentityResult userRoleCheck = await _roleManager.CreateAsync(userRole);
                    if (userRoleCheck.Succeeded)
                    {
                        var permissionList = _permissions.GetType().GetFields().ToList();
                        FieldInfo userPermission = permissionList.FirstOrDefault(x => x.Name == Permissions.Visitor);
                        if (userPermission != null)
                        {
                            object val = userPermission.GetValue(userPermission.Name);
                            await _roleManager.AddClaimAsync(userRole, new Claim(userPermission.Name, Convert.ToString(val)));
                        }
                    }
                }

                //--- Account
                User uUser = await _dbContext.Users.FirstOrDefaultAsync(x => x.UserName == "UserTest");
                if (uUser == null)
                {
                    User userAccount = new User()
                    {
                        Email = "yurekliisa@user.com",
                        UserName = "UserTest"
                    };
                    IdentityResult userAccountCheck = await _userManager.CreateAsync(userAccount, "123!QweQwe");

                    if (userAccountCheck.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(userAccount, "UserTest");
                    }
                }
                #endregion

                #region Country Default Value
                var trValue = await _dbContext.Countries.FirstOrDefaultAsync(x => x.Name == "Türkiye" && x.Code == "TR");
                if(trValue == null)
                {
                    _dbContext.Countries.Add(new Country()
                    {
                        Code = "TR",
                        Name = "Türkiye"
                    });
                }
                #endregion

                await _dbContext.SaveChangesAsync();
            }
        }

    }
}
