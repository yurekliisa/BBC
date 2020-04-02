using BBC.Core.Domain.Entities;
using BBC.Core.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBC.Core.Domain
{
    public class JobUser : EntityBase<int>
    {
        public int JobId { get; set; }
        public Job Job { get; set; }

        public User User { get; set; }
        public int UserId { get; set; }
    }
}
