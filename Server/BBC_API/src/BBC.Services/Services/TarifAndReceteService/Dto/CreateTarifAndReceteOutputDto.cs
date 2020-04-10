using BBC.Services.Services.CategoryService.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBC.Services.Services.TarifAndReceteService.Dto
{
    public class CreateTarifAndReceteOutputDto
    {
        public List<CategoryListDto> Categories{ get; set; }
        public CreateTarifAndReceteOutputDto()
        {
            Categories = new List<CategoryListDto>();
        }
    }
}
