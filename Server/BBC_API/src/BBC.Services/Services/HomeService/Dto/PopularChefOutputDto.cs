using BBC.Services.Services.Common.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBC.Services.Services.HomeService.Dto
{
    public class PopularChefOutputDto:BaseDto<int>
    {
        public string FullName { get; set; }
        public string Photo { get; set; }
    }
}
