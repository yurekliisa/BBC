using AutoMapper;
using BBC.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBC.Services.Identity.Dto
{
    public class IdentityMapping : Profile, IMapperBase
    {
        public IdentityMapping()
        {
        }
    }
}
