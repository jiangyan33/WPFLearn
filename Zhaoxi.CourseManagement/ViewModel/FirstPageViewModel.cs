using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Zhaoxi.CourseManagement.Common;
using Zhaoxi.CourseManagement.DataAccess;
using Zhaoxi.CourseManagement.Model;

namespace Zhaoxi.CourseManagement.ViewModel
{
    public class FirstPageViewModel : NotifyBase
    {

        private int _platNum;

        public int PlatNum
        {
            get { return _platNum; }
            set { _platNum = value; DoNotify(); }
        }


        private int _instrumentValue = 0;

        public int InstrumentValue
        {
            get { return _instrumentValue; }
            set { _instrumentValue = value; DoNotify(); }
        }

        public ObservableCollection<CourseSeriesModel> CourseSeriesList { get; set; } = new ObservableCollection<CourseSeriesModel>();

        public FirstPageViewModel()
        {
            RefreshInstrumentValue();
            InitCourseSeries();
        }

        private readonly Random random = new Random();

        private bool taskSwitch = true;
        private readonly List<Task> taskList = new List<Task>();

        private void InitCourseSeries()
        {
            var cList = LocalDataAccess.GetInstance().GetCoursePlayRecord();
            PlatNum = cList.Max(x => x.SeriesList.Count);
            foreach (var c in cList)
            {
                CourseSeriesList.Add(c);
            }
        }

        private void RefreshInstrumentValue()
        {
            var task = Task.Factory.StartNew(async () =>
              {
                  while (taskSwitch)
                  {
                      InstrumentValue = random.Next(Math.Max(_instrumentValue - 5, -20), Math.Min(_instrumentValue + 5, 80));
                      await Task.Delay(1500);
                  }
              });
            taskList.Add(task);
        }

        public void Dispose()
        {
            try
            {
                taskSwitch = false;
                Task.WaitAll(taskList.ToArray());
            }
            catch
            {
            }
        }

    }
}
