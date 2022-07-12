using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace Zhaoxi.SmartParking.Client.MainModule
{
    public class MainModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            // 获取实例设置到指定的区域
            var manager = containerProvider.Resolve<IRegionManager>();

            manager.RegisterViewWithRegion("LeftMenuTreeRegion", typeof(Views.TreeMenuView));

            manager.RegisterViewWithRegion("MainHeaderRegion", typeof(Views.MainHeaderView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // 注册实例
            containerRegistry.Register<Views.TreeMenuView>();

            containerRegistry.Register<Views.MainHeaderView>();
        }
    }
}
