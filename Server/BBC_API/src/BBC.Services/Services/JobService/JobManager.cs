using BBC.Core.Domain;
using BBC.Core.Repositories.Base;
using BBC.Infrastructure.Data;
using BBC.Services.Services.Common.Base;
using BBC.Services.Services.ContentService.Dto;
using BBC.Services.Services.JobService.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BBC.Services.Services.JobService
{
    public class JobManager : BaseService, IJobService
    {
        private readonly IRepositoryBase<BBCContext, JobAdvert, int> _jobRepository;

        public JobManager(IRepositoryBase<BBCContext, JobAdvert, int> jobRepository)
        {
            _jobRepository = jobRepository;
        }
        public async Task CreateJob(CreateJobDto input)
        {
            var job = _mapper.Map<JobAdvert>(input);
            await _jobRepository.InsertAsync(job);
        }

        public async Task DeleteJob(int Id)
        {
            await _jobRepository.DeleteAsync(Id);
        }

        public async Task EditJob(EditJobDto input)
        {
            var job = await _jobRepository.FindAsync(input.Id);
            _mapper.Map(job, input);
            await _jobRepository.UpdateAsync(job);
        }

        public async Task<List<JobListDto>> GetAllJobs()
        {
            var jobs = await _jobRepository.GetListAsync();
            var result = _mapper.Map<List<JobListDto>>(jobs);
            return result;
        }

        public async Task<EditJobDto> GetJob(int Id)
        {
            var job = await _jobRepository.GetAsync(Id);
            var result = _mapper.Map<EditJobDto>(job);
            return result;
        }
    }
}
