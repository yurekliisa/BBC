using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BBC.Services.Identity.Dto.UserDtos
{
    public class EditUserDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PhoneNumber { get; set; }
        public bool LockoutEnabled { get; set; }
        public bool TwoFactorEnabled { get; set; }
    }
}
