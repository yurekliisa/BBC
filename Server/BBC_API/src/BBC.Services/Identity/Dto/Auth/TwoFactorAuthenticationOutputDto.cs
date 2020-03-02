using System;
using System.Collections.Generic;
using System.Text;

namespace BBC.Services.Identity.Dto.Auth
{
    public class TwoFactorAuthenticationOutputDto
    {
        public bool HasAuthenticator { get; set; }

        public int RecoveryCodesLeft { get; set; }

        public bool Is2faEnabled { get; set; }
    }
}
