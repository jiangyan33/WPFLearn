using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ZhaoXi.Controls.Components
{
    public class ComponentBase : UserControl
    {
        private bool _isSelected;

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;

                if (value)
                {
                    var parent = VisualTreeHelper.GetParent(this) as Grid;

                    if (parent != null)
                    {
                        foreach (var item in parent.Children)
                        {
                            if (item is ComponentBase componentBase) componentBase.IsSelected = false;
                        }
                    }

                }


                // 设置控件的状态
                VisualStateManager.GoToState(this, value ? "SelectedState" : "UnselectedState", false);
            }
        }


        /// <summary>
        /// 是否运行
        /// </summary>
        public bool IsRunning
        {
            get { return (bool)GetValue(IsRunningProperty); }
            set { SetValue(IsRunningProperty, value); }
        }

        public static readonly DependencyProperty IsRunningProperty =
            DependencyProperty.Register(nameof(IsRunning), typeof(bool), typeof(ComponentBase), new PropertyMetadata(default(bool), OnPropertyChanged));


        /// <summary>
        /// 是否报警
        /// </summary>
        public bool IsFault
        {
            get { return (bool)GetValue(IsFaultProperty); }
            set { SetValue(IsFaultProperty, value); }
        }

        public static readonly DependencyProperty IsFaultProperty =
            DependencyProperty.Register(nameof(IsFault), typeof(bool), typeof(ComponentBase), new PropertyMetadata(default(bool), OnPropertyChanged));


        private static void OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var obj = d as ComponentBase;

            var state = (bool)e.NewValue;

            if (e.Property == IsRunningProperty)
            {
                VisualStateManager.GoToState(obj, state ? "RunState" : "StopState", false);
            }
            else if (e.Property == IsFaultProperty)
            {
                VisualStateManager.GoToState(obj, state ? "FaultState" : "NormalState", false);
            }
        }


        /// <summary>
        /// 命令
        /// </summary>
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register(nameof(Command), typeof(ICommand), typeof(ComponentBase), new PropertyMetadata(default(ICommand)));



        /// <summary>
        /// 命令参数
        /// </summary>
        public object CommandParamter
        {
            get { return (object)GetValue(CommandParamterProperty); }
            set { SetValue(CommandParamterProperty, value); }
        }

        public static readonly DependencyProperty CommandParamterProperty =
            DependencyProperty.Register(nameof(CommandParamter), typeof(object), typeof(ComponentBase), new PropertyMetadata(default(object)));


        public ComponentBase()
        {
            this.PreviewMouseLeftButtonDown += ComponentBase_PreviewMouseLeftButtonDown;
        }

        private void ComponentBase_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.IsSelected = !this.IsSelected;

            this.Command?.Execute(this.CommandParamter);

            e.Handled = true;
        }

    }
}
