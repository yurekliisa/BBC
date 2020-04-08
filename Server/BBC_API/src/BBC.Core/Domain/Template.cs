using BBC.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBC.Core.Domain
{
    public class Template : EntityBase<int>
    {
        public bool IsDeleted { get; set; }
        public string TemplateData { get; set; }
        //public TarifAndRecete TarifAndRecete { get; set; }
    }
}
