using BBC.Core.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBC.Core.Domain.Identity
{
    public class User : IdentityUser<int>, IEntityBase<int>
    {
        public virtual string Name { get; set; }
        public virtual string SurName { get; set; }
        public virtual string Photo { get; set; }
        public virtual string About { get; set; }
        public virtual DateTime Birthday { get; set; }


        public SocialMedia SocialMedia { get; set; }
        public ICollection<JobAdvert> JobAdverts { get; set; }
        public ICollection<LobiUser> LobiUsers { get; set; }
        public ICollection<LobiMessages> LobiMessages { get; set; }
        public ICollection<PrivateMessages> PrivateMessages { get; set; }
        public ICollection<TarifAndRecete> TarifAndRecetes { get; set; }
        public ICollection<Popularity> Popularities { get; set; }

        public User()
        {
            init();
        }

        public User(string userName) : base(userName)
        {
            init();
        }

        private void init()
        {
            JobAdverts = new HashSet<JobAdvert>();
            LobiUsers = new HashSet<LobiUser>();
            LobiMessages = new HashSet<LobiMessages>();
            PrivateMessages = new HashSet<PrivateMessages>();
            TarifAndRecetes = new HashSet<TarifAndRecete>();
            Popularities = new HashSet<Popularity>();

        }
    }
}
