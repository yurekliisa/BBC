using BBC.Services.Services.Common.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBC.Services.Services.TarifAndReceteService.Dto
{
    public class ContentDto
    {
        public string MainImage { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public double Puan { get; set; }
        public int CommentCount { get; set; }
        public string ContentText { get; set; }
    }
}
