using BBC.Services.Services.Common.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBC.Services.Services.CategoryService.Dto
{
    class EditCategoryDto : BaseDto<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
