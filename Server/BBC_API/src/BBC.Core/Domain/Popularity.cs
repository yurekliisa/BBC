using BBC.Core.Domain.Entities;
using BBC.Core.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBC.Core.Domain
{
    public class Popularity : EntityBase<int>
    {
        public string Comment { get; set; }
        public DateTime CreationTime { get; set; }
        public bool IsDeleted { get; set; }
        public string Puan { get; set; }
        public int TarifAndReceteId { get; set; }
        public int ToRId { get; set; }
        public int UserId { get; set; }
        public ICollection<TarifAndRecete> TarifAndRecetes { get; set; }
    }
}
