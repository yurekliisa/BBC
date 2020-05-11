using BBC.Core.Domain;
using BBC.Core.Repositories.Base;
using BBC.Infrastructure.Data;
using BBC.Services.Services.Common.Base;
using BBC.Services.Services.LobiService.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BBC.Services.Services.LobiService
{
    public class LobiManager : BaseService, ILobiService
    {
        private readonly IRepositoryBase<BBCContext, Lobi, int> _lobiRepository;

        public LobiManager(IRepositoryBase<BBCContext, Lobi, int> lobiRepository)
        {
            _lobiRepository = lobiRepository;
        }

        public async Task CreateLobi(CreateLobiDto input)
        {
            var lobi = _mapper.Map<Lobi>(input);
            await _lobiRepository.InsertAsync(lobi);
        }

        public async Task DeleteLobi(int Id)
        {
            await _lobiRepository.DeleteAsync(Id);
        }

        public async Task EditLobi(EditLobiDto input)
        {
            var lobi = await _lobiRepository.FindAsync(input.Id);
            _mapper.Map(lobi, input);
            await _lobiRepository.UpdateAsync(lobi);
        }

        public async Task<List<LobiListDto>> GetAllLobis()
        {
            var lobis = await _lobiRepository.GetListAsync();
            var result = _mapper.Map<List<LobiListDto>>(lobis);
            return result;
        }

        public async Task<EditLobiDto> GetLobi(int Id)
        {
            var lobi = await _lobiRepository.GetAsync(Id);
            var result = _mapper.Map<EditLobiDto>(lobi);
            return result;
        }
    }
}
