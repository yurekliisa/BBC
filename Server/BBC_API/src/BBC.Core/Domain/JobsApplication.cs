using BBC.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BBC.Core.Domain
{
    public class JobsApplication : EntityBase<int>
    {
        public string Contact { get; set; }


        [ForeignKey("JobAdvertId")]
        public int JobAdvertId { get; set; }
        public JobAdvert JobAdvert { get; set; }

    }
}