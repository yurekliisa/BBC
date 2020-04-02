using BBC.Core.Domain.Entities;
using BBC.Core.Domain.Identity;
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
        public ICollection<User> Users { get; set; }
    }
}
