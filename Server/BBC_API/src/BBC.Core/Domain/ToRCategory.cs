using BBC.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBC.Core.Domain
{
    public class ToRCategory : EntityBase<int>
    {
        //ManyToMany için gerekli ara class
        public int CategoryId { get; set; }
        public int TarifAndReceteId { get; set; }
        public int ToRId { get; set; }
    }
}
