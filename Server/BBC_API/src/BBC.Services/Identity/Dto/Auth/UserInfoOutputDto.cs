using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BBC.Services.Identity.Dto.Auth
{
    public class UserInfoOutputDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get;  set; }
        public ICollection<string> Roles { get; set; }
        public UserInfoOutputDto()
        {
            Roles = new HashSet<string>();
        }
    }
}
