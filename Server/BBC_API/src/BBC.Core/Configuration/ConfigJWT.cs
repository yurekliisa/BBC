using BBC.Core.Configuration.Base;
using BBC.Core.Configuration.Config;
using BBC.Core.Dependency;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBC.Core.Configuration
{
    public class ConfigJWT : ConfigBase
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public double DurationInMinutes { get; set; }
        public ConfigJWT()
        {
            _configuration.Bind(ConfigurationKeys.Jwt, this);
        }

    }
}
