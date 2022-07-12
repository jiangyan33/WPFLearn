using Prism.Commands;
using System.Windows.Input;

namespace Zhaoxi.SmartParking.Client.BaseModule.ViewModels
{
    public class UserManagementViewModel
    {
        public string PageTitle { get; set; } = "用户信息管理";

        public bool IsCanClose { get; set; } = true;

        public ICommand CloseCommand
        {
            get => new DelegateCommand<string>(Close);
        }

        private void Close(string uri)
        {

        }
    }
}
