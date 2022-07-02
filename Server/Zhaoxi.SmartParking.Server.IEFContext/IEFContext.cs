using System;
using Zhaoxi.SmartParking.Server.EFCore;

namespace Zhaoxi.SmartParking.Server.IEFContext
{
    public interface IEFContext
    {
        EFCoreContext CreateDBContext();
    }
}
 