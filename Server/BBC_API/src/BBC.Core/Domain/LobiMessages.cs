using BBC.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBC.Core.Domain
{
    public class LobiMessages : EntityBase<int>
    {
        public int FromUserId { get; set; }
        public string Message { get; set; }
        public DateTime SendTime { get; set; }
        public int ToLobiId { get; set; }
    }
}
