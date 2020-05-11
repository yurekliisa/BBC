using BBC.Services.Services.Common.Dto;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBC.Services.Services.TarifAndReceteService.Dto
{
    public class ContentDto
    {
        public string Title { get; set; }
        public string ContentText { get; set; }
        public string ShortDescription { get; set; }

        public IFormFile MainImage{ get; set; }
    }
}
