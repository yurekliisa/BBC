using BBC.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBC.Core.Domain
{
    public class Media : EntityBase<int>
    {
        public int ContentId { get; set; }
        public string MediaUrl { get; set; }
    }
}
