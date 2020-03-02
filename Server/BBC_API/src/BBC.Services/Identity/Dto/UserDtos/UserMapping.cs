using AutoMapper;
using BBC.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBC.Services.Identity.Dto.UserDtos
{
    public class UserMapping : Profile, IMapperBase
    {
        public UserMapping()
        {
        }
    }
}
