using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZhaoXi.Industrial.Model
{
    public class DeviceModel
    {
        public string DeviceId { get; set; }

        public string DeviceName { get; set; }

        public bool IsRunning { get; set; } = true;

        public ObservableCollection<MonitorValueModel> MonitorValueList { get; set; } = new
            ObservableCollection<MonitorValueModel>();

        public ObservableCollection<WarningMessageModel> WarningMessageList = new ObservableCollection<WarningMessageModel>();

        public bool IsWarning { get; set; }

    }
}
