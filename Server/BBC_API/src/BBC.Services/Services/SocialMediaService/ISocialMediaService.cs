using BBC.Services.Services.Common.Base;
using BBC.Services.Services.ContentService.Dto;
using BBC.Services.Services.LobiService.Dto;
using BBC.Services.Services.SocialMediaService.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BBC.Services.Services.SocialMediaService
{
    public interface ISocialMediaService : IBaseService
    {
        Task<List<SocialMediaListDto>> GetAllSocialMedias();
        Task<EditSocialMediaDto> GetSocialMedia(int Id);
        Task CreateSocialMedia(CreateSocialMediaDto input);
        Task EditSocialMedia(EditSocialMediaDto input);
        Task DeleteSocialMedia(int Id);
    }
}
