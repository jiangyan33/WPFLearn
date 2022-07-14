using Prism.Ioc;
using Prism.Modularity;
using Prism.Unity;
using System.Windows;
using System.Windows.Threading;
using Unity;
using Zhaoxi.SmartParking.Client.BaseModule;
using Zhaoxi.SmartParking.Client.BLL;
using Zhaoxi.SmartParking.Client.DAL;
using Zhaoxi.SmartParking.Client.IBLL;
using Zhaoxi.SmartParking.Client.IDAL;
using Zhaoxi.SmartParking.Client.MainModule;
using ZHaoxi.SmartParking.Client.Start.Views;

namespace ZHaoxi.SmartParking.Client.Start
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void InitializeShell(Window shell)
        {
            if (Container.Resolve<LoginView>().ShowDialog() == true)
            {
                base.InitializeShell(shell);
            }
            else
            {
                Application.Current?.Shutdown();
            }
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<Dispatcher>(() => Application.Current.Dispatcher);

            // 注册实例到IOC容器中 Registers a Transient Service
            containerRegistry.Register<ISysUserBLL, SysUserBLL>();

            containerRegistry.Register<ISysUserDAL, SysUserDAL>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            // 实例化模块，比如一个用户模块，包括用户、角色、菜单的维护等信息
            moduleCatalog.AddModule<MainModule>();

            moduleCatalog.AddModule<BaseModule>();

            base.ConfigureModuleCatalog(moduleCatalog);
        }
    }
}
