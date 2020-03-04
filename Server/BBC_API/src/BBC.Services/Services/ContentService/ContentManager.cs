using BBC.Core.Domain;
using BBC.Core.Repositories.Base;
using BBC.Infrastructure.Data;
using BBC.Services.Services.Common.Base;
using BBC.Services.Services.ContentService.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BBC.Services.Services.ContentService
{
    public class LobiManager : BaseService, ILobiService
    {
        private readonly IRepositoryBase<BBCContext, Content, int> _contentRepository;

        public LobiManager(IRepositoryBase<BBCContext, Content, int> contentRepository)
        {
            _contentRepository = contentRepository;
        }
        public async Task CreateContent(CreateLobiDto input)
        {
            var content = _mapper.Map<Content>(input);
            await _contentRepository.InsertAsync(content);
        }

        public async Task DeleteContent(int Id)
        {
            await _contentRepository.DeleteAsync(Id);
        }

        public async Task EditContent(EditLobiDto input)
        {
            var content = await _contentRepository.FindAsync(input.Id);
            _mapper.Map(content, input);
            await _contentRepository.UpdateAsync(content);
        }

        public async Task<List<LobiListDto>> GetAllContents()
        {
            var contents = await _contentRepository.GetListAsync();
            var result = _mapper.Map<List<LobiListDto>>(contents);
            return result;
        }

        public async Task<EditLobiDto> GetContent(int Id)
        {
            var content = await _contentRepository.GetAsync(Id);
            var result = _mapper.Map<EditLobiDto>(content);
            return result;
        }
    }
}
