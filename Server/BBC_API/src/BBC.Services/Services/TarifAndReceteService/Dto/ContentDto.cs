using BBC.Services.Services.Common.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBC.Services.Services.TarifAndReceteService.Dto
{
    public class ContentDto
    {
        public string Title { get; set; }
        public string ContentText { get; set; }

        public ICollection<MediaDto> MediaDtos { get; set; }

        public ContentDto()
        {
            MediaDtos = new HashSet<MediaDto>();
        }

    }
}
