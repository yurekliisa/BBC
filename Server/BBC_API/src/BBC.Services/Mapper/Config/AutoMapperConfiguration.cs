using AutoMapper;
using BBC.Core.Interfaces;
using BBC.Core.Kernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BBC.Services.Mapper.Config
{
    public static class AutoMapperConfiguration
    {
        public static IMapper Configure()
        {
            List<Type> profiles = KernelAssembly.GetAssemblies()
                       .SelectMany(a => a.GetTypes())
                       .Where(t => t.GetInterfaces().Contains(typeof(IMapperBase))).ToList();
            var mapperConfig = new MapperConfiguration(config =>
            {
                config.AddMaps(KernelAssembly.GetAssemblies());
                foreach (var profile in profiles)
                {
                    config.AddProfile(profile);
                }
            });
            return mapperConfig.CreateMapper();
        }

    }
}
