using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Zhaoxi.CourseManagement.Common;
using Zhaoxi.CourseManagement.Model;

namespace Zhaoxi.CourseManagement.ViewModel
{
    public class MainViewModel : NotifyBase
    {

        public UserModel UserInfo { get; set; }

        private string _searchText;

        public string SearchText
        {
            get { return _searchText; }
            set { _searchText = value; DoNotify(); }
        }

        private FrameworkElement _mainContent;

        public FrameworkElement MainContent
        {
            get { return _mainContent; }
            set { _mainContent = value; DoNotify(); }
        }

        public CommandBase NavChangedCommand { get; set; }

        public MainViewModel()
        {
            UserInfo = new UserModel();

            NavChangedCommand = new CommandBase();
            NavChangedCommand.DoExecute = new Action<object>(DoNavChanged);
            NavChangedCommand.DoCanExecute = new Func<object, bool>((o) => true);
            DoNavChanged("FirstPageView");
        }

        private void DoNavChanged(object obj)
        {
            var type = Type.GetType("Zhaoxi.CourseManagement.View." + obj.ToString());
            var cti = type.GetConstructor(Type.EmptyTypes);

            MainContent = (FrameworkElement)cti.Invoke(null);
        }
    }
}
