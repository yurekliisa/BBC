using BBC.Core.Domain;
using BBC.Core.Repositories.Base;
using BBC.Infrastructure.Data;
using BBC.Services.Services.Common.Base;
using BBC.Services.Services.SettingsService.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BBC.Services.Services.SettingsService
{
    public class SettingsManager : BaseService, ISettingsService
    {
        private readonly IRepositoryBase<BBCContext, Settings, int> _settingsRepository;
        public SettingsManager(IRepositoryBase<BBCContext, Settings, int> settingsRepository)
        {
            _settingsRepository = settingsRepository;
        }
        public async Task CreateSettings(CreateSettingsDto input)
        {
            var settings = _mapper.Map<Settings>(input);
            await _settingsRepository.InsertAsync(settings);
        }

        public async Task DeleteSettings(int Id)
        {
            await _settingsRepository.DeleteAsync(Id);
        }

        public async Task EditSettings(EditSettingsDto input)
        {
            var settings = await _settingsRepository.FindAsync(input.Id);
            _mapper.Map(settings, input);
            await _settingsRepository.UpdateAsync(settings);
        }

        public async Task<List<SettingsListDto>> GetAllSettings()
        {
            var settings = await _settingsRepository.GetListAsync();
            var result = _mapper.Map<List<SettingsListDto>>(settings);
            return result;
        }

        public async Task<EditSettingsDto> GetSetting(int Id)
        {
            var settings = await _settingsRepository.GetAsync(Id);
            var result = _mapper.Map<EditSettingsDto>(settings);
            return result;
        }
    }
}
