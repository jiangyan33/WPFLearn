using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zhaoxi.CourseManagement.Common;
using Zhaoxi.CourseManagement.DataAccess;
using Zhaoxi.CourseManagement.Model;

namespace Zhaoxi.CourseManagement.ViewModel
{
    public class CoursePageViewModel
    {
        public ObservableCollection<CategoryItemModel> CategoryCourses { get; set; }
        public ObservableCollection<CategoryItemModel> CategoryTechnology { get; set; }
        public ObservableCollection<CategoryItemModel> CategoryTeacher { get; set; }

        public ObservableCollection<CourseModel> CourseModels { get; set; }

        public CommandBase OpenCourseUrl { get; set; }

        public CoursePageViewModel()
        {
            OpenCourseUrl = new CommandBase();

            OpenCourseUrl.DoCanExecute = new Func<object, bool>(o => true);

            OpenCourseUrl.DoExecute = new Action<object>((o) =>
            {
                System.Diagnostics.Process.Start(o.ToString());
            });
            InitCategory();
            InitCourseList();
        }

        private void InitCategory()
        {
            CategoryCourses = new ObservableCollection<CategoryItemModel>();
            CategoryCourses.Add(new CategoryItemModel("全部", true));
            CategoryCourses.Add(new CategoryItemModel("公开课"));
            CategoryCourses.Add(new CategoryItemModel("VIP课程"));

            CategoryTechnology = new ObservableCollection<CategoryItemModel>();
            CategoryTechnology.Add(new CategoryItemModel("全部", true));
            CategoryTechnology.Add(new CategoryItemModel("C#"));
            CategoryTechnology.Add(new CategoryItemModel("ASP.NET"));
            CategoryTechnology.Add(new CategoryItemModel("微服务"));
            CategoryTechnology.Add(new CategoryItemModel("Java"));
            CategoryTechnology.Add(new CategoryItemModel("Vue"));
            CategoryTechnology.Add(new CategoryItemModel("微信小程序"));
            CategoryTechnology.Add(new CategoryItemModel("Winform"));
            CategoryTechnology.Add(new CategoryItemModel("上位机"));

            CategoryTeacher = new ObservableCollection<CategoryItemModel>();
            CategoryTeacher.Add(new CategoryItemModel("全部", true));
            var teacherList = LocalDataAccess.GetInstance().GetTeachers();
            foreach (var item in teacherList) CategoryTeacher.Add(new CategoryItemModel(item));
        }

        private void InitCourseList()
        {
            CourseModels = new ObservableCollection<CourseModel>();
            var courses = LocalDataAccess.GetInstance().GetCourses();
            foreach (var item in courses) CourseModels.Add(item);
        }
    }
}
