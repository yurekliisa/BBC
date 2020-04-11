using BBC.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBC.Services.Services.SettingsService.Dto
{
    public class CreateSettingsDto : EntityBase<int>
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
