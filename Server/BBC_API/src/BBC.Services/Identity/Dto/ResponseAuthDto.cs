using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBC.Services.Identity.Dto
{
    public class ResponseAuthDto
    {
        public bool Status { get; set; }
        public ICollection<IdentityError> Errors { get; set; }
        public string Token { get; set; }
        public ResponseAuthDto()
        {
            Token = "";
            Errors = new HashSet<IdentityError>();
        }
    }
}
