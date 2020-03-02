using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BBC.Services.Identity.Dto.UserDtos
{
    public class UserDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string ApplicationRoleId { get; set; }
        public string UserId { get; set; }
        public string RoleId { get; set; }
        public string PhoneNumber { get; set; }
        public bool EmailConfirmed { get; set; }
    }
}
