using BBC.Core.Domain.Entities;
using BBC.Core.Domain.Identity;
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
        public ICollection<Template> Templates { get; set; }
        public ICollection<User> Users { get; set; }
        public Content Content { get; set; }
        //public int UserId { get; set; }
    }
}
