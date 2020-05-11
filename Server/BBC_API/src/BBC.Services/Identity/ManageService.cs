using BBC.Core.Configuration;
using BBC.Core.Domain.Identity;
using BBC.Infrastructure.Identity.Providers;
using BBC.Services.Helper;
using BBC.Services.Identity.Dto.Auth;
using BBC.Services.Identity.Interfaces;
using BBC.Services.Services.Base;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace BBC.Services.Identity
{
    public class ManageService : ApplicationBaseServices<User, Role>, IManageService
    {
        private readonly UrlEncoder _urlEncoder;
        private readonly IEmailService _emailService;
        private readonly ConfigClientApp _client;
        private readonly ConfigQRCode _qr;
        private readonly ConfigJWT _jwt;

        private const string AuthenticatorUriFormat = "otpauth://totp/{0}:{1}?secret={2}&issuer={0}&digits=6";

        public ManageService(
            UrlEncoder urlEncoder,
            IEmailService emailService,
            ConfigClientApp client,
            ConfigQRCode qr,
            ConfigJWT jwt
            )
        {
            _urlEncoder = urlEncoder;
            _emailService = emailService;
            _client = client;
            _qr = qr;
            _jwt = jwt;
        }

        public async Task<UserInfoOutputDto> UserInfo()
        {
            var user = await GetCurrentUserAsync();
            if (user == null)
                return null;
            var roles = await _userManager.GetRolesAsync(user);
            var userInfoDto = new UserInfoOutputDto
            {
                Email = user.Email,
                Id = user.Id,
                UserName = user.UserName,
                FullName = user.Name + " " + user.SurName,
                Roles = roles
            };

            return userInfoDto;
        }

        public async Task<EnableAuthenticatorInputDto> EnableAuthenticator()
        {
            var user = await GetCurrentUserAsync();
            if (user == null)
                return null;

            var model = new EnableAuthenticatorInputDto();
            await LoadSharedKeyAndQrCodeUriAsync(user, model);

            return model;
        }

        public async Task<IdentityResult> ChangePassword(ChangePasswordInputDto model)
        {
            var user = await GetCurrentUserAsync();
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

            var passwordValidator = new PasswordValidator<User>();
            var result = await passwordValidator.ValidateAsync(_userManager, null, model.NewPassword);

            if (result.Succeeded)
            {
                var changePasswordResult = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                return changePasswordResult;
            }
            else
            {
                return result;
            }
        }

        public async Task<IdentityResult> SendVerificationEmail()
        {
            var user = await GetCurrentUserAsync();
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

        public async Task<IdentityResult> SetPassword(SetPasswordInputDto model)
        {
            var user = await GetCurrentUserAsync();
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

            var addPasswordResult = await _userManager.AddPasswordAsync(user, model.NewPassword);
            return addPasswordResult;
        }

        public async Task<IdentityResult> Disable2fa()
        {
            var user = await GetCurrentUserAsync();
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

            var disable2faResult = await _userManager.SetTwoFactorEnabledAsync(user, false);

            return disable2faResult;
        }

        public async Task<RecoveryCodeOutputDto> EnableAuthenticator(EnableAuthenticatorInputDto model, bool isValid)
        {
            var user = await GetCurrentUserAsync();
            var result = new RecoveryCodeOutputDto();
            if (user == null)
            {
                result.IdentityResult = IdentityResult.Failed(new IdentityError[]
                {
                    new IdentityError()
                    {
                        Code="User",
                        Description = "Not Found User"
                    }
                });
            }

            if (!isValid)
            {
                await LoadSharedKeyAndQrCodeUriAsync(user, model);
                result.IdentityResult = IdentityResult.Success;
                result.EnableAuthenticatorDto = model;
                return result;
            }

            var verificationCode = model.Code.Replace(" ", string.Empty).Replace("-", string.Empty);

            var is2faTokenValid = await _userManager.VerifyTwoFactorTokenAsync(
                user, _userManager.Options.Tokens.AuthenticatorTokenProvider, verificationCode);

            if (!is2faTokenValid)
            {
                result.IdentityResult = IdentityResult.Failed(new IdentityError[]
                {
                    new IdentityError()
                    {
                        Code="Code",
                        Description = "Verification code is invalid."
                    }
                });
                await LoadSharedKeyAndQrCodeUriAsync(user, model);
                result.EnableAuthenticatorDto = model;
                result.IdentityResult = IdentityResult.Success;
                result.isView = true;
                return result;
            }

            await _userManager.SetTwoFactorEnabledAsync(user, true);
            var recoveryCodes = await _userManager.GenerateNewTwoFactorRecoveryCodesAsync(user, 10);
            result.RecoveryCodes = recoveryCodes.ToList();
            return result;
        }

        public async Task<IdentityResult> ResetAuthenticator()
        {
            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return IdentityResult.Failed(
                    new IdentityError[]{
                        new IdentityError()
                        {
                            Code="User",
                            Description = "Not Found User"
                        }
                    }
                );
            }

            var result = await _userManager.SetTwoFactorEnabledAsync(user, false);
            if (!result.Succeeded)
            {
                return result;
            }
            result = await _userManager.ResetAuthenticatorKeyAsync(user);
            return result;
        }

        public async Task<ShowRecoveryCodesOutputDto> GenerateRecoveryCodes()
        {
            var user = await GetCurrentUserAsync();
            var result = new ShowRecoveryCodesOutputDto();
            if (user == null)
            {
                result.IdentityResult = IdentityResult.Failed(
                    new IdentityError[]
                      {
                            new IdentityError()
                            {
                                Code="User",
                                Description = "Not Found User"
                            }
                      }
                    );
                return result;
            }
            if (!user.TwoFactorEnabled)
            {
                result.IdentityResult = IdentityResult.Failed(
                  new IdentityError[] {
                        new IdentityError()
                        {
                            Code="Two Factor",
                            Description =  $"Cannot generate recovery codes for user with ID '{user.Id}' as they do not have 2FA enabled."
                        }
                  }
                );
                return result;
            }
            var recoveryCodes = await _userManager.GenerateNewTwoFactorRecoveryCodesAsync(user, 10);

            result.RecoveryCodes = recoveryCodes.ToArray();
            result.IdentityResult = IdentityResult.Success;

            return result;
        }

        public async Task<TokenOutputDto> RefreshToken(TokenInputDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            var checkRefreshToken = await _userManager.VerifyUserTokenAsync(user, "BBC", "RefreshToken", model.RefreshToken);
            if (checkRefreshToken)
            {
                //set new refresh token
                model.RefreshToken = await _userManager.GenerateUserTokenAsync(user, "BBC", "RefreshToken");
                await _userManager.SetAuthenticationTokenAsync(user, "BBC", "RefreshToken", model.RefreshToken);

                var tokenModel = new TokenOutputDto()
                {
                    UserId=user.Id,
                    HasVerifiedEmail = true,
                    RefreshToken = model.RefreshToken
                };

                JwtSecurityToken jwtSecurityToken = await CreateJwtToken(user);
                tokenModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
                return tokenModel;
            }
            return new TokenOutputDto()
            {
                Errors = new string[]
                {
                    "Not Found User",
                }
            };
        }
        //TODO: Authda ve burda kullanıldı teke dusur
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
        private async Task<List<Claim>> GetUserPermissions(User user)
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


        private string FormatKey(string unformattedKey)
        {
            var result = new StringBuilder();
            int currentPosition = 0;
            while (currentPosition + 4 < unformattedKey.Length)
            {
                result.Append(unformattedKey.Substring(currentPosition, 4)).Append(" ");
                currentPosition += 4;
            }
            if (currentPosition < unformattedKey.Length)
            {
                result.Append(unformattedKey.Substring(currentPosition));
            }

            return result.ToString().ToLowerInvariant();
        }

        private string GenerateQrCodeUri(string email, string unformattedKey)
        {
            return string.Format(
                AuthenticatorUriFormat,
                _urlEncoder.Encode(_qr.AppName),
                _urlEncoder.Encode(email),
                unformattedKey);
        }

        private async Task LoadSharedKeyAndQrCodeUriAsync(User user, EnableAuthenticatorInputDto model)
        {
            var unformattedKey = await _userManager.GetAuthenticatorKeyAsync(user);
            if (string.IsNullOrEmpty(unformattedKey))
            {
                await _userManager.ResetAuthenticatorKeyAsync(user);
                unformattedKey = await _userManager.GetAuthenticatorKeyAsync(user);
            }

            model.SharedKey = FormatKey(unformattedKey);
            model.AuthenticatorUri = GenerateQrCodeUri(user.Email, unformattedKey);
        }
    }
}
