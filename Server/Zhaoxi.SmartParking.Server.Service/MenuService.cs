using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zhaoxi.SmartParking.Server.IService;
using Zhaoxi.SmartParking.Server.Models;

namespace Zhaoxi.SmartParking.Server.Service
{
    public class MenuService : ServiceBase, IMenuService
    {
        public MenuService(IEFContext.IEFContext eFContext) : base(eFContext)
        {
        }

        public List<MenuInfo> GetAllMenus()
        {
            return Context.Set<MenuInfo>().AsQueryable().Where(x => x.State == 1).ToList();
        }

        public List<MenuInfo> GetMenusByRoleId(int roleId)
        {
            var roleIdList = (from role in Context.Set<RoleInfo>()
                              where role.Id == roleId && role.State == 1
                              select role.Id).ToList();

            if (roleIdList.Count == 0) return new List<MenuInfo>();

            var query = from menu in Context.Set<MenuInfo>()
                        join role_menu in Context.Set<RoleMenu>()
                        on menu.Id equals role_menu.MenuId
                        where role_menu.RoleId == roleId && menu.State == 1
                        select menu;

            return query.Distinct().ToList();
        }

        public List<MenuInfo> GetMenusByUserId(int userId)
        {
            var roles = (from user_role in Context.Set<UserRole>()
                         join role in Context.Set<RoleInfo>()
                         on user_role.RoleId equals role.Id
                         where user_role.UserId == userId && role.State == 1
                         select role.Id).ToList();

            if (roles.Count == 0) return new List<MenuInfo>();

            var query = from menu in Context.Set<MenuInfo>()
                        join role_menu in Context.Set<RoleMenu>()
                        on menu.Id equals role_menu.MenuId
                        where roles.Contains(role_menu.RoleId) && menu.State == 1
                        select menu;

            return query.Distinct().ToList();
        }

        public void Save(string data)
        {
            var value = JsonConvert.DeserializeObject<MenuInfo>(data);

            if (value.Id == 0)
            {
                var index = Context.Set<MenuInfo>().Max(x => x.Index);

                value.Index = index + 1;
            }

            value.State = 1;

            Context.Entry(value).State = value.Id == 0 ? EntityState.Added : EntityState.Modified;

            Context.SaveChanges();
        }
    }
}
