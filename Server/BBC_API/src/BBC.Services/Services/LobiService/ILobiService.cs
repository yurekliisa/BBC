using BBC.Services.Services.Common.Base;
using BBC.Services.Services.ContentService.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BBC.Services.Services.LobiService
{
    public interface ISocialMediaService : IBaseService
    {
        Task<List<LobiListDto>> GetAllLobis();
        Task<EditLobiDto> GetLobi(int Id);
        Task CreateLobi(CreateLobiDto input);
        Task EditLobi(EditLobiDto input);
        Task DeleteLobi(int Id);
    }
}
