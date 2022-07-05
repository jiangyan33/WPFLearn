using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ZhaoXi.Controls.Components
{
    /// <summary>
    /// Pipeline.xaml 的交互逻辑
    /// </summary>
    public partial class Pipeline : UserControl
    {
        public Pipeline()
        {
            InitializeComponent();
        }


        /// <summary>
        /// 流体方向
        /// </summary>
        public int Direction
        {
            get { return (int)GetValue(DirectionProperty); }
            set { SetValue(DirectionProperty, value); }
        }

        public static readonly DependencyProperty DirectionProperty =
            DependencyProperty.Register(nameof(Direction), typeof(int), typeof(Pipeline), new PropertyMetadata(default(int), new PropertyChangedCallback(OnPropertyChanged)));


        private static void OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var value = int.Parse(e.NewValue.ToString());

            VisualStateManager.GoToState(d as Pipeline, value == 1 ? "WEFlowState" : "EWFlowState", false);
        }


        /// <summary>
        /// 流体颜色
        /// </summary>
        public Brush LiquidColor
        {
            get { return (Brush)GetValue(LiquidColorProperty); }
            set { SetValue(LiquidColorProperty, value); }
        }

        public static readonly DependencyProperty LiquidColorProperty =
            DependencyProperty.Register(nameof(LiquidColor), typeof(Brush), typeof(Pipeline), new PropertyMetadata(Brushes.Orange));


        /// <summary>
        /// 流体圆角
        /// </summary>
        public int CapRadius
        {
            get { return (int)GetValue(CapRadiusProperty); }
            set { SetValue(CapRadiusProperty, value); }
        }

        public static readonly DependencyProperty CapRadiusProperty =
            DependencyProperty.Register(nameof(CapRadius), typeof(int), typeof(Pipeline), new PropertyMetadata(default(int)));

    }
}
