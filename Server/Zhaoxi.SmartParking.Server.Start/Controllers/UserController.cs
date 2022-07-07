using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Zhaoxi.SmartParking.Server.IService;
using Zhaoxi.SmartParking.Server.Models;

namespace Zhaoxi.SmartParking.Server.Start.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly ISysUserInfoService _sysUserInfoService;

        public UserController(ISysUserInfoService sysUserInfoService)
        {
            _sysUserInfoService = sysUserInfoService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] SysUserInfo sysUserInfo)
        {
            var pwd = GetMd5Str(GetMd5Str(sysUserInfo.Password) + "|" + sysUserInfo.UserName);

            var userList = _sysUserInfoService.Query<SysUserInfo>(x => x.UserName == sysUserInfo.UserName && x.Password == pwd);

            if (userList?.Count() > 0)
            {
                var userInfo = userList.ToList()[0];

                return Ok(userInfo);
            }
            else
            {
                return NoContent();
            }
        }

        private string GetMd5Str(string str)
        {
            if (string.IsNullOrEmpty(str)) return "";

            var result = Encoding.Default.GetBytes(str);

            var md5 = new MD5CryptoServiceProvider();

            var output = md5.ComputeHash(result);

            return BitConverter.ToString(output).Replace("-", "");
        }
    }
}
