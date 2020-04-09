using BBC.Core.Domain.Entities;
using BBC.Core.Domain.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BBC.Core.Domain
{
    public class JobAdvert : EntityBase<int>
    {
        public JobAdvert()
        {
            JobsApplication = new HashSet<JobsApplication>();
        }

        public string Content { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime DeletedTime { get; set; }
        public DateTime ModifiedTime { get; set; }
        public bool IsDeleted { get; set; }
        public string Title { get; set; }


        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public User Users { get; set; }

        public ICollection<JobsApplication> JobsApplication { get; set; }

    }
}