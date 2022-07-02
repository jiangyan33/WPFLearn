using System;
using Zhaoxi.SmartParking.Server.IConfiguration;

namespace Zhaoxi.SmartParking.Server.Configuration
{
    public class Configuration : IConfiguration.IConfiguration
    {
        private static Microsoft.Extensions.Configuration.IConfiguration _configuration;

        public Configuration(Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Read(string key)
        {
            return _configuration[key];
        }
    }
}
