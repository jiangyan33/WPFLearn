using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZhaoXi.Communication;
using ZhaoXi.Communication.Modbus;
using ZhaoXi.Industrial.BLL;
using ZhaoXi.Industrial.Model;

namespace ZhaoXi.Industrial.Base
{
    public class GlobalMonitor
    {
        public static List<StorageModel> StorageList { get; set; }

        public static List<DeviceModel> DeviceList { get; set; } = new List<DeviceModel>();

        public static SerialInfo SerialInfo { get; set; }

        public static Task mainTask = null;

        public static bool isRunning = true;

        public static RTU rtuInstance;

        public static void Start(Action successAction, Action<string> faultAction)
        {
            mainTask = Task.Run(async () =>
             {
                 var bll = new IndustrialBLL();

                 var si = bll.InitSerialInfo();

                 if (si.State)
                 {
                     SerialInfo = si.Data;
                 }
                 else
                 {
                     faultAction(si.Message); return;
                 }

                 var sa = bll.InitStorageArea();

                 if (sa.State)
                 {
                     StorageList = sa.Data;
                 }
                 else
                 {
                     faultAction(si.Message); return;
                 }

                 // 初始化设备变量集合及警戒值
                 var dr = bll.InitDevices();

                 if (dr.State)
                 {
                     DeviceList = dr.Data;
                 }
                 else
                 {
                     faultAction(si.Message); return;
                 }

                 // 初始化串口通信
                 rtuInstance = RTU.GetInstance(SerialInfo);

                 rtuInstance.ResponseData = ParsingData;

                 if (rtuInstance.Connection())
                 {
                     successAction();

                     int startAddr = 0;

                     while (isRunning)
                     {

                         foreach (var item in StorageList)
                         {
                             if (item.Length > 100)
                             {
                                 startAddr = item.StartAddress;

                                 int readCount = item.Length / 100;

                                 for (int i = 0; i < readCount; i++)
                                 {
                                     int readLen = i == readCount ? item.Length - (100 * i) : 100;

                                     await rtuInstance.Send(item.SlaveAddress, byte.Parse(item.FuncCode), startAddr + 100 * i, readLen);

                                 }
                             }

                             if (item.Length % 100 > 0)
                             {
                                 await rtuInstance.Send(item.SlaveAddress, byte.Parse(item.FuncCode), startAddr + 100 * (item.Length / 100), item.Length % 100);
                             }
                         }

                     }
                 }
                 else
                 {
                     faultAction("程序无法启动，串口给连接初始化失败！请检查设备连接是否正常。"); return;
                 }
             });
        }

        private static void ParsingData(int startAddr, List<byte> byteList)
        {
            if (byteList != null && byteList.Count > 0)
            {
                // 查找设备监控点位与当前返回报文相关的监控列表 根据从站地址、功能码、起始地址
                var mv1 = (from q in DeviceList
                           from m in q.MonitorValueList
                           where m.StorageAreaId == (byteList[0].ToString() + byteList[1].ToString("00") + startAddr.ToString())
                           && q.IsRunning
                           select m
                           ).ToList();


                int startByte;

                byte[] res = null;

                foreach (var item in mv1)
                {
                    switch (item.DataType)
                    {
                        case "Float":

                            startByte = (item.StartAddress * 2) + 3;

                            if (startByte < startAddr + byteList.Count)
                            {
                                res = new byte[4] { byteList[startByte], byteList[startByte + 1], byteList[startByte + 2], byteList[startByte + 3] };

                                item.CurrentValue = Convert.ToDouble(res.ByteArrayToFloat());
                            }

                            break;

                        case "Bool":

                            break;
                    }
                }
            }
        }

        public static void Dispose()
        {
            isRunning = false;

            if (rtuInstance != null) rtuInstance.Dispose();

            if (mainTask != null)
            {
                mainTask.Wait();
                mainTask.Dispose();
            }
        }


    }
}
