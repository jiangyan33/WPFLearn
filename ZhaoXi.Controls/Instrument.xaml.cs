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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ZhaoXi.Controls
{
    /// <summary>
    /// Instrument.xaml 的交互逻辑
    /// </summary>
    public partial class Instrument : UserControl
    {

        #region 依赖属性

        /// <summary>
        /// 依赖属性包装器
        /// </summary>
        public Brush PlateBackground
        {
            get { return (Brush)GetValue(PlateBackgroundProperty); }
            set { SetValue(PlateBackgroundProperty, value); }
        }

        /// <summary>
        /// 依赖属性PlateBackground.
        /// </summary>
        public static readonly DependencyProperty PlateBackgroundProperty =
            DependencyProperty.Register(nameof(PlateBackground), typeof(Brush), typeof(Instrument),
              new PropertyMetadata(default(Brush)));

        /// <summary>
        /// 依赖属性包装器
        /// </summary>
        public int Value
        {
            get { return (int)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        /// <summary>
        /// 依赖属性Value.
        /// </summary>
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(nameof(Value), typeof(int), typeof(Instrument),
              new PropertyMetadata(default(int), new PropertyChangedCallback(OnPropertyChanged)));

        /// <summary>
        /// 依赖属性包装器
        /// </summary>
        public int MinNum
        {
            get { return (int)GetValue(MinNumProperty); }
            set { SetValue(MinNumProperty, value); }
        }

        /// <summary>
        /// 依赖属性MinNum.
        /// </summary>
        public static readonly DependencyProperty MinNumProperty =
            DependencyProperty.Register(nameof(MinNum), typeof(int), typeof(Instrument),
              new PropertyMetadata(0, new PropertyChangedCallback(OnPropertyChanged)));


        /// <summary>
        /// 依赖属性包装器
        /// </summary>
        public int MaxNum
        {
            get { return (int)GetValue(MaxNumProperty); }
            set { SetValue(MaxNumProperty, value); }
        }

        /// <summary>
        /// 依赖属性MaxNum.
        /// </summary>
        public static readonly DependencyProperty MaxNumProperty =
            DependencyProperty.Register(nameof(MaxNum), typeof(int), typeof(Instrument),
              new PropertyMetadata(100, new PropertyChangedCallback(OnPropertyChanged)));



        /// <summary>
        /// 依赖属性包装器
        /// </summary>
        public int Interval
        {
            get { return (int)GetValue(IntervalProperty); }
            set { SetValue(IntervalProperty, value); }
        }

        /// <summary>
        /// 依赖属性Interval.
        /// </summary>
        public static readonly DependencyProperty IntervalProperty =
            DependencyProperty.Register(nameof(Interval), typeof(int), typeof(Instrument),
              new PropertyMetadata(10, new PropertyChangedCallback(OnPropertyChanged)));

        /// <summary>
        /// 依赖属性包装器
        /// </summary>
        public int ScaleTextSize
        {
            get { return (int)GetValue(ScaleTextSizeProperty); }
            set { SetValue(ScaleTextSizeProperty, value); }
        }

        /// <summary>
        /// 依赖属性ScaleTextSize.
        /// </summary>
        public static readonly DependencyProperty ScaleTextSizeProperty =
            DependencyProperty.Register(nameof(ScaleTextSize), typeof(int), typeof(Instrument),
              new PropertyMetadata(16, new PropertyChangedCallback(OnPropertyChanged)));


        /// <summary>
        /// 依赖属性包装器
        /// </summary>
        public Brush ScaleBrush
        {
            get { return (Brush)GetValue(ScaleBrushProperty); }
            set { SetValue(ScaleBrushProperty, value); }
        }

        /// <summary>
        /// 依赖属性ScaleBrush.
        /// </summary>
        public static readonly DependencyProperty ScaleBrushProperty =
            DependencyProperty.Register(nameof(ScaleBrush), typeof(Brush), typeof(Instrument),
              new PropertyMetadata(Brushes.White, new PropertyChangedCallback(OnPropertyChanged)));


        #endregion 依赖属性

        public Instrument()
        {
            InitializeComponent();
            this.SizeChanged += Instrument_SizeChanged;
        }

        private void Instrument_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double minSize = Math.Min(this.RenderSize.Width, this.RenderSize.Height);
            this.backEllipse.Width = minSize;
            this.backEllipse.Height = minSize;
        }

        private static void OnPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            (obj as Instrument).Refresh();
        }

        private void Refresh()
        {
            var radius = this.backEllipse.Width / 2;
            if (double.IsNaN(radius)) return;
            this.mainCanvas.Children.Clear();

            double step = 270.0 / (this.MaxNum - this.MinNum);

            // 画小刻度
            for (int i = 0; i < this.MaxNum - this.MinNum; i++)
            {
                var angle = (i * step - 45) * Math.PI / 180;

                var lineScale = new Line { Stroke = this.ScaleBrush, StrokeThickness = 1 };
                lineScale.X1 = radius - (radius - 13) * Math.Cos(angle);
                lineScale.Y1 = radius - (radius - 13) * Math.Sin(angle);
                lineScale.X2 = radius - (radius - 8) * Math.Cos(angle);
                lineScale.Y2 = radius - (radius - 8) * Math.Sin(angle);
                mainCanvas.Children.Add(lineScale);
            }

            step = 270.0 / this.Interval;
            var scaleText = this.MinNum;
            // 画大刻度
            for (var i = 0; i <= this.Interval; i++)
            {
                var angle = (i * step - 45) * Math.PI / 180;

                var lineScale = new Line { Stroke = this.ScaleBrush, StrokeThickness = 1 };
                lineScale.X1 = radius - (radius - 20) * Math.Cos(angle);
                lineScale.Y1 = radius - (radius - 20) * Math.Sin(angle);
                lineScale.X2 = radius - (radius - 8) * Math.Cos(angle);
                lineScale.Y2 = radius - (radius - 8) * Math.Sin(angle);
                mainCanvas.Children.Add(lineScale);

                var textBlack = new TextBlock();
                textBlack.FontSize = this.ScaleTextSize;
                textBlack.TextAlignment = TextAlignment.Center;
                textBlack.Width = 34;
                textBlack.Text = (scaleText + this.Interval * i).ToString();
                textBlack.Foreground = this.ScaleBrush;
                // 36值越大，越往里面
                Canvas.SetLeft(textBlack, radius - (radius - 36) * Math.Cos(angle) - 17);
                Canvas.SetTop(textBlack, radius - (radius - 36) * Math.Sin(angle) - 10);
                mainCanvas.Children.Add(textBlack);
            }

            // "M100 200(起点坐标，从左上角开始),200 300(结束坐标)        这个圆弧和想要的结果不一样，需要向右选择90度
            var sData = "M{0} {1} A{0} {0} 0 1 1 {1} {2}";
            sData = String.Format(sData, radius / 2, radius, radius * 1.5);
            var converter = TypeDescriptor.GetConverter(typeof(Geometry));
            circle.Data = (Geometry)converter.ConvertFrom(sData);
            // Value值*最小刻度-45
            var value = (Value - MinNum) * (270.0 / (this.MaxNum - this.MinNum)) - 45;
            //rtPointer.Angle = Value * (270.0 / (max - min)) - 45;
            var doubleAnimation = new DoubleAnimation(value, new Duration(TimeSpan.FromMilliseconds(200)));
            rtPointer.BeginAnimation(RotateTransform.AngleProperty, doubleAnimation);
            // 画三角形 需要三个点
            sData = "M{0} {1},{1} {2},{1} {3}";
            sData = String.Format(sData, radius * 0.35, radius, radius - 5, radius + 5);
            converter = TypeDescriptor.GetConverter(typeof(Geometry));
            pointer.Data = (Geometry)converter.ConvertFrom(sData);
        }
    }
}
