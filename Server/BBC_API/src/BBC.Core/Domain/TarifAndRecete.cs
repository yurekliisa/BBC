using BBC.Core.Domain.Entities;
using BBC.Core.Domain.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BBC.Core.Domain
{
    public class TarifAndRecete : EntityBase<int>
    {
        public DateTime CreationTime { get; set; }
        public DateTime DeletedTime { get; set; }
        public DateTime ModifiedTime { get; set; }
        public bool IsDeleted { get; set; }

        //public int TemplateId { get; set; }

        [ForeignKey("ContentId")]
        public int ContentId { get; set; }
        public Content Content { get; set; }

        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public User User { get; set; }
        
        public IList<TaRCategory> TaRCategories { get; set; }
        public IList<Popularity> Popularities { get; set; }


    }
}

