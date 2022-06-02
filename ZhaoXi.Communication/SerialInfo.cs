using System.IO.Ports;

namespace ZhaoXi.Communication
{
    /// <summary>
    /// 串口信息
    /// </summary>
    public class SerialInfo
    {
        /// <summary>
        /// 串口号
        /// </summary>
        public string PortName { get; set; } = "COM1";

        /// <summary>
        /// 波特率
        /// </summary>
        public int BaudRate { get; set; } = 9600;

        /// <summary>
        /// 数据位
        /// </summary>
        public int DataBit { get; set; } = 8;

        /// <summary>
        /// 校验位
        /// </summary>
        public Parity Parity { get; set; } = Parity.None;

        /// <summary>
        /// 停止位
        /// </summary>
        public StopBits StopBits { get; set; } = StopBits.One;
    }
}
