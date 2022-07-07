using System.Threading.Tasks;

namespace Zhaoxi.SmartParking.Client.IBLL
{
    public interface ISysUserBLL
    {
        public Task<bool> Login(string username, string password);
    }
}
