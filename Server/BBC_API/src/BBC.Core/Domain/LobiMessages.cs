using BBC.Core.Domain.Entities;
using BBC.Core.Domain.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BBC.Core.Domain
{
    public class LobiMessages : EntityBase<int>
    {
        public string Message { get; set; }
        public DateTime SendTime { get; set; }
        
        [ForeignKey("ToLobiId")]   
        public int ToLobiId { get; set; }
        public Lobi ToLobi { get; set; }

        [ForeignKey("FromUserId")]
        public int FromUserId { get; set; }
        public User FromUser { get; set; }
    }
}