using BBC.Core.Configuration.Base;
using BBC.Core.Configuration.Config;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBC.Core.Configuration
{
  
    public class ConfigQRCode : ConfigBase
    {
        public string AppName { get; set; }
        public ConfigQRCode()
        {
            _configuration.Bind(ConfigurationKeys.QRCode, this);
        }

    }
}
