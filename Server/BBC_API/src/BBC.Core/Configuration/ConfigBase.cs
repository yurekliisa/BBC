using BBC.Core.Dependency;
using BBC.Core.IoC;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBC.Core.Configuration
{
    public class ConfigBase : ISingletonDI
    {
        protected readonly IConfiguration _configuration;
        public ConfigBase()
        {
            _configuration = IoCManager.GetResolve<IConfiguration>();
        }
        public string GetSection(string key)
        {
            string result = _configuration.GetSection(key).ToString();
            return result;
        }

        public string GetConnectionString(string key)
        {
            return _configuration.GetConnectionString(key);
        }
    }
}
