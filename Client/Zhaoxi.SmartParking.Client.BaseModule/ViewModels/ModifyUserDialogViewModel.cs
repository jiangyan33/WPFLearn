using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Windows.Input;
using Zhaoxi.SmartParking.Client.BaseModule.Models;
using Zhaoxi.SmartParking.Client.Entity;
using Zhaoxi.SmartParking.Client.IBLL;

namespace Zhaoxi.SmartParking.Client.BaseModule.ViewModels
{
    public class ModifyUserDialogViewModel : BindableBase, IDialogAware
    {

        private string _title = "用户信息编辑";

        public string Title => _title;

        public event Action<IDialogResult> RequestClose;

        private readonly ISysUserBLL _sysUserBLL;

        public ModifyUserDialogViewModel(ISysUserBLL sysUserBLL)
        {
            _sysUserBLL = sysUserBLL;
        }


        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {

        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            MainModel = parameters.GetValue<UserModel>("model");
        }

        private UserModel _mainModel;

        public UserModel MainModel
        {
            get { return _mainModel; }
            set { SetProperty<UserModel>(ref _mainModel, value); }
        }


        public ICommand ConfirmCommand { get => new DelegateCommand(Confirm); }

        private async void Confirm()
        {
            // 修改保存

            var model = new SysUserEntity();

            model.Id = MainModel.UserId;

            model.Age = MainModel.Age;

            model.RealName = MainModel.RealName;

            model.UserName = MainModel.UserName;

            model.State = 1;

            await _sysUserBLL.Save(model);

            RequestClose?.Invoke(new DialogResult(ButtonResult.OK));
        }

        public ICommand CancelCommand { get => new DelegateCommand(Close); }

        private void Close()
        {
            RequestClose?.Invoke(new DialogResult(ButtonResult.Cancel));
        }
    }
}
