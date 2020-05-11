using BBC.Core.Domain.Identity;
using BBC.Services.Identity.Dto.Auth;
using BBC.Services.Services.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BBC.Services.Identity.Interfaces
{
    public interface IManageService : IApplicationBaseServices<User, Role>
    {
        Task<UserInfoOutputDto> UserInfo();
        Task<EnableAuthenticatorInputDto> EnableAuthenticator();
        Task<IdentityResult> ChangePassword(ChangePasswordInputDto model);
        Task<IdentityResult> SendVerificationEmail();
        Task<IdentityResult> SetPassword(SetPasswordInputDto model);
        Task<RecoveryCodeOutputDto> EnableAuthenticator(EnableAuthenticatorInputDto model, bool isValid);
        Task<IdentityResult> ResetAuthenticator();
        Task<ShowRecoveryCodesOutputDto> GenerateRecoveryCodes();
        Task<TokenOutputDto> RefreshToken(TokenInputDto model);
        Task UserProfilePhoto(IFormFile file);
    }
}
