using Prism.Commands;
using Prism.Regions;
using Prism.Services.Dialogs;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using Zhaoxi.SmartParking.Client.BaseModule.Models;
using Zhaoxi.SmartParking.Client.BaseModule.Views;
using Zhaoxi.SmartParking.Client.Common;
using Zhaoxi.SmartParking.Client.IBLL;

namespace Zhaoxi.SmartParking.Client.BaseModule.ViewModels
{
    public class UserManagementViewModel : ViewModelNavigationBase
    {
        public ObservableCollection<UserModel> UserModelList { get; set; } = new ObservableCollection<UserModel>();

        private readonly ISysUserBLL _sysUserBLL;

        private readonly Dispatcher _dispatcher;

        private readonly DialogService _dialogService;

        public UserManagementViewModel(IRegionManager regionManager, ISysUserBLL sysUserBLL, Dispatcher dispatcher, DialogService dialogService) : base(regionManager)
        {
            PageTitle = "用户信息管理";

            _dispatcher = dispatcher;

            _sysUserBLL = sysUserBLL;

            _dialogService = dialogService;
        }

        public override void Refresh()
        {
            UserModelList.Clear();

            Task.Run(async () =>
            {
                var tempList = await _sysUserBLL.All();

                foreach (var item in tempList)
                {
                    var userModel = new UserModel
                    {
                        Age = item.Age,
                        UserName = item.UserName,
                        UserIcon = "pack://application:,,,/Zhaoxi.SmartParking.Client.Assets;component/Images/avatar.png",
                        RealName = item.RealName,
                        UserId = item.Id,
                        Password = item.Password,
                        Index = tempList.IndexOf(item) + 1
                    };

                    var roles = item.Roles.Select(x => new RoleModel { RoleId = x.Id, RoleName = x.RoleName, State = x.State }).ToList();

                    foreach (var role in roles) userModel.RoleList.Add(role);

                    userModel.EditCommand = new DelegateCommand<object>(EditItem);

                    userModel.DeleteCommand = new DelegateCommand<object>(DeleteItem);

                    userModel.RoleCommand = new DelegateCommand<object>(SetRoles);

                    userModel.PwdCommand = new DelegateCommand<object>(ResetPassword);

                    _dispatcher.Invoke(() => UserModelList.Add(userModel));
                }
            });
        }

        public override void Add()
        {
        }

        private void EditItem(object obj)
        {
            var value = obj as UserModel;


            var param = new DialogParameters();

            param.Add("model", new UserModel
            {
                UserId = value.UserId,
                Age = value.Age,
                RealName = value.RealName,
                UserName = value.UserName,
            });

            _dialogService.ShowDialog(nameof(ModifyUserDialogView), param, EditResult);
        }

        private void DeleteItem(object obj)
        {
            if (MessageBox.Show("是否确认删除此用户信息？", "提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                // 执行逻辑删除
                this.Refresh();
            }
        }

        private void SetRoles(object obj)
        {

        }

        private void EditResult(IDialogResult dialogResult)
        {
            if (dialogResult.Result == ButtonResult.OK)
            {
                MessageBox.Show("数据保存成功", "提示");

                Refresh();
            }
        }

        private void ResetPassword(object obj)
        {
            Task.Run(() =>
            {
                _sysUserBLL.ResetPwd(obj.ToString());

                System.Windows.MessageBox.Show("密码已重置", "提示");
            });

        }

    }
}
