using BBC.Services.Services.Common.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBC.Services.Services.LobiService.Dto
{
    public class EditSocialMediaDto : BaseDto<int>
    {
        public string Name { get; set; }
    }
}
