using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace BBC.Services.Identity.Dto.Auth
{
    public class RecoveryCodeOutputDto
    {
        public List<string> RecoveryCodes { get; set; }
        public IdentityResult IdentityResult { get; set; }
        public EnableAuthenticatorInputDto EnableAuthenticatorDto { get; set; }
        public bool isView { get; set; }
    }
}