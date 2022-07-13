using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Zhaoxi.SmartParking.Client.BaseModule.Models
{
    public class UserModel
    {
        public int Index { get; set; }

        public int UserId { get; set; }

        public string UserName { get; set; }

        public string UserIcon { get; set; }

        public string Password { get; set; }

        public int Age { get; set; }

        public string RealName { get; set; }

        public ObservableCollection<RoleModel> RoleList { get; set; } = new ObservableCollection<RoleModel>();

        public ICommand EditCommand { get; set; }

        public ICommand DeleteCommand { get; set; }

        public ICommand RoleCommand { get; set; }

        public ICommand PwdCommand { get; set; }

    }
}
