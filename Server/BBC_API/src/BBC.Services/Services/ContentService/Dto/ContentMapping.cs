using AutoMapper;
using BBC.Core.Domain;
using BBC.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBC.Services.Services.ContentService.Dto
{
    public class LobiMapping : Profile, IMapperBase
    {
        public LobiMapping()
        {
            CreateMap<Content, LobiListDto>();
            CreateMap<Content, EditLobiDto>();
            CreateMap<EditLobiDto, Content>();
            CreateMap<CreateLobiDto, Content>();
        }
    }
}
