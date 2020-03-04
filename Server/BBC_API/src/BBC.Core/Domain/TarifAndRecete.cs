using BBC.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBC.Core.Domain
{
    public class TarifAndRecete : EntityBase<int>
    {
        public int ContentId { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime DeletedTime { get; set; }
        public DateTime ModifiedTime { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsRecete { get; set; }
        public bool IsTarif { get; set; }
        public int TemplateId { get; set; }
        public int UserId { get; set; }
    }
}
