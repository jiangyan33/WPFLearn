using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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

        private List<CourseModel> courseModelAll;

        public CommandBase OpenCourseUrlCommand { get; set; }

        public CommandBase TeacherFilterCommand { get; set; }

        public CoursePageViewModel()
        {
            OpenCourseUrlCommand = new CommandBase();

            OpenCourseUrlCommand.DoCanExecute = new Func<object, bool>(o => true);

            OpenCourseUrlCommand.DoExecute = new Action<object>((o) =>
            {
                System.Diagnostics.Process.Start(o.ToString());
            });

            TeacherFilterCommand = new CommandBase();

            TeacherFilterCommand.DoCanExecute = new Func<object, bool>(o => true);

            TeacherFilterCommand.DoExecute = DoFilter;
            InitCategory();
            InitCourseList();
        }

        private void DoFilter(object obj)
        {
            var teacherName = obj.ToString();
            var result = courseModelAll;
            if (teacherName != "全部")
            {
                result = result.FindAll(x => x.Teachers.Exists(it => it == teacherName));
            }
            CourseModels.Clear();
            foreach (var item in result)
            {
                CourseModels.Add(item);
            }
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

            for (var i = 0; i < 10; i++)
            {
                CourseModels.Add(new CourseModel { IsShowSkeleton = true });
            }

            Task.Run(new Action(() =>
           {
               courseModelAll = LocalDataAccess.GetInstance().GetCourses();
               Application.Current.Dispatcher.Invoke(new Action(() =>
               {
                   CourseModels.Clear();
                   foreach (var item in courseModelAll) CourseModels.Add(item);
               }));
           }));
        }
    }
}
