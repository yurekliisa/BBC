using BBC.Services.Services.Common.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBC.Services.Services.HomeService.Dto
{
    public class PopularCategory:BaseDto<int>
    {
        public string Name { get; set; }
    }
}
