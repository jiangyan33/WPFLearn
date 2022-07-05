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
    /// 项目说明，串口通信，主站一直向从站请求数据，一次请求的数据包含3个驱动中的所有数据，一个驱动中有6个监控数值，一个18个数据
    /// 
    /// 
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
