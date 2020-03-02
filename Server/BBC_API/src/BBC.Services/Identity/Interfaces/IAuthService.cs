using BBC.Core.Domain.Identity;
using BBC.Services.Identity.Dto.Auth;
using BBC.Services.Identity.Dto.UserDtos;
using BBC.Services.Services.Base;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BBC.Services.Identity.Interfaces
{
    public interface IAuthService : IApplicationBaseServices<User, Role>
    {
        Task<IdentityResult> ConfirmEmail(ConfirmEmailInputDto model);
        Task<IdentityResult> Register(RegisterInputDto model);
        Task<TokenOutputDto> CreateToken(LoginInputDto model);
        Task<TokenOutputDto> LoginWith2fa(LoginWith2faInputDto model);
        Task<IdentityResult> ForgotPassword(ForgotPasswordInputDto model);
        Task<IdentityResult> ResetPassword(ResetPasswordInputDto model);
        Task<IdentityResult> ResendVerificationEmail(UserDto model);
    }
}
