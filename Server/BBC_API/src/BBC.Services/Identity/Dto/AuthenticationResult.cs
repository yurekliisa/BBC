using BBC.Services.Types.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBC.Services.Identity.Dto
{
    public class AuthenticationResult : ResponseBase
    {

        public AuthenticationResult()
        {
            UserInfo = new UserInfo();
        }

        public UserInfo UserInfo { get; set; }

        public DateTime ExpiryDate { get; set; }

        public string Token { get; set; }

        public string RefreshToken { get; set; }



    }
}
