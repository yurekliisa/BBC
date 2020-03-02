using BBC.Core.Configuration;
using BBC.Core.Domain.Identity;
using BBC.Infrastructure.Identity.Providers;
using BBC.Services.Identity.Dto.Auth;
using BBC.Services.Identity.Interfaces;
using BBC.Services.Services.Base;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
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

        private const string AuthenticatorUriFormat = "otpauth://totp/{0}:{1}?secret={2}&issuer={0}&digits=6";

        public ManageService(
            UrlEncoder urlEncoder,
            IEmailService emailService,
            ConfigClientApp client,
            ConfigQRCode qr
            )
        {
            _urlEncoder = urlEncoder;
            _emailService = emailService;
            _client = client;
            _qr = qr;
        }

        public async Task<UserInfoOutputDto> UserInfo()
        {
            var user = await GetCurrentUserAsync();
            if (user == null)
                return null;

            var userInfoDto = new UserInfoOutputDto
            {
                Email = user.Email,
                EmailConfirmed = user.EmailConfirmed,
                LockoutEnabled = user.LockoutEnabled,
                Roles = await _userManager.GetRolesAsync(user)
            };

            return userInfoDto;
        }

        public async Task<TwoFactorAuthenticationOutputDto> TwoFactorAuthentication()
        {
            var user = await GetCurrentUserAsync();
            if (user == null)
                return null;

            var result = new TwoFactorAuthenticationOutputDto
            {
                HasAuthenticator = await _userManager.GetAuthenticatorKeyAsync(user) != null,
                Is2faEnabled = user.TwoFactorEnabled,
                RecoveryCodesLeft = await _userManager.CountRecoveryCodesAsync(user)
            };

            return result;
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
