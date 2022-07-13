using System.Collections.Generic;
using System.Threading.Tasks;
using Zhaoxi.SmartParking.Client.Entity;
using Zhaoxi.SmartParking.Client.IBLL;
using Zhaoxi.SmartParking.Client.IDAL;

namespace Zhaoxi.SmartParking.Client.BLL
{
    public class SysUserBLL : ISysUserBLL
    {
        private readonly ISysUserDAL _sysUserDAL;

        public SysUserBLL(ISysUserDAL sysUserDAL)
        {
            _sysUserDAL = sysUserDAL;
        }

        public async Task<List<SysUserEntity>> All()
        {
            var result = await _sysUserDAL.All();

            return Newtonsoft.Json.JsonConvert.DeserializeObject<List<SysUserEntity>>(result);
        }

        public async Task<bool> Login(string username, string password)
        {
            var result = await _sysUserDAL.Login(username, password);

            if (string.IsNullOrEmpty(result)) return false;

            GlobalEntity.CurrentUserInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<SysUserEntity>(result);

            return true;
        }

        public async Task<bool> ResetPwd(string userId)
        {
            var result = await _sysUserDAL.ResetPwd(userId);

            return Newtonsoft.Json.JsonConvert.DeserializeObject<bool>(result);
        }
    }
}
