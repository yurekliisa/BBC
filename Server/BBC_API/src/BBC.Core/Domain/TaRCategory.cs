using BBC.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBC.Core.Domain
{
    public class TaRCategory
    {
        public int CategoryId { get; set; }
        public Category Category { get; set; }


        public int TarifAndReceteId { get; set; }
        public TarifAndRecete TarifAndRecete { get; set; }
    }
}
