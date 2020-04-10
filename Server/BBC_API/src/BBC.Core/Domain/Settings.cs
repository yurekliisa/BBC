using BBC.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBC.Core.Domain
{
    public class Settings : EntityBase<int>
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
