using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Zhaoxi.SmartParking.Server.IService;
using Zhaoxi.SmartParking.Server.Models;

namespace Zhaoxi.SmartParking.Server.Service
{
    public class SysUserInfoService : ServiceBase, ISysUserInfoService
    {
        public SysUserInfoService(IEFContext.IEFContext eFContext) : base(eFContext)
        {
        }

        public string GetMd5Str(string str)
        {
            if (string.IsNullOrEmpty(str)) return "";

            var result = Encoding.Default.GetBytes(str);

            var md5 = new MD5CryptoServiceProvider();

            var output = md5.ComputeHash(result);

            return BitConverter.ToString(output).Replace("-", "");
        }

        public bool ResetPassword(int userId)
        {
            try
            {
                var userList = Context.Set<SysUserInfo>().Where(x => x.Id == userId).ToList();

                foreach (var user in userList)
                {
                    user.Password = GetMd5Str(GetMd5Str("123456") + "|" + user.UserName);
                }

                Context.SaveChanges();
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
