using System.Collections.ObjectModel;
using System.Windows.Input;
using ZhaoXi.Industrial.Base;
using ZhaoXi.Industrial.Model;

namespace ZhaoXi.Industrial.ViewModel
{
    public class SystemMonitorViewModel : NotifyBase
    {
        public ObservableCollection<LogModel> LogList { get; set; } = new ObservableCollection<LogModel>();

        private DeviceModel _currentDevice;

        public DeviceModel CurrentDevice
        {
            get { return _currentDevice; }
            set { Set<DeviceModel>(ref _currentDevice, value); }
        }

        private bool _isShowDetail;

        public bool IsShowDetail
        {
            get { return _isShowDetail; }
            set { Set<bool>(ref _isShowDetail, value); }
        }

        public DeviceModel TestDevice { get; set; }

        public SystemMonitorViewModel()
        {
            InitLogInfo();

            #region 测试数据
            TestDevice = new DeviceModel();

            TestDevice.DeviceName = "冷却塔 1#";

            TestDevice.IsRunning = true;

            TestDevice.IsWarning = true;

            TestDevice.MonitorValueList.Add(new MonitorValueModel
            {
                ValueId = "1",
                ValueName = "液位",
                Unit = "m",
                CurrentValue = 45
            });

            TestDevice.MonitorValueList.Add(new MonitorValueModel
            {
                ValueId = "1",
                ValueName = "入口压力",
                Unit = "Mpa",
                CurrentValue = 34
            });

            TestDevice.MonitorValueList.Add(new MonitorValueModel
            {
                ValueId = "1",
                ValueName = "入口温度",
                Unit = "℃",
                CurrentValue = 34
            });

            TestDevice.MonitorValueList.Add(new MonitorValueModel
            {
                ValueId = "1",
                ValueName = "出口压力",
                Unit = "Mpa",
                CurrentValue = 34
            });

            TestDevice.MonitorValueList.Add(new MonitorValueModel
            {
                ValueId = "1",
                ValueName = "出口温度",
                Unit = "Mpa",
                CurrentValue = 34
            });

            TestDevice.MonitorValueList.Add(new MonitorValueModel
            {
                ValueId = "1",
                ValueName = "补水压力",
                Unit = "Mpa",
                CurrentValue = 34
            });

            TestDevice.WarningMessageList.Add(new WarningMessageModel { Message = "冷却塔1#液位极低。当前值：0", ValueId = "1" });

            TestDevice.WarningMessageList.Add(new WarningMessageModel { Message = "冷却塔1#入口压力极低。当前值：0", ValueId = "1" });

            TestDevice.WarningMessageList.Add(new WarningMessageModel { Message = "冷却塔1#入口温度极低。当前值：0", ValueId = "1" });

            #endregion

            this.ComponentCommand = new CommandBase(DoTowerCommand);
        }

        private void InitLogInfo()
        {
            LogList.Add(new LogModel { RowNumber = 1, DeviceName = "冷却塔 1#", LogInfo = "已启动", LogType = LogType.Info });

            LogList.Add(new LogModel { RowNumber = 2, DeviceName = "冷却塔 2#", LogInfo = "已启动", LogType = LogType.Info });

            LogList.Add(new LogModel { RowNumber = 3, DeviceName = "冷却塔 3#", LogInfo = "液位极低", LogType = LogType.Warn });

            LogList.Add(new LogModel { RowNumber = 4, DeviceName = "循环水泵 1#", LogInfo = "频率过大", LogType = LogType.Warn });

            LogList.Add(new LogModel { RowNumber = 5, DeviceName = "循环水泵 2#", LogInfo = "已启动", LogType = LogType.Info });

            LogList.Add(new LogModel { RowNumber = 6, DeviceName = "循环水泵 3#", LogInfo = "已启动", LogType = LogType.Info });
        }

        public CommandBase ComponentCommand { get; set; }

        private void DoTowerCommand(object param)
        {

            var device = param as DeviceModel;

            if (CurrentDevice != device)
            {
                CurrentDevice = device;
            }
            if (!IsShowDetail)
                IsShowDetail = true;
        }
    }
}
