using System;
using System.Threading.Tasks;
using System.Windows;
using Zhaoxi.CourseManagement.Common;
using Zhaoxi.CourseManagement.DataAccess;
using Zhaoxi.CourseManagement.Model;

namespace Zhaoxi.CourseManagement.ViewModel
{
    /// <summary>
    /// 
    /// </summary>
    public class LoginViewModel : NotifyBase
    {

        /**
         * ViewModel使用
         *  一个ViewModel对应一个View，如果UI层可以进行修改，Model需要继承NotifyBase,
         *  其中包括Model,如果UI层可以进行修改，Model需要继承NotifyBase,在UI绑定的时候要设置触发事件(UpdateSourceTrigger=PropertyChanged)，在构造方法中实例化
         *  CommandBase UI层触发的事件，在构造方法中进行实例化
         * 
         */

        public LoginModel LoginModel { get; set; } = new LoginModel();

        public CommandBase CloseWindowCommand { get; set; }
        public CommandBase LoginCommand { get; set; }

        private string _errorMessage;

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { _errorMessage = value; DoNotify(); }
        }

        private Visibility _showProgress = Visibility.Collapsed;

        public Visibility ShowProgress
        {
            get { return _showProgress; }
            set
            {
                _showProgress = value; this.DoNotify();
                LoginCommand.RaiseCanExecuteChanged();
            }
        }

        public LoginViewModel()
        {
#if DEBUG
            LoginModel.UserName = "admin";
            LoginModel.Password = "123456";
            LoginModel.ValidationCode = "ETU4";
#endif

            CloseWindowCommand = new CommandBase() { };
            CloseWindowCommand.DoExecute = new Action<object>((o) =>
            {
                (o as Window).Close();
            });

            CloseWindowCommand.DoCanExecute = new Func<object, bool>((o) => { return true; });

            LoginCommand = new CommandBase() { };
            LoginCommand.DoExecute = new Action<object>(DoLogin);
            LoginCommand.DoCanExecute = new Func<object, bool>((o) => { return ShowProgress == Visibility.Collapsed; });
        }

        private void DoLogin(object o)
        {
            ShowProgress = Visibility.Visible;
            ErrorMessage = "";
            if (string.IsNullOrEmpty(LoginModel.UserName))
            {
                ErrorMessage = "请输入用户名！";
                ShowProgress = Visibility.Collapsed;
                return;
            }

            if (string.IsNullOrEmpty(LoginModel.Password))
            {
                ErrorMessage = "请输入密码！";
                ShowProgress = Visibility.Collapsed;
                return;
            }

            if (string.IsNullOrEmpty(LoginModel.ValidationCode))
            {
                ErrorMessage = "请输入验证码！";
                ShowProgress = Visibility.Collapsed;
                return;
            }

            if (LoginModel.ValidationCode.ToLower() != "etu4")
            {
                ErrorMessage = "验证码输入不正确！";
                ShowProgress = Visibility.Collapsed;
                return;
            }
            Task.Run(new Action(() =>
            {
                try
                {
                    DataAccess.DataEntity.UserEntity user = LocalDataAccess.GetInstance().CheckUserInfo(LoginModel.UserName, LoginModel.Password);
                    GlobalValues.UserInfo = user ?? throw new Exception("登录失败！用户名或密码错误！");
                    // 异步更新UI中的内容
                    Application.Current.Dispatcher.Invoke(new Action(() => (o as Window).DialogResult = true));

                }
                catch (Exception ex)
                {
                    ErrorMessage = ex.Message;
                }
                finally
                {
                    Application.Current.Dispatcher.Invoke(new Action(() => ShowProgress = Visibility.Collapsed));
                }
            }));
        }
    }
}
