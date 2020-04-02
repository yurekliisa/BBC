using BBC.Core.Domain.Entities;
using BBC.Core.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBC.Core.Domain
{
    public class SocialMedia : EntityBase<int>
    {
        public string FacebookUrl { get; set; }
        public string InstagramUrl { get; set; }
        public string TwitterUrl { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
