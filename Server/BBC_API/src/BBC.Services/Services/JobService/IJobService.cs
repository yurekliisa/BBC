using BBC.Services.Services.Common.Base;
using BBC.Services.Services.JobService.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BBC.Services.Services.JobService
{
    public interface IJobService : IBaseService
    {
        Task<List<JobListDto>> GetAllJobs();
        Task<EditJobDto> GetJob(int Id);
        Task CreateJob(CreateJobDto input);
        Task EditJob(EditJobDto input);
        Task DeleteJob(int Id);
    }
}
