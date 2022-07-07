using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ZHaoxi.SmartParking.Client.Start.ViewModels
{
    public class LoginViewModel : BindableBase
    {

        private string _userName = "admin";

        public string UserName
        {
            get { return _userName; }
            set { SetProperty<string>(ref _userName, value); }
        }

        private string _password;

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

        private void DoLogin(object obj)
        {
            try
            {
                if (string.IsNullOrEmpty(UserName))
                {
                    ErrorMessage = "请输入用户名";

                    return;
                }

                if (string.IsNullOrEmpty(Password)) 
                {
                    ErrorMessage = "请输入密码";

                    return;
                }
            }
            catch (Exception ex)
            {

            }
        }

    }
}
