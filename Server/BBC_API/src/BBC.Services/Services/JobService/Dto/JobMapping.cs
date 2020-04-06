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
            CreateMap<JobAdvert, JobListDto>();
            CreateMap<JobAdvert, EditJobDto>();
            CreateMap<EditJobDto, JobAdvert>();
            CreateMap<CreateJobDto, JobAdvert>();
        }
    }
}
