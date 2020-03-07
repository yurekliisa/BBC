using AutoMapper;
using BBC.Core.Domain;
using BBC.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBC.Services.Services.MediaService.Dto
{
    class MediaMapping : Profile, IMapperBase
    {
        public MediaMapping()
        {
            //CreateMap<Kaynak, Hedef>();
            CreateMap<Media, MediaListDto>();
            CreateMap<Media, EditMediaDto>();
            CreateMap<EditMediaDto, Media>();
            CreateMap<CreateMediaDto, Media>();
        }
    }
}
