using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Windows.Input;
using Zhaoxi.SmartParking.Client.BaseModule.Models;

namespace Zhaoxi.SmartParking.Client.BaseModule.ViewModels
{
    public class ModifyUserDialogViewModel : BindableBase, IDialogAware
    {

        private string _title = "用户信息编辑";

        public string Title => _title;

        public event Action<IDialogResult> RequestClose;

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

        private void Confirm()
        {
            RequestClose?.Invoke(new DialogResult(ButtonResult.OK));
        }

        public ICommand CancelCommand { get => new DelegateCommand(Close); }

        private void Close()
        {
            RequestClose?.Invoke(new DialogResult(ButtonResult.Cancel));
        }



    }
}
