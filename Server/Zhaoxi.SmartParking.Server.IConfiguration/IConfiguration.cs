using System;

namespace Zhaoxi.SmartParking.Server.IConfiguration
{
    public interface IConfiguration
    {
        string Read(string key);
    }
}
