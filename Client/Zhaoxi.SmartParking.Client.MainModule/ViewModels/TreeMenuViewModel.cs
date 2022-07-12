using Prism.Regions;
using System.Collections.Generic;
using Zhaoxi.SmartParking.Client.Entity;
using Zhaoxi.SmartParking.Client.MainModule.Models;

namespace Zhaoxi.SmartParking.Client.MainModule.ViewModels
{
    public class TreeMenuViewModel
    {
        public List<MenuItemModel> Menus { get; set; } = new List<MenuItemModel>();

        private List<MenuEntity> originMenus;

        private readonly IRegionManager _regionManager;

        public TreeMenuViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;

            originMenus = GlobalEntity.CurrentUserInfo.Menus;

            FillMenus(Menus, 0);
        }

        private void FillMenus(List<MenuItemModel> menus, int parentId)
        {
            var sub = originMenus.FindAll(x => x.ParentId == parentId);

            if (sub.Count > 0)
            {
                foreach (var item in sub)
                {
                    var menuItemModel = new MenuItemModel(_regionManager)
                    {
                        MenuIcon = item.MenuIcon,
                        MenuHeader = item.MenuHeader,
                        TargetView = item.TargetView,
                    };

                    menus.Add(menuItemModel);

                    FillMenus(menuItemModel.Children, item.Id);
                }
            }
        }

    }
}
