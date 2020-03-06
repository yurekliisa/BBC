using BBC.Services.Services.Common.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBC.Services.Services.ContentService.Dto
{
    public class EditContentDto : BaseDto<int>
    {
        public string ContentText { get; set; }
        public string Title { get; set; }
    }
}
