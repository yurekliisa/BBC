using BBC.Services.Services.CategoryService.Dto;
using BBC.Services.Services.Common.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBC.Services.Services.TarifAndReceteService.Dto
{
    public class UserTarifAndReceteDto : BaseDto<int>
    {
        public ICollection<CategoryListDto> Categories { get; set; }
        public ContentDto Content { get; set; }
        public UserTarifAndReceteDto()
        {
            Categories = new HashSet<CategoryListDto>();
        }
    }
}
