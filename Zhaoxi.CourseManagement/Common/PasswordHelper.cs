using System.Windows;
using System.Windows.Controls;

namespace Zhaoxi.CourseManagement.Common
{
    /// <summary>
    /// PasswordBox附加类
    /// </summary>
    public class PasswordHelper
    {

        private static bool _isUpdate = false;

        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.RegisterAttached("Password", typeof(string), typeof(PasswordHelper),
                new FrameworkPropertyMetadata("", new PropertyChangedCallback(OnPropertyChanged)));

        public static readonly DependencyProperty AttachProperty =
           DependencyProperty.RegisterAttached("Attach", typeof(bool), typeof(PasswordHelper),
               new FrameworkPropertyMetadata(default(bool), new PropertyChangedCallback(OnAttached)));

        public static string GetPassword(DependencyObject dp)
        {
            return dp.GetValue(PasswordProperty).ToString();
        }

        public static void SetPassword(DependencyObject dp, string value)
        {
            dp.SetValue(PasswordProperty, value);
        }

        public static bool GetAttach(DependencyObject dp)
        {
            return (bool)dp.GetValue(AttachProperty);
        }

        public static void SetAttach(DependencyObject dp, bool value)
        {
            dp.SetValue(AttachProperty, value);
        }

        /// <summary>
        /// 当依赖属性发生改变时更新UI上的数据
        /// </summary>
        /// <param name="dp"></param>
        /// <param name="e"></param>
        private static void OnPropertyChanged(DependencyObject dp, DependencyPropertyChangedEventArgs e)
        {
            // 当依赖属性值改变时，同时更新UI上的数据
            if (dp is PasswordBox passwordBox)
            {
                passwordBox.PasswordChanged -= PasswordBox_PasswordChanged;
                if (!_isUpdate)
                    passwordBox.Password = e.NewValue?.ToString();
                passwordBox.PasswordChanged += PasswordBox_PasswordChanged;
            }
        }

        /// <summary>
        /// 当UI上的属性变更时修改依赖属性的值
        /// </summary>
        /// <param name="dp"></param>
        /// <param name="e"></param>
        private static void OnAttached(DependencyObject dp, DependencyPropertyChangedEventArgs e)
        {
            if (dp is PasswordBox passwordBox)
            {
                passwordBox.PasswordChanged += PasswordBox_PasswordChanged;
            }
        }

        private static void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var passwordBox = sender as PasswordBox;
            _isUpdate = true;
            SetPassword(passwordBox, passwordBox.Password);
            _isUpdate = false;
        }
    }
}
