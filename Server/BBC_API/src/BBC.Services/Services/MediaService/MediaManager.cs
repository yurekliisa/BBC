using BBC.Core.Domain;
using BBC.Core.Repositories.Base;
using BBC.Infrastructure.Data;
using BBC.Services.Services.Common.Base;
using BBC.Services.Services.MediaService.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BBC.Services.Services.MediaService
{
    public class MediaManager : BaseService, IMediaService 
    {
        //Inject
        private readonly IRepositoryBase<BBCContext, Media, int> _mediaRepository;
        public MediaManager(IRepositoryBase<BBCContext, Media, int> mediaRepository)
        {
            _mediaRepository = mediaRepository;
        }
        public async Task CreateMedia(CreateMediaDto input)
        {
            var media = _mapper.Map<Media>(input);
            await _mediaRepository.InsertAsync(media);
        }
        public async Task DeleteMedia(int Id)
        {
            await _mediaRepository.DeleteAsync(Id);
        }
        public async Task EditMedia(EditMediaDto input)
        {
            var media = await _mediaRepository.FindAsync(input.Id);
            _mapper.Map(media, input);
            await _mediaRepository.UpdateAsync(media);
        }
        public async Task<List<MediaListDto>> GetAllMedia()
        {
            var medias = await _mediaRepository.GetListAsync();
            var result = _mapper.Map<List<MediaListDto>>(medias);
            return result;
        }
        public async Task<EditMediaDto> GetMedia(int Id)
        {
            var media = await _mediaRepository.GetAsync(Id);
            var result = _mapper.Map<EditMediaDto>(media);
            return result;
        }
    }
}
