using System;
using System.Collections.Generic;
using System.Text;

namespace BBC.Services.Services.JobService.Dto
{
    public class CreateJobDto
    {
        public string Content { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime DeletedTime { get; set; }
        public DateTime ModifiedTime { get; set; }
        public bool IsDeleted { get; set; }
        public string Title { get; set; }
    }
}
