using BBC.Services.Services.Common.Base;
using BBC.Services.Services.ContentService.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BBC.Services.Services.ContentService
{
    public interface ILobiService : IBaseService
    {
        Task<List<LobiListDto>> GetAllContents();
        Task<EditLobiDto> GetContent(int Id);
        Task CreateContent(CreateLobiDto input);
        Task EditContent(EditLobiDto input);
        Task DeleteContent(int Id);
    }
}
