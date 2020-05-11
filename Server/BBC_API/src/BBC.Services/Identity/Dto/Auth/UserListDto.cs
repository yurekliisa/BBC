using System;
using System.Collections.Generic;
using System.Text;

namespace BBC.Services.Identity.Dto.Auth
{
    public class UserListDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public virtual string Name { get; set; }
        public virtual string SurName { get; set; }
        public virtual DateTime Birthday { get; set; }
    }
}
