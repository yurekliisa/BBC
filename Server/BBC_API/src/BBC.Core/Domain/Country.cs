using BBC.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBC.Core.Domain
{
    public class Country : EntityBase<int>
    {
        public string Name { get; set; }
        public string Code { get; set; }
    }
}
