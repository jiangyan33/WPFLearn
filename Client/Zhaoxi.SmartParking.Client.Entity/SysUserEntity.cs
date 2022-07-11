using System;
using System.Collections.Generic;

namespace Zhaoxi.SmartParking.Client.Entity
{
    public class SysUserEntity
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string UserIcon { get; set; }

        public List<MenuEntity> Menus { get; set; }
    }
}
