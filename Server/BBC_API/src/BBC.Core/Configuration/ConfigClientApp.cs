using BBC.Core.Configuration.Base;
using BBC.Core.Configuration.Config;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBC.Core.Configuration
{
   
    public class ConfigClientApp : ConfigBase
    {
        public string Url { get; set; }
        public string EmailConfirmationPath { get; set; }
        public string ResetPasswordPath { get; set; }
        public ConfigClientApp()
        {
            _configuration.Bind(ConfigurationKeys.ClientApp, this);
        }

    }
}
