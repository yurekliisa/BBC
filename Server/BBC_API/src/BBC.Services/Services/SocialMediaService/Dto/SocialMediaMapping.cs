using AutoMapper;
using BBC.Core.Domain;
using BBC.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBC.Services.Services.SocialMediaService.Dto
{
    public class SocialMediaMapping : Profile, IMapperBase
    {
        public SocialMediaMapping()
        {
            CreateMap<SocialMedia, SocialMediaDto>();
            CreateMap<SocialMediaDto, SocialMedia>();
            CreateMap<SocialMedia, EditSocialMediaDto>();
            CreateMap<EditSocialMediaDto, SocialMedia>();
            CreateMap<CreateSocialMediaDto, SocialMedia>();
        }
    }
}
