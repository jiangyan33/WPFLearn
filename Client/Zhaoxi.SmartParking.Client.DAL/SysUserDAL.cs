using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Zhaoxi.SmartParking.Client.IDAL;

namespace Zhaoxi.SmartParking.Client.DAL
{
    public class SysUserDAL : WebDataAccess, ISysUserDAL
    {
        public async Task<string> Login(string username, string password)
        {

            var content = new StringContent(JsonConvert.SerializeObject(new { username, password }), Encoding.UTF8, "application/json");

            return await PostDatas("user/login", content);
        }
    }
}
