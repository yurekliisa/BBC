using BBC.Services.Services.Common.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBC.Services.Services.SettingsService.Dto
{
    public class EditSettingsDto : BaseDto<int>
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
