using Prism.Ioc;
using Prism.Modularity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zhaoxi.SmartParking.Client.BaseModule.Views;

namespace Zhaoxi.SmartParking.Client.BaseModule
{
    public class BaseModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // 注册弹出框
            containerRegistry.RegisterDialog<ModifyUserDialogView>();

            // 注册导航
            containerRegistry.RegisterForNavigation<UserManagementView>();
        }
    }
}
