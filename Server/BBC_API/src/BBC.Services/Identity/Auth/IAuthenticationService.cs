using BBC.Core.Dependency;
using BBC.Services.Identity.Dto;
using BBC.Services.Types.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BBC.Services.Identity.Auth
{
    public interface IAuthenticationService : ITransientDI
    {
        Task<ResponseBase> RefreshToken(RefreshTokenRequest request);
        Task<ResponseBase> Register(UserRegistrationRequest input, string password);
        Task<ResponseBase> Login(string email, string password);
    }
}
