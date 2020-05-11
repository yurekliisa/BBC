using AutoMapper;
using BBC.Core.Domain;
using BBC.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBC.Services.Services.TarifAndReceteService.Dto
{
    public class TarifAndReceteMapping : Profile, IMapperBase
    {
        public TarifAndReceteMapping()
        {
            CreateMap<ContentDto,Content>();
            CreateMap<CreateTarifAndReceteDto, TarifAndRecete>();
            CreateMap<TarifAndRecete, TarifAndReceteListDto>();
            CreateMap<TarifAndRecete, TarifAndReceteDetailDto>();
            CreateMap<TarifAndRecete, EditTarifAndReceteDto>();
        }
    }
}
