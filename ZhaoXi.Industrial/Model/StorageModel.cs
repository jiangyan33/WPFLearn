namespace ZhaoXi.Industrial.Model
{
    /// <summary>
    /// 存储区
    /// </summary>
    public class StorageModel
    {
        public string Id { get; set; }

        /// <summary>
        /// 从站地址
        /// </summary>
        public int SlaveAddress { get; set; }

        /// <summary>
        /// 功能码
        /// </summary>
        public string FuncCode { get; set; }

        /// <summary>
        /// 起始地址
        /// </summary>
        public int StartAddress { get; set; }

        /// <summary>
        /// 长度
        /// </summary>
        public int Length { get; set; }
    }
}
