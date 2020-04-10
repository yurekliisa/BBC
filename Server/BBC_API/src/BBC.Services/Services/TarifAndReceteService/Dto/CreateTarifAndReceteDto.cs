using BBC.Services.Services.CategoryService.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBC.Services.Services.TarifAndReceteService.Dto
{
    public class CreateTarifAndReceteDto
    {
        //Categorieleri almak için
        public ICollection<CategoryListDto> Categories{ get; set; }
        
        public ContentDto Content { get; set; }
    }
}
