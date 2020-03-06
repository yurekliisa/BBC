using BBC.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBC.Core.Domain
{
    public class Content : EntityBase<int>
    {
        public string ContentText { get; set; }
        public string Title { get; set; }
    }
}
