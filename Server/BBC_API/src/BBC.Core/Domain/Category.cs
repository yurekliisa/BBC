using BBC.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBC.Core.Domain
{
    public class Category : EntityBase<int>
    {
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<Content> Contents { get; set; }
    }
}
