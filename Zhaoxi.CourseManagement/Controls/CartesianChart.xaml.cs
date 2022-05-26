using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Zhaoxi.CourseManagement.Controls
{
    /// <summary>
    /// CartesianChart.xaml 的交互逻辑
    /// </summary>
    public partial class CartesianChart : UserControl
    {
        #region 依赖属性

        /// <summary>
        /// 
        /// </summary>
        public string IconText
        {
            get { return (string)GetValue(IconTextProperty); }
            set { SetValue(IconTextProperty, value); }
        }

        public static readonly DependencyProperty IconTextProperty =
            DependencyProperty.Register(nameof(IconText), typeof(string), typeof(CartesianChart), new PropertyMetadata("", new PropertyChangedCallback(OnPropertyChanged)));


        /// <summary>
        /// 
        /// </summary>
        public string LabelText
        {
            get { return (string)GetValue(LabelTextProperty); }
            set { SetValue(LabelTextProperty, value); }
        }

        public static readonly DependencyProperty LabelTextProperty =
            DependencyProperty.Register(nameof(LabelText), typeof(string), typeof(CartesianChart), new PropertyMetadata("", new PropertyChangedCallback(OnPropertyChanged)));


        /// <summary>
        /// 
        /// </summary>
        public string Value
        {
            get { return (string)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(nameof(Value), typeof(string), typeof(CartesianChart), new PropertyMetadata("", new PropertyChangedCallback(OnPropertyChanged)));


        /// <summary>
        /// 
        /// </summary>
        public string Values
        {
            get { return (string)GetValue(ValuesProperty); }
            set { SetValue(ValuesProperty, value); }
        }

        public static readonly DependencyProperty ValuesProperty =
            DependencyProperty.Register(nameof(Values), typeof(string), typeof(CartesianChart), new PropertyMetadata("", new PropertyChangedCallback(OnPropertyChanged)));

        #endregion
        public CartesianChart()
        {
            InitializeComponent();
        }

        private static void OnPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var instance = obj as CartesianChart;

            var newValue = args.NewValue.ToString();

            if (args.Property == ValueProperty)
            {
                instance.textBlockValueOne.Text = newValue;
                instance.textBlockValueTwo.Text = newValue;
            }
            else if (args.Property == ValuesProperty)
            {
                if (!string.IsNullOrEmpty(newValue))
                {
                    var arr = newValue.Split(',').Select(x => Convert.ToDouble(x)).ToArray();

                    instance.lineSeries.Values = new LiveCharts.ChartValues<double>(arr);
                }
            }
            else if (args.Property == LabelTextProperty)
            {
                instance.textBlockKey.Text = newValue;
            }
            else if (args.Property == IconTextProperty)
            {
                instance.textBlockIcon.Text = newValue;
            }
        }
    }
}
