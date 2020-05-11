using System;
using System.Collections.Generic;
using System.Text;

namespace BBC.Services.Identity.Dto.UserDtos
{
    public class UserProfileDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public virtual string Name { get; set; }
        public virtual string SurName { get; set; }
        public virtual string Photo { get; set; }
        public virtual string About { get; set; }
        public virtual DateTime Birthday { get; set; }
    }
}
