using BBC.Services.Services.Common.Base;
using BBC.Services.Services.MediaService.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BBC.Services.Services.MediaService
{
    public interface IMediaService : IBaseService
    {
        Task<List<MediaListDto>> GetAllMedia();
        Task<EditMediaDto> GetMedia(int Id);
        Task CreateMedia(CreateMediaDto input);
        Task EditMedia(EditMediaDto input);
        Task DeleteMedia(int Id);

    }
}
