using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zhaoxi.SmartParking.Server.Models;

namespace Zhaoxi.SmartParking.Server.IService
{
    public interface IMenuService
    {
        public List<MenuInfo> GetMenusByUserId(int userId);

        public List<MenuInfo> GetMenusByRoleId(int roleId);
        
        public List<MenuInfo> GetAllMenus();
        
        public void Save(string data);
    }
}
