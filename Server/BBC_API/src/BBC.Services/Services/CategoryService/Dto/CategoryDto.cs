using BBC.Services.Services.Common.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBC.Services.Services.CategoryService.Dto
{
    public class CategoryDto:BaseDto<int>
    {
        public string Name { get; set; }
    }
}
