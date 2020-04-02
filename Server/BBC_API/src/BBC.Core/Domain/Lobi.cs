using BBC.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBC.Core.Domain
{
    public class Lobi : EntityBase<int>
    {
        public string Name { get; set; }
        public DateTime CreationTime { get; set; }
        public ICollection<LobiUser> LobiUsers { get; set; }
    }
}
