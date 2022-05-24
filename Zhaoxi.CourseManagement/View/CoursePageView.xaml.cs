using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Zhaoxi.CourseManagement.Model;
using Zhaoxi.CourseManagement.ViewModel;

namespace Zhaoxi.CourseManagement.View
{
    /// <summary>
    /// CoursePageView.xaml 的交互逻辑
    /// </summary>
    public partial class CoursePageView : UserControl
    {
        public CoursePageView()
        {
            InitializeComponent();
            this.DataContext = new CoursePageViewModel();
        }

        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            var teacher = (sender as RadioButton).Content.ToString();
            var view = CollectionViewSource.GetDefaultView(courseModels.ItemsSource);
            if (teacher == "全部")
            {
                view.Filter = null;
                //view.SortDescriptions.Add(new System.ComponentModel.SortDescription("CourseName", System.ComponentModel.ListSortDirection.Descending));
            }
            else
            {
                view.Filter = new Predicate<object>((o) =>
                {
                    return (o as CourseModel).Teachers.Exists(t => t == teacher);
                });
            }
        }
    }
}
