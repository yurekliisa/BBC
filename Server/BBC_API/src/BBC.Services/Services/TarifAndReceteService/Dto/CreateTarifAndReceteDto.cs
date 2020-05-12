using BBC.Services.Services.CategoryService.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBC.Services.Services.TarifAndReceteService.Dto
{

    public class CreateTarifAndReceteDto
    {
        //Categorieleri almak için
        public List<int> Categories{ get; set; }

        public CreateTarifAndReceteDto()
        {
            Categories = new List<int>();
        }
        
        public CreateContentDto Content { get; set; }
    }
}
