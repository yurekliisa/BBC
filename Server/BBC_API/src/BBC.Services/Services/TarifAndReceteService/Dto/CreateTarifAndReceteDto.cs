using BBC.Services.Services.CategoryService.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBC.Services.Services.TarifAndReceteService.Dto
{
    public class CreateTarifAndReceteDto
    {
        //Categorieleri almak için
        public List<int> CategoryId{ get; set; }

        public CreateTarifAndReceteDto()
        {
            CategoryId = new List<int>();
        }
        
        public ContentDto Content { get; set; }
    }
}
