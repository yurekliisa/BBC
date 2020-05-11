using BBC.Services.Services.CategoryService.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBC.Services.Services.TarifAndReceteService.Dto
{
    public class TarifAndReceteListDto
    {
        public ICollection<CategoryListDto> Categories { get; set; }
        public ContentDto Content { get; set; }
        public string Title { get; set; }
        public string ContentText { get; set; }

        public TarifAndReceteListDto()
        {
            Categories = new HashSet<CategoryListDto>();
        }
    }
}
