using BBC.Services.Services.CategoryService.Dto;
using BBC.Services.Services.Common.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBC.Services.Services.TarifAndReceteService.Dto
{
    public class EditTarifAndReceteDto : BaseDto<int>
    {
        public ICollection<CategoryListDto> Categories { get; set; }
        public ContentDto Content { get; set; }
        public string Title { get; set; }
        public string ContentText { get; set; }

        public ICollection<MediaDto> MediaDtos { get; set; }

        public EditTarifAndReceteDto()
        {
            MediaDtos = new HashSet<MediaDto>();
        }
    }
}
