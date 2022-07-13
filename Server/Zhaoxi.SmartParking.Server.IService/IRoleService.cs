using System.Collections.Generic;
using Zhaoxi.SmartParking.Server.Models;

namespace Zhaoxi.SmartParking.Server.IService
{
    public interface IRoleService
    {
        public List<SysUserInfo> GetRolesByUserIdList(List<int> userIdList);
    }
}
