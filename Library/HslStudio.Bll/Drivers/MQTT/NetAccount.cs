using System;
namespace HslStudio.Bll.Drivers.MQTT
{
    public class NetAccount
    {
        /// <summary>
        /// 唯一ID
        /// </summary>
        public string Guid { get; set; }
        /// <summary>
        /// Ip地址
        /// </summary>
        public string Ip { get; set; }
        /// <summary>
        /// 上线时间
        /// </summary>
        public DateTime OnlineTime { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }



        private string GetOnlineTime()
        {
            TimeSpan ts = DateTime.Now - OnlineTime;
            if (ts.TotalSeconds < 60)
            {
                return ts.Seconds + " 秒";
            }
            else if (ts.TotalHours < 1)
            {
                return ts.Minutes + "分" + ts.Seconds + "秒";
            }
            else if (ts.TotalDays < 1)
            {
                return ts.Hours + "时" + ts.Minutes + "分";
            }
            else
            {
                return ts.Days + "天" + ts.Hours + "时";
            }
        }

        /// <summary>
        /// String identifier
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "[" + Ip + "] : online time " + GetOnlineTime();
        }
    }
}
