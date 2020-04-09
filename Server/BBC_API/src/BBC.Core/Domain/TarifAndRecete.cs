using BBC.Core.Domain.Entities;
using BBC.Core.Domain.Entities.FullAudited;
using BBC.Core.Domain.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BBC.Core.Domain
{
    public class TarifAndRecete : FullAuditedEntity<int>
    {
        public Content Content { get; set; }


        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public User User { get; set; }

        public ICollection<TaRCategory> TaRCategories { get; set; }
        public ICollection<Popularity> Popularities { get; set; }


        public TarifAndRecete()
        {
            TaRCategories = new HashSet<TaRCategory>();
            Popularities = new HashSet<Popularity>();
        }
    }
}

