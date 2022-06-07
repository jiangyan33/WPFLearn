using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace ZhaoXi.Controls
{
    /// <summary>
    /// CircularProgressBar.xaml 的交互逻辑
    /// </summary>
    public partial class CircularProgressBar : UserControl
    {
        public CircularProgressBar()
        {
            InitializeComponent();
            this.SizeChanged += CircularProgressBar_SizeChanged;
        }

        private void CircularProgressBar_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateValue();
        }


        /// <summary>
        /// 
        /// </summary>
        public Brush ForeColor
        {
            get { return (Brush)GetValue(ForeColorProperty); }
            set { SetValue(ForeColorProperty, value); }
        }

        public static readonly DependencyProperty ForeColorProperty =
            DependencyProperty.Register(nameof(ForeColor), typeof(Brush), typeof(CircularProgressBar), new PropertyMetadata(Brushes.Orange));


        /// <summary>
        /// 
        /// </summary>
        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(nameof(Value), typeof(double), typeof(CircularProgressBar), new PropertyMetadata(0.0, new PropertyChangedCallback(OnPropertyChanged)));

        public static void OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as CircularProgressBar).UpdateValue();
        }

        private void UpdateValue()
        {
            var minRender = Math.Min(RenderSize.Width, RenderSize.Height);

            layout.Width = minRender;

            var radius = minRender / 2;

            if (radius <= 0) return;

            var newValue = Value % 100.0;

            // 360度分为100份，因为是从x轴正方向开始，需要减去90度  弧度转换为长度，不知道抽空学习下 todo:
            double newX = radius + (radius - 3) * Math.Cos((newValue * 3.6 - 90) * Math.PI / 180);

            double newY = radius + (radius - 3) * Math.Sin((newValue * 3.6 - 90) * Math.PI / 180);


            // 参考官方 https://docs.microsoft.com/zh-cn/dotnet/desktop/wpf/graphics-multimedia/path-markup-syntax?view=netframeworkdesktop-4.8
            // 参考csdn https://blog.csdn.net/youyomei/article/details/104688798

            // M{0} 3A{1} {1} 0 {4} 1 {2} {3}
            // M{0} 3 几何图形起点坐标
            // 3A{1} {1} 圆弧的x轴和y轴的半径
            // 0 {4} 1  【0表示椭圆的旋转，0 不旋转】  【{4}表示设置圆弧的大小，如果圆弧的角度应为180或者更大，设置为1，否则为0】 【1 正方向绘制为1，否则为0】     
            //{2} {3} 结束点坐标
            var pathDataStr = "M{0} 3A{1} {1} 0 {4} 1 {2} {3}";

            // 4 优势弧、劣势弧
            pathDataStr = string.Format(pathDataStr,
                radius + 0.01,
                radius - 3,
                newX,
                newY,
                newValue < 50 && newValue > 0 ? 0 : 1);

            var converter = TypeDescriptor.GetConverter(typeof(Geometry));

            path.Data = (Geometry)converter.ConvertFrom(pathDataStr);

        }

    }
}
