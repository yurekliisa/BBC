using BBC.Core.Domain.Entities;
using BBC.Core.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBC.Core.Domain
{
    public class LobiUser 
    {
        public int LobiId { get; set; }
        public Lobi Lobi { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}