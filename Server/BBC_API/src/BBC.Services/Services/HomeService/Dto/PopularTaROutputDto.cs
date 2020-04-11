using BBC.Services.Services.Common.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBC.Services.Services.HomeService.Dto
{
    public class PopularTaROutputDto:BaseDto<int>
    {
        public string MainImage { get; set; }
        public string Title { get; set; }
    }
}
