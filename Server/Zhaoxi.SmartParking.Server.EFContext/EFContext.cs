using System;
using Zhaoxi.SmartParking.Server.EFCore;
using Zhaoxi.SmartParking.Server.IConfiguration;

namespace Zhaoxi.SmartParking.Server.EFContext
{
    public class EFContext : IEFContext.IEFContext
    {

        private readonly IConfiguration.IConfiguration _configuration;

        public EFContext(IConfiguration.IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public EFCoreContext CreateDBContext()
        {
            var str = _configuration.Read("DBContextStr");

            return new EFCoreContext(str);
        }
    }
}
