using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ZhaoXi.Industrial.Base;

namespace ZhaoXi.Industrial
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            GlobalMonitor.Start(
                () =>
                    {
                        Current.Dispatcher.Invoke(() =>
                        {
                            new MainWindow().Show();
                        });
                    },
                (msg) =>
                {
                    Current.Dispatcher.Invoke(() =>
                    {
                        MessageBox.Show(msg, "系统启动失败");
                        Current.Shutdown();
                    });
                });
        }

        protected override void OnExit(ExitEventArgs e)
        {
            GlobalMonitor.Dispose();
            base.OnExit(e);
        }
    }
}
