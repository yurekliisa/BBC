using AutoMapper;
using BBC.Core.Domain;
using BBC.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBC.Services.Services.CountryService.Dto
{
    public class CountryMapping : Profile, IMapperBase
    {
        public CountryMapping()
        {
            CreateMap<Country, CountryListDto>();
            CreateMap<Country, EditCountryDto>();
            CreateMap<EditCountryDto, Country>();
            CreateMap<CreateCountryDto, Country>();

        }
    }
}
