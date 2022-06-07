using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZhaoXi.Industrial.Base;

namespace ZhaoXi.Industrial.Model
{
    public class DeviceModel : NotifyBase
    {
        public string DeviceId { get; set; }

        public string DeviceName { get; set; }

        private bool _isRunning;

        public bool IsRunning
        {
            get { return _isRunning; }
            set { Set<bool>(ref _isRunning, value); }
        }

        private bool _isWarning;

        public bool IsWarning
        {
            get { return _isWarning; }
            set { Set<bool>(ref _isWarning, value); }
        }




        public ObservableCollection<MonitorValueModel> MonitorValueList { get; set; } = new
            ObservableCollection<MonitorValueModel>();

        public ObservableCollection<WarningMessageModel> WarningMessageList { get; set; } = new ObservableCollection<WarningMessageModel>();


    }
}
