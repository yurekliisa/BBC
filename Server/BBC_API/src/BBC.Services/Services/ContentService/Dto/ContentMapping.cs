using AutoMapper;
using BBC.Core.Domain;
using BBC.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBC.Services.Services.ContentService.Dto
{
    public class ContentMapping : Profile, IMapperBase
    {
        public ContentMapping()
        {
            CreateMap<Content, ContentListDto>();
            CreateMap<Content, EditContentDto>();
            CreateMap<EditContentDto, Content>();
            CreateMap<CreateContentDto, Content>();
        }
    }
}
