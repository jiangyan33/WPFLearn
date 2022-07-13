using System;
using System.Threading.Tasks;

namespace Zhaoxi.SmartParking.Client.IDAL
{
    public interface ISysUserDAL
    {
        Task<string> Login(string username, string password);

        Task<string> All();

        public Task<string> ResetPwd(string userId);

    }
}
