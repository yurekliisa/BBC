using AutoMapper;
using BBC.Core.Domain;
using BBC.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBC.Services.Services.LobiService.Dto
{
    public class SocialMediaMapping : Profile, IMapperBase
    {
        public SocialMediaMapping()
        {
            CreateMap<Lobi, SocialMediaListDto>();
            CreateMap<Lobi, EditSocialMediaDto>();
            CreateMap<EditSocialMediaDto, Lobi>();
            CreateMap<CreateSocialMediaDto, Lobi>();
        }
    }
}
