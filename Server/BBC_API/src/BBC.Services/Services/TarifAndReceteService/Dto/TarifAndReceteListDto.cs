using BBC.Services.Services.CategoryService.Dto;
using BBC.Services.Services.Common.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBC.Services.Services.TarifAndReceteService.Dto
{
    public class TarifAndReceteListDto : BaseDto<int>
    {
        public string MainImage { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string UserFullName { get; set; }
        public string UserPhoto { get; set; }
        public int UserId { get; set; }
        public double Puan { get; set; }
        public int CommentCount { get; set; }
        public List<string> Categories { get; set; }
        public TarifAndReceteListDto()
        {
            Categories = new List<string>();
        }
    }
}
