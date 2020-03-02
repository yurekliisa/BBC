using BBC.Core.Configuration.Base;
using BBC.Core.Configuration.Config;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBC.Core.Configuration
{
    public class ConfigEmail : ConfigBase
    {
        public bool DefaultCredentials { get; set; }
        public string To { get; set; }
        public string From { get; set; }
        public string SMTPServer { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }
        public string DisplayName { get; set; }
        public ConfigEmail()
        {
            _configuration.Bind(ConfigurationKeys.Email, this);
        }
    }
}
