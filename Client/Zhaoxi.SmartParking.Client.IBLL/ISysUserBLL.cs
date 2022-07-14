using System.Collections.Generic;
using System.Threading.Tasks;
using Zhaoxi.SmartParking.Client.Entity;

namespace Zhaoxi.SmartParking.Client.IBLL
{
    public interface ISysUserBLL
    {
        public Task<bool> Login(string username, string password);

        public Task<List<SysUserEntity>> All();

        public Task<bool> ResetPwd(string userId);

        public Task<bool> Save(SysUserEntity sysUserEntity);

    }
}
