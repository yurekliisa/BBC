using AutoMapper;
using BBC.Core.Domain.Identity;
using BBC.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBC.Services.Identity.Dto.RoleDtos
{
    public class RoleMapping : Profile, IMapperBase
    {
        public RoleMapping()
        {
            CreateMap<Role, RoleDto>();
            CreateMap<CreateRoleDto, Role>();
            CreateMap<Role, EditRoleDto>();

        }
    }
}
