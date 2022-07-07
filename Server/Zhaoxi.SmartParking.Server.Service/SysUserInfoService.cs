using Microsoft.EntityFrameworkCore;
using System;
using Zhaoxi.SmartParking.Server.IService;
using Zhaoxi.SmartParking.Server.Models;

namespace Zhaoxi.SmartParking.Server.Service
{
    public class SysUserInfoService : ServiceBase, ISysUserInfoService
    {

        public SysUserInfoService(IEFContext.IEFContext eFContext) : base(eFContext)
        {

        }

    }
}
