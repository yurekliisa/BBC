using BBC.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBC.Core.Domain
{
    public class Category:EntityBase<int>
    {
        public string Name { get; set; }

    }
}
