using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Zhaoxi.SmartParking.Server.IService;
using Zhaoxi.SmartParking.Server.Models;

namespace Zhaoxi.SmartParking.Server.Start.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly ISysUserInfoService _sysUserInfoService;

        private readonly IMenuService _menuService;

        private readonly IRoleService _roleService;

        public UserController(ISysUserInfoService sysUserInfoService, IMenuService menuService, IRoleService roleService)
        {
            _sysUserInfoService = sysUserInfoService;

            _menuService = menuService;

            _roleService = roleService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] SysUserInfo sysUserInfo)
        {
            var pwd = _sysUserInfoService.GetMd5Str(_sysUserInfoService.GetMd5Str(sysUserInfo.Password) + "|" + sysUserInfo.UserName);

            var userList = _sysUserInfoService.Query<SysUserInfo>(x => x.UserName == sysUserInfo.UserName && x.Password == pwd && x.State == 1);

            if (userList?.Count() > 0)
            {
                var userInfo = userList.ToList()[0];

                var menus = _menuService.GetMenusByUserId(userInfo.Id);

                userInfo.Menus = menus;

                return Ok(userInfo);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpPost("all")]
        public JsonResult All()
        {
            var list = _sysUserInfoService.Query<SysUserInfo>(x => x.State == 1).ToList();

            var userList = _roleService.GetRolesByUserIdList(list.Select(x => x.Id).ToList());

            foreach (var item in list)
            {
                var index = userList.FindIndex(x => x.Id == item.Id);
                if (index == -1) continue;

                item.Roles = userList[index].Roles;
            }

            return new JsonResult(list);
        }

        [HttpPost("resetpwd")]
        public IActionResult ResetPassword([FromBody] int userId)
        {
            var result = _sysUserInfoService.ResetPassword(userId);

            return Ok(result);
        }
    }
}
