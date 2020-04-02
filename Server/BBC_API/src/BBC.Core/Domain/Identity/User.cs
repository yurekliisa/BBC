using BBC.Core.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBC.Core.Domain.Identity
{
    public class User:IdentityUser<int>,IEntityBase<int>
    {
        public virtual string Name { get; set; }
        public virtual string SurName { get; set; }
        public ICollection<LobiUser> LobiUsers { get; set; }
        public TarifAndRecete TarifAndRecete { get; set; }
        public User()
        {

        }

        public User(string userName):base(userName)
        {

        }
    }
}
