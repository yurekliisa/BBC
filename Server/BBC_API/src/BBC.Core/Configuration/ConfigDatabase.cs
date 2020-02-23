using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBC.Core.Configuration
{
    public class ConfigDatabase : ConfigBase
    {
        public virtual string Default { get; set; }
        public ConfigDatabase()
        {
            _configuration.Bind(ConfigurationKeys.Database, this);
        }

    }
}
