using BBC.Core.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;


namespace BBC.Core.Domain.Identity
{
    public class Role:IdentityRole<int>, IEntityBase<int>
    {
        public Role()
        {

        }
        public Role(string roleName):base(roleName)
        {

        }


    }
}
