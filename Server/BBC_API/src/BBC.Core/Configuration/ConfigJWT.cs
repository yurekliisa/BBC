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
        public virtual string Secret { get; set; }
        public virtual string Issuer { get; set; }
        public virtual string Audience { get; set; }
        public virtual string TokenLifetime { get; set; }
        public ConfigJWT()
        {
            _configuration.Bind(ConfigurationKeys.Jwt, this);
        }

    }
}
