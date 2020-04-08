using BBC.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BBC.Core.Domain
{
    public class Media : EntityBase<int>
    {
        public string MediaUrl { get; set; }

        [ForeignKey("ContentId")]  //ContentId nin foreignKey olduğunu belirttik.
        public int ContentId { get; set; }
        public Content Content { get; set; }
    }
}