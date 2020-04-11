using BBC.Core.Domain.Identity;
using BBC.Services.Services.Base;
using BBC.Services.Services.Common.Base;
using BBC.Services.Services.SettingsService.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BBC.Services.Services.SettingsService
{
    public interface ISettingsService : IApplicationBaseServices<User, Role>
    {
        Task<List<SettingsListDto>> GetAllSettings();
        Task<EditSettingsDto> GetSetting(int Id);
        Task CreateSettings(CreateSettingsDto input);
        Task EditSettings(EditSettingsDto input);
        Task DeleteSettings(int Id);
    }
}
