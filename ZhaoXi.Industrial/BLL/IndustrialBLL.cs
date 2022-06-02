using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZhaoXi.Communication;
using ZhaoXi.Industrial.DAL;
using ZhaoXi.Industrial.Model;

namespace ZhaoXi.Industrial.BLL
{
    /// <summary>
    /// 业务逻辑层
    /// </summary>
    public class IndustrialBLL
    {

        private readonly DataAccess da = new DataAccess();


        /// <summary>
        /// 获取串口信息
        /// </summary>
        /// <returns></returns>
        public DataResult<SerialInfo> InitSerialInfo()
        {
            var result = new DataResult<SerialInfo>();
            try
            {
                var si = new SerialInfo
                {
                    PortName = ConfigurationManager.AppSettings["port"].ToString(),
                    BaudRate = Convert.ToInt32(ConfigurationManager.AppSettings["baud"].ToString()),
                    DataBit = Convert.ToInt32(ConfigurationManager.AppSettings["data_bit"].ToString()),
                    Parity = (Parity)Enum.Parse(typeof(Parity), ConfigurationManager.AppSettings["parity"].ToString(), true),
                    StopBits = (StopBits)Enum.Parse(typeof(StopBits), ConfigurationManager.AppSettings["stopbit"].ToString(), true),
                };
                result.State = true;
                result.Data = si;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return result;
        }

        /// <summary>
        /// 获取存储区信息
        /// </summary>
        /// <returns></returns>
        public DataResult<List<StorageModel>> InitStorageArea()
        {
            DataResult<List<StorageModel>> result = new DataResult<List<StorageModel>>();

            try
            {
                var model = new StorageModel();

                var sa = da.GetStorageArea();

                result.Data = (from q in sa.AsEnumerable()
                               select new StorageModel
                               {
                                   Id = q.Field<string>("id"),
                                   SlaveAddress = Convert.ToInt32(q.Field<string>("slave_add")),
                                   FuncCode = q.Field<string>("func_code"),
                                   StartAddress = Convert.ToInt32(q.Field<string>("start_reg")),
                                   Length = Convert.ToInt32(q.Field<string>("length"))
                               }).ToList();

                result.State = true;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return result;
        }

        public DataResult<List<DeviceModel>> InitDevices()
        {
            DataResult<List<DeviceModel>> result = new DataResult<List<DeviceModel>>();

            try
            {
                var devices = da.GetDevices();

                var monitorValues = da.GetMonitorValues();

                var deviceList = new List<DeviceModel>();

                foreach (var dr in devices.AsEnumerable())
                {
                    var model = new DeviceModel();

                    deviceList.Add(model);

                    model.DeviceId = dr.Field<string>("d_id");

                    model.DeviceName = dr.Field<string>("d_name");

                    foreach (var mv in monitorValues.AsEnumerable().Where(m => m.Field<string>("d_id") == model.DeviceId))
                    {
                        var mvm = new MonitorValueModel();

                        model.MonitorValueList.Add(mvm);

                        mvm.ValueId = mv.Field<string>("value_id");

                        mvm.ValueName = mv.Field<string>("value_name");

                        mvm.StorageAreaId = mv.Field<string>("s_area_id");

                        mvm.StartAddress = mv.Field<int>("address");

                        mvm.DataType = mv.Field<string>("data_type");

                        mvm.IsAlarm = mv.Field<int>("is_alarm") == 1;

                        mvm.ValueDesc = mv.Field<string>("description");

                        mvm.Unit = mv.Field<string>("unit");


                        // 警戒值
                        var column = mv.Field<string>("alarm_lolo");

                        mvm.LoLoAlarm = column == null ? 0.0 : double.Parse(column);

                        column = mv.Field<string>("alarm_low");

                        mvm.LowAlarm = column == null ? 0.0 : double.Parse(column);

                        column = mv.Field<string>("alarm_high");

                        mvm.HighAlarm = column == null ? 0.0 : double.Parse(column);

                        column = mv.Field<string>("alarm_hihi");

                        mvm.HiHiAlarm = column == null ? 0.0 : double.Parse(column);

                        mvm.ValueStateChanged = (state, msg, valueId) =>
                        {
                            var value = model.WarningMessageList.FirstOrDefault(x => x.ValueId == valueId);

                            if (value != null)
                            {
                                model.WarningMessageList.Remove(value);
                            }

                            if (state != Base.MonitorValueState.OK)
                            {
                                model.IsWarning = true;

                                model.WarningMessageList.Add(new WarningMessageModel { ValueId = valueId, Message = msg });
                            }

                            if (model.WarningMessageList.Count > 0 != model.IsWarning)
                            {
                                model.IsWarning = model.WarningMessageList.Count > 0;
                            }
                        };
                    }

                }

                result.State = true;

                result.Data = deviceList;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return result;
        }
    }
}
