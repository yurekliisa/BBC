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
        public ICollection<JobAdvert> JobAdverts { get; set; }
        public IList<LobiUser> LobiUsers { get; set; }
        public ICollection<LobiMessages> LobiMessages { get; set; }
        public ICollection<PrivateMessages> PrivateMessages { get; set; }
        public ICollection<SocialMedia> SocialMedias { get; set; }
        public ICollection<TarifAndRecete> TarifAndRecetes { get; set; }
        public ICollection<Popularity> Popularities { get; set; }

        public User()
        {

        }

        public User(string userName):base(userName)
        {

        }
    }
}
