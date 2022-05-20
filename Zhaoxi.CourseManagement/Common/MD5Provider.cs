using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Zhaoxi.CourseManagement.Common
{
    public class MD5Provider
    {
        public static string GetMD5String(string str)
        {
            var md5 = MD5.Create();
            var pws = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
            var pwd = new StringBuilder();
            foreach (var p in pws)
            {
                // 字符串应该以16进制进行格式化
                pwd.Append(p.ToString("X2"));
            }
            return pwd.ToString();
        }
    }
}
