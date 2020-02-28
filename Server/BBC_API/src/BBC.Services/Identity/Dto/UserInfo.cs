using System;
using System.Collections.Generic;
using System.Text;

namespace BBC.Services.Identity.Dto
{
    public class UserInfo
    {
        public UserInfo()
        {
            Permissions = new List<UserPermission>();
        }
        public string UserName { get; set; }
        public List<UserPermission> Permissions { get; set; }
    }
}
