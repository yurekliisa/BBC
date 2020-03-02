using BBC.Services.Services.Common.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBC.Services.Services.CountryService.Dto
{
    public class EditCountryDto : BaseDto<int>
    {
        public string Name { get; set; }
        public string Code { get; set; }
    }
}
