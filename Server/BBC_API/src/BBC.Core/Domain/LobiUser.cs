using BBC.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBC.Core.Domain
{
    public class LobiUser : EntityBase<int>
    {
        public int LobiId { get; set; }
        public int UserId { get; set; }
    }
}
