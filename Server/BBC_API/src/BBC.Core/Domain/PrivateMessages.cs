using BBC.Core.Domain.Entities;
using BBC.Core.Domain.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BBC.Core.Domain
{
    public class PrivateMessages : EntityBase<int>
    {
        public bool IsRead { get; set; }
        public string Message { get; set; }
        public DateTime SendTime { get; set; }

        
        /// <summary>
        /// To User Id tutulcak sadece ilişkisi kurulmıcak
        /// </summary>
        public int ToUserId { get; set; }


        [ForeignKey("FromUserId")]
        public int FromUserId { get; set; }
        public User FromUser { get; set; }
    }
}