using AutoMapper;
using BBC.Core.Domain;
using BBC.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBC.Services.Services.JobService.Dto
{
    public class JobMapping : Profile, IMapperBase
    {
        public JobMapping()
        {
            CreateMap<Job, JobListDto>();
            CreateMap<Job, EditJobDto>();
            CreateMap<EditJobDto, Job>();
            CreateMap<CreateJobDto, Job>();
        }
    }
}
