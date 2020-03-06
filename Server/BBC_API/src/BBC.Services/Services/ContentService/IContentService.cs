using BBC.Services.Services.Common.Base;
using BBC.Services.Services.ContentService.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BBC.Services.Services.ContentService
{
    public interface IContentService : IBaseService
    {
        Task<List<ContentListDto>> GetAllContents();
        Task<EditContentDto> GetContent(int Id);
        Task CreateContent(CreateContentDto input);
        Task EditContent(EditContentDto input);
        Task DeleteContent(int Id);
    }
}
