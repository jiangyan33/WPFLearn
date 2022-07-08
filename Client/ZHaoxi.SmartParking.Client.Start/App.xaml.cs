﻿using Prism.Ioc;
using Prism.Unity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Zhaoxi.SmartParking.Client.BLL;
using Zhaoxi.SmartParking.Client.DAL;
using Zhaoxi.SmartParking.Client.IBLL;
using Zhaoxi.SmartParking.Client.IDAL;
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
            containerRegistry.Register<ISysUserBLL, SysUserBLL>();

            containerRegistry.Register<ISysUserDAL, SysUserDAL>();
        }
    }
}