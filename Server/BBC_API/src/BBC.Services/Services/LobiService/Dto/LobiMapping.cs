using AutoMapper;
using BBC.Core.Domain;
using BBC.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBC.Services.Services.LobiService.Dto
{
    public class LobiMapping : Profile, IMapperBase
    {
        public LobiMapping()
        {
            CreateMap<Lobi, LobiListDto>();
            CreateMap<Lobi, EditLobiDto>();
            CreateMap<EditLobiDto, Lobi>();
            CreateMap<CreateLobiDto, Lobi>();
        }
    }
}
