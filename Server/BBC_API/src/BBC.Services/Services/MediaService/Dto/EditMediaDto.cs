using BBC.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBC.Services.Services.MediaService.Dto
{
    public class EditMediaDto : EntityBase<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
