using System.Collections.Generic;
using System.Linq;
using Zhaoxi.SmartParking.Server.IService;
using Zhaoxi.SmartParking.Server.Models;

namespace Zhaoxi.SmartParking.Server.Service
{
    public class RoleService : ServiceBase, IRoleService
    {

        public RoleService(IEFContext.IEFContext eFContext) : base(eFContext)
        {
        }

        public List<SysUserInfo> GetRolesByUserIdList(List<int> userIdList)
        {
            if (userIdList == null || userIdList.Count == 0) return new List<SysUserInfo>();

            var tempList = (from user_role in Context.Set<UserRole>()
                            join role in Context.Set<RoleInfo>()
                            on user_role.RoleId equals role.Id
                            where userIdList.Contains(user_role.UserId) && role.State == 1
                            select new { UserId = user_role.UserId, RoleInfo = role }).ToList();

            if (tempList.Count == 0) return new List<SysUserInfo>();

            return tempList.GroupBy(x => x.UserId).Select(x =>
             {
                 var roles = x.Select(it => it.RoleInfo).ToList();

                 return new SysUserInfo { Id = x.Key, Roles = roles };
             }).ToList();
        }
    }
}
