using BBC.Core.Domain.Entities;
using BBC.Core.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBC.Core.Domain
{
    public class PrivateMessages : EntityBase<int>
    {
        public int FromUserId { get; set; }
        public bool IsRead { get; set; }
        public string Message { get; set; }
        public DateTime SendTime { get; set; }
        public int ToUserId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}