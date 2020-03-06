using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BBC.Services.Identity.Dto.Auth
{
    public class TokenInputDto
    {
        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = "yurekliisa@admin.com";
        public string RefreshToken { get; set; }
    }
}
