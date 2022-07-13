﻿using Prism.Commands;
using Prism.Regions;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Threading;
using Zhaoxi.SmartParking.Client.BaseModule.Models;
using Zhaoxi.SmartParking.Client.Common;
using Zhaoxi.SmartParking.Client.IBLL;

namespace Zhaoxi.SmartParking.Client.BaseModule.ViewModels
{
    public class UserManagementViewModel : ViewModelNavigationBase
    {
        public ObservableCollection<UserModel> UserModelList { get; set; } = new ObservableCollection<UserModel>();

        private readonly ISysUserBLL _sysUserBLL;

        private readonly Dispatcher _dispatcher;

        public UserManagementViewModel(IRegionManager regionManager, ISysUserBLL sysUserBLL, Dispatcher dispatcher) : base(regionManager)
        {
            PageTitle = "用户信息管理";

            _dispatcher = dispatcher;

            _sysUserBLL = sysUserBLL;
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

        }

        private void DeleteItem(object obj)
        {

        }

        private void SetRoles(object obj)
        {

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
