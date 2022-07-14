using System;
using System.Collections.Generic;
using Zhaoxi.SmartParking.Server.Models;

namespace Zhaoxi.SmartParking.Server.IService
{
    public interface ISysUserInfoService : IServiceBase
    {
        public string GetMd5Str(string str);

        public bool ResetPassword(int userId);

        public bool Save(SysUserInfo sysUserInfo);

    }
}
