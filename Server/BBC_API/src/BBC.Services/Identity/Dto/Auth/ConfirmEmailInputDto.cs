using System;
using System.Collections.Generic;
using System.Text;

namespace BBC.Services.Identity.Dto.Auth
{
    public class ConfirmEmailInputDto
    {
        public string UserId { get; set; }
        public string Code { get; set; }
    }
}
