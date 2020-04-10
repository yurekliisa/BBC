using AutoMapper;
using BBC.Core.Domain;
using BBC.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBC.Services.Services.SettingsService.Dto
{
    public class SettingsMapping : Profile, IMapperBase
    {
        public SettingsMapping()
        {
            CreateMap<Settings, SettingsListDto>();
            CreateMap<Settings, EditSettingsDto>();
            CreateMap<EditSettingsDto, Settings>();
            CreateMap<CreateSettingsDto, Settings>();
        }
    }
}
