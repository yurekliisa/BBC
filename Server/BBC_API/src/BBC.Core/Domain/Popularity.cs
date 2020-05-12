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
        public double Puan { get; set; }


       
        public int? TarifAndReceteId { get; set; }
        [ForeignKey("TarifAndReceteId")]
        public TarifAndRecete TarifAndRecete { get; set; }



        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}