using System;
using System.Windows.Input;

namespace Zhaoxi.CourseManagement.Common
{
    public class CommandBase : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return DoCanExecute?.Invoke(parameter) == true;
        }

        public void Execute(object parameter)
        {
            DoExecute?.Invoke(parameter);
        }

        public Action<object> DoExecute { get; set; }

        public Func<object, bool> DoCanExecute { get; set; }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}
