using System;
using System.Windows;
using ZhaoXi.Industrial.Base;

namespace ZhaoXi.Industrial.ViewModel
{
    public class MainViewModel : NotifyBase
    {

        private UIElement _mainContent;

        public UIElement MainContent
        {
            get { return _mainContent; }
            set { Set<UIElement>(ref _mainContent, value); }
        }

        public CommandBase TabChangedCommand { get; set; }

        public MainViewModel()
        {
            TabChangedCommand = new CommandBase(DoNavChanged);
            DoNavChanged("SystemMonitor");
        }

        private void DoNavChanged(object obj)
        {
            if (obj == null || MainContent?.GetType().Name == obj.ToString()) return;

            var type = Type.GetType("ZhaoXi.Industrial.View." + obj.ToString());

            MainContent = (UIElement)Activator.CreateInstance(type);
        }

    }
}
