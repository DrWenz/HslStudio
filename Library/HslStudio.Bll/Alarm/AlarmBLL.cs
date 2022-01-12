using System;
using System.Collections.Generic;
using System.Xml;
using static HslStudio.Com.Drivers.XCollection;
namespace HslStudio.Bll.Alarm
{
    public class AlarmBLL
    {
        public const string ROOT = "Root";
        public const string AllAlarm = "Alarm";
        public const string Alarm_NAME = "AlarmName";
        public const string Alarm_Text = "AlarmText";
        public const string Alarm_Calss = "AlarmCalss";
        public const string Alarm_Value = "Value";
        public const string TriggerTeg = "TriggerTeg";
        public const string Channel = "Channel";
        public const string Device = "Device";
        public const string DataBlock = "DataBlock";
        public const string XML_NAME_DEFAULT = "AlarmCollection";
        private static readonly object mutex = new object();
        private static AlarmBLL _instance;

        public string XmlPath { set; get; }
        public List<ClassAlarm> Alarms { get; set; } = new List<ClassAlarm>();

        public static AlarmBLL GetAlarmManager()
        {
            lock (mutex)
            {
                if (_instance == null)
                {
                    _instance = new AlarmBLL();
                }
            }

            return _instance;
        }


        public void AddAlarm(ClassAlarm SQ)
        {
            try
            {
                if (SQ == null)
                {
                    throw new NullReferenceException("The Alarm is null reference exception");
                }

                ClassAlarm fCh = IsExisted(SQ);
                if (fCh != null)
                {
                    throw new Exception($"Alarm name: '{SQ.Name}' is existed");
                }

                Alarms.Add(SQ);
            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(GetType().Name, ex.Message);
            }
        }
        public ClassAlarm IsExisted(ClassAlarm ch)
        {
            ClassAlarm result = null;
            try
            {
                foreach (ClassAlarm item in Alarms)
                {
                    if (item.Name.Equals(ch.Name))
                    {
                        result = item;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {

                EventscadaException?.Invoke(GetType().Name, ex.Message);
            }

            return result;
        }

        public void UpdateAlarm(ClassAlarm ch)
        {
            try
            {
                if (ch == null)
                {
                    throw new NullReferenceException("The Alarms is null reference exception");
                }

                ClassAlarm fCh = IsExisted(ch);
                if (fCh != null)
                {
                    throw new Exception($"Alarms name: '{ch.Name}' is existed");
                }

                foreach (ClassAlarm item in Alarms)
                {
                    if (item.Name == ch.Name)
                    {
                        item.Name = ch.Name;
                        item.AlarmCalss = ch.AlarmCalss;
                        item.AlarmText = ch.AlarmText;
                        item.Value = ch.Value;
                        item.TriggerTeg = ch.TriggerTeg;
                        item.Channel = ch.Channel;
                        item.Device = ch.Device;
                        item.DataBlock = ch.DataBlock;

                    }
                }
            }
            catch (Exception ex)
            {

                EventscadaException?.Invoke(GetType().Name, ex.Message);
            }
        }


        /// <summary>
        ///     Xóa kênh.
        /// </summary>
        /// <param name="chName">Tên kênh</param>
        public void Delete(string chName)
        {
            try
            {
                ClassAlarm result = GetByAlarmName(chName);
                if (result == null)
                {
                    throw new KeyNotFoundException("Alarms name is not found exception");
                }

                Alarms.Remove(result);
            }
            catch (Exception ex)
            {

                EventscadaException?.Invoke(GetType().Name, ex.Message);
            }
        }

        /// <summary>
        ///     Xóa kênh.
        /// </summary>
        /// <param name="ch">Kênh</param>
        public void DeleteAlarm(ClassAlarm ch)
        {
            try
            {
                if (ch == null)
                {
                    throw new NullReferenceException("The Alarms is null reference exception");
                }

                foreach (ClassAlarm item in Alarms)
                {
                    if (item.Name == ch.Name)
                    {
                        Alarms.Remove(item);
                        break;
                    }
                }
            }
            catch (Exception ex)
            {

                EventscadaException?.Invoke(GetType().Name, ex.Message);
            }
        }


        /// <summary>
        ///     Tìm kiếm kênh theo tên kênh.
        /// </summary>
        /// <param name="chName">Tên kênh</param>
        /// <returns>Kênh</returns>
        public ClassAlarm GetByAlarmName(string chName)
        {
            ClassAlarm result = null;
            try
            {
                foreach (ClassAlarm item in Alarms)
                {
                    if (item.Name.Equals(chName))
                    {
                        result = item;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {

                EventscadaException?.Invoke(GetType().Name, ex.Message);
            }

            return result;
        }
        public List<ClassAlarm> GetAlarms(string XmlPath)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                if (string.IsNullOrEmpty(XmlPath) || string.IsNullOrWhiteSpace(XmlPath))
                {
                    //XmlPath = ReadKey(XML_NAME_DEFAULT);
                }

                xmlDoc.Load(XmlPath);
                XmlNodeList nodes = xmlDoc.SelectNodes(ROOT);
                foreach (XmlNode rootNode in nodes)
                {
                    XmlNodeList channelNodeList = rootNode.SelectNodes(AllAlarm);
                    foreach (XmlNode chNode in channelNodeList)
                    {
                        ClassAlarm newClassAlarm = new ClassAlarm();


                        if (newClassAlarm != null)
                        {

                            newClassAlarm.Name = chNode.Attributes[Alarm_NAME].Value;
                            newClassAlarm.AlarmCalss = chNode.Attributes[Alarm_Calss].Value;
                            newClassAlarm.AlarmText = chNode.Attributes[Alarm_Text].Value;
                            newClassAlarm.Channel = chNode.Attributes[Channel].Value;
                            newClassAlarm.DataBlock = chNode.Attributes[DataBlock].Value;
                            newClassAlarm.Device = chNode.Attributes[Device].Value;
                            newClassAlarm.TriggerTeg = chNode.Attributes[TriggerTeg].Value;
                            newClassAlarm.Value = chNode.Attributes[Alarm_Value].Value;

                            Alarms.Add(newClassAlarm);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                EventscadaException?.Invoke(GetType().Name, ex.Message);
            }

            return Alarms;
        }




    }
}
