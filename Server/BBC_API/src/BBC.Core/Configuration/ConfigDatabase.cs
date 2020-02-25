using BBC.Core.Dependency;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBC.Core.Configuration
{
    public class ConfigDatabase : ConfigBase, ISingletonDI
    {
        public virtual string Default { get; set; }
        public ConfigDatabase()
        {
            _configuration.Bind(ConfigurationKeys.Database, this);
        }

    }
}
