using BBC.Core.Configuration;
using BBC.Core.Domain.Identity;
using BBC.Core.Permission;
using BBC.Infrastructure.Identity.Providers;
using BBC.Services.Helper;
using BBC.Services.Identity.Dto.Auth;
using BBC.Services.Identity.Dto.UserDtos;
using BBC.Services.Identity.Interfaces;
using BBC.Services.Services.Base;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BBC.Services.Identity
{
    public class AuthService : ApplicationBaseServices<User, Role>, IAuthService
    {
        private readonly IEmailService _emailService;
        private readonly ConfigClientApp _client;
        private readonly ConfigJWT _jwt;
        private readonly SignInManager<User> _signInManager;
        private readonly IHostingEnvironment _environment;
        public AuthService(
            SignInManager<User> signInManager,
            IEmailService emailService,
            ConfigClientApp client,
            ConfigJWT jwt,
            IHostingEnvironment environment
            )
        {
            _environment = environment;
            _signInManager = signInManager;
            _emailService = emailService;
            _client = client;
            _jwt = jwt;
        }

        public async Task<IdentityResult> ConfirmEmail(ConfirmEmailInputDto model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
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
            var result = await _userManager.ConfirmEmailAsync(user, model.Code);
            return result;
        }

        public async Task<IdentityResult> Register(RegisterInputDto model)
        {

            var user = new User { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                result = await _userManager.AddToRoleAsync(user, "UserTest");
                if (result.Succeeded)
                {
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = $"{_client.Url}{_client.EmailConfirmationPath}?uid={user.Id}&code={System.Net.WebUtility.UrlEncode(code)}";
                    if (!_environment.IsDevelopment())
                    {
                        await _emailService.SendEmailConfirmationAsync(model.Email, callbackUrl);
                    }
                }
                return result;

            }
            return result;
        }

        public async Task<TokenOutputDto> CreateToken(LoginInputDto model)
        {
            var tokenModel = new TokenOutputDto()
            {
                HasVerifiedEmail = false
            };
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                tokenModel.Errors = new string[] { "Not Found User" };
                return tokenModel;
            }


            if (!_environment.IsDevelopment())
            {
                if (!user.EmailConfirmed)
                {
                    tokenModel.Errors = new string[] { "No Email Confirmed" };
                    return tokenModel;
                }

                if (user.LockoutEnabled)
                {
                    tokenModel.Errors = new string[] { "This account has been locked." };
                    return tokenModel;
                }
            }


            if (await _userManager.CheckPasswordAsync(user, model.Password))
            {
                tokenModel.HasVerifiedEmail = true;

                if (user.TwoFactorEnabled)
                {
                    tokenModel.TFAEnabled = true;
                    return tokenModel;
                }
                else
                {
                    JwtSecurityToken jwtSecurityToken = await CreateJwtToken(user);
                    tokenModel.TFAEnabled = false;
                    tokenModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

                    tokenModel.RefreshToken = await _userManager.GetAuthenticationTokenAsync(user, "local", "RefreshToken");
                    if (String.IsNullOrEmpty(tokenModel.RefreshToken))
                    {
                        tokenModel.RefreshToken = Guid.NewGuid().ToString();
                        await _userManager.SetAuthenticationTokenAsync(user, "local", "RefreshToken", tokenModel.RefreshToken);
                    }
                    return tokenModel;
                }
            }
            tokenModel.Errors = new string[] { "Invalid login attempt." };
            return tokenModel;
        }

        public async Task<TokenOutputDto> LoginWith2fa(LoginWith2faInputDto model)
        {
            var tokenModel = new TokenOutputDto();

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                tokenModel.Errors = new string[] { "Not Found User" };
                return tokenModel;
            }
            if (await _userManager.VerifyTwoFactorTokenAsync(user, "Authenticator", model.TwoFactorCode))
            {
                JwtSecurityToken jwtSecurityToken = await CreateJwtToken(user);

                tokenModel = new TokenOutputDto()
                {
                    HasVerifiedEmail = true,
                    TFAEnabled = false,
                    Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken)
                };

                string refreshToken = await _userManager.GetAuthenticationTokenAsync(user, "local", "RefreshToken");
                if (String.IsNullOrEmpty(refreshToken))
                {
                    refreshToken = Guid.NewGuid().ToString();
                    await _userManager.SetAuthenticationTokenAsync(user, "local", "RefreshToken", refreshToken);
                }
                await _userManager.AddLoginAsync(user, new UserLoginInfo("local", "Web", user.UserName));

                await _signInManager.CreateUserPrincipalAsync(user);


                return tokenModel;
            }
            tokenModel.Errors = new string[] { "Unable to verify Authenticator Code!" };

            return tokenModel;
        }

        public async Task<IdentityResult> ForgotPassword(ForgotPasswordInputDto model)
        {

            var user = await _userManager.FindByEmailAsync(model.Email);
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
            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            var callbackUrl = $"{_client.Url}{_client.ResetPasswordPath}?uid={user.Id}&code={System.Net.WebUtility.UrlEncode(code)}";

            await _emailService.SendPasswordResetAsync(model.Email, callbackUrl);

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> ResetPassword(ResetPasswordInputDto model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
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
            var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);

            return result;
        }

        public async Task<IdentityResult> ResendVerificationEmail(UserDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
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
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = $"{_client.Url}{_client.EmailConfirmationPath}?uid={user.Id}&code={System.Net.WebUtility.UrlEncode(code)}";
            await _emailService.SendEmailConfirmationAsync(user.Email, callbackUrl);
            return IdentityResult.Success;
        }

        private async Task<JwtSecurityToken> CreateJwtToken(User user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var roleClaims = new List<Claim>();

            for (int i = 0; i < roles.Count; i++)
            {
                roleClaims.Add(new Claim("roles", roles[i]));
            }
            var permissions = await GetUserPermissions(user);
            string ipAddress = IpHelper.GetIpAddress();

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id.ToString()),
                new Claim("ip", ipAddress),

            }
            .Union(permissions)
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwt.DurationInMinutes),
                signingCredentials: signingCredentials);
            return jwtSecurityToken;
        }
        // TODO : Get roles token olustururkende getiriliyo ikisini burda tek obje donus sekilde yazilcak
        public virtual async Task<List<Claim>> GetUserPermissions(User user)
        {
            List<Claim> userPermissions = new List<Claim>();
            IList<string> userRole = await _userManager.GetRolesAsync(user);
            foreach (string role in userRole)
            {
                Role getRole = await _roleManager.FindByNameAsync(role);
                IList<Claim> roleClaims = await _roleManager.GetClaimsAsync(getRole);
                userPermissions = userPermissions.Union(roleClaims).ToList();
            }

            return userPermissions;
        }
    }
}
