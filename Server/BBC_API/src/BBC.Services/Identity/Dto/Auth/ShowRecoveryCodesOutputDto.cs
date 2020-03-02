using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBC.Services.Identity.Dto.Auth
{
    public class ShowRecoveryCodesOutputDto
    {
        public string[] RecoveryCodes { get; set; }
        public IdentityResult IdentityResult { get; set; }
    }
}
