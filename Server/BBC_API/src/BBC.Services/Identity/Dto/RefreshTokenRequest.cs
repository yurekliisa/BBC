using BBC.Services.Types.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBC.Services.Identity.Dto
{
    public class RefreshTokenRequest : ResponseBase
    {
        public string Token { get; set; }

        public string RefreshToken { get; set; }
    }
}
