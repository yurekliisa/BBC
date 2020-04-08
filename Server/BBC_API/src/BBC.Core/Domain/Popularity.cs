using BBC.Core.Domain.Entities;
using BBC.Core.Domain.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BBC.Core.Domain
{
    public class Popularity : EntityBase<int>
    {
        public string Comment { get; set; }
        public DateTime CreationTime { get; set; }
        public bool IsDeleted { get; set; }
        public string Puan { get; set; }


        [ForeignKey("TaRId")]
        public int TaRId { get; set; }
        public TarifAndRecete TarifAndRecete { get; set; }

        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public User User { get; set; }
    }
}