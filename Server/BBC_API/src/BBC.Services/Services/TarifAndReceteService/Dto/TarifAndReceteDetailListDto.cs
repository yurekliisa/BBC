using BBC.Services.Services.CategoryService.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBC.Services.Services.TarifAndReceteService.Dto
{
    public class TarifAndReceteDetailListDto
    {
        public ICollection<CategoryListDto> Categories { get; set; }
        public ContentDto Content { get; set; }
        public string Title { get; set; }
        public string ContentText { get; set; }
        public double Puan { get; set; }
        public ICollection<CommentDto> CommentDtos { get; set; }
        public ICollection<MediaDto> MediaDtos { get; set; }

        public TarifAndReceteDetailListDto()
        {
            CommentDtos = new HashSet<CommentDto>();
            MediaDtos = new HashSet<MediaDto>();
        }
    }
}
