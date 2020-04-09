using BBC.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BBC.Core.Domain
{
    public class Content : EntityBase<int>
    {
        public string ContentText { get; set; }
        public string Title { get; set; }


        [ForeignKey("TarifandReceteId")]
        public int TarifandReceteId { get; set; }
        public TarifAndRecete TarifAndRecete { get; set; }


        public ICollection<Media> Medias { get; set; }

        public Content()
        {
            Medias = new HashSet<Media>();
        }

    }
}