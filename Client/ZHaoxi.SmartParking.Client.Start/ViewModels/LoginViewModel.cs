using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Zhaoxi.SmartParking.Client.IBLL;

namespace ZHaoxi.SmartParking.Client.Start.ViewModels
{
    public class LoginViewModel : BindableBase
    {
        private readonly ISysUserBLL _sysUserBLL;

        public LoginViewModel(ISysUserBLL sysUserBLL)
        {
            _sysUserBLL = sysUserBLL;
        }

        private string _userName = "admin";

        public string UserName
        {
            get { return _userName; }
            set { SetProperty<string>(ref _userName, value); }
        }

        private string _password="123456";

        public string Password
        {
            get { return _password; }
            set { SetProperty<string>(ref _password, value); }
        }

        private string _errorMessage;

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { SetProperty<string>(ref _errorMessage, value); }
        }


        public ICommand LoginCommand
        {
            get => new DelegateCommand<object>(DoLogin);
        }

        private async void DoLogin(object obj)
        {
            try
            {
                if (string.IsNullOrEmpty(UserName))
                {
                    throw new Exception("请输入用户名");
                }

                if (string.IsNullOrEmpty(Password))
                {
                    throw new Exception("请输入密码");
                }
                var result = await _sysUserBLL.Login(this.UserName, this.Password);

                if (!result)
                {
                    throw new Exception("用户名或者密码不正确");
                }
                (obj as Window).DialogResult = true;
            }
            catch (Exception ex)
            {
                ErrorMessage = $"登录失败,{ex.Message}";
            }
        }

    }
}
