using HslStudio.ClassLibrary.Interface;
using HslStudio.ClassLibrary.TagManagers;
using System.Xml;
using static HslStudio.Com.Drivers.XCollection;
namespace HslStudio.Bll.TagManagers
{

    public class ChannelBLL : IChannelBLL
    {
        public const string ROOT = "Root";
        public const string CHANNEL = "Channel";
        public const string CHANNEL_ID = "ChannelId";
        public const string CHANNEL_NAME = "ChannelName";
        public const string CHANNEL_Types = "ChannelTypes";
        public const string MODEL = "Model";
        public const string DESCRIPTION = "Description";
        public const string Channel_Address = "ChannelAddress";
        public const string MODE = "Mode";
        public const string Connection_Type = "ConnectionType";

        public const string Active_ID = "IsActive";
        public string XML_NAME_DEFAULT { get; set; } = "TagCollection";
        public List<Channel> Channels { get; set; } = new List<Channel>();
        public string XmlPath { set; get; }

        private readonly DeviceBLL objDeviceBLL = new DeviceBLL();
        #region Singleton

        // For implementation refer to: http://geekswithblogs.net/BlackRabbitCoder/archive/2010/05/19/c-system.lazylttgt-and-the-singleton-design-pattern.aspx        
        private static readonly Lazy<ChannelBLL> _instance = new Lazy<ChannelBLL>(() => new ChannelBLL());

        public static ChannelBLL Instance => _instance.Value;

        #endregion
        public void Add(Channel ch)
        {
            try
            {
                if (ch == null)
                {
                    throw new NullReferenceException("The Channel is null reference exception");
                }

                Channel fCh = IsExisted(ch);
                if (fCh != null)
                {
                    throw new Exception($"Channel name: '{ch.ChannelName}' is existed");
                }

                Channels.Add(ch);
            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(GetType().Name, ex.Message);
            }
        }


        public void Update(Channel ch)
        {
            try
            {
                if (ch == null)
                {
                    throw new NullReferenceException("The Channel is null reference exception");
                }

                Channel fCh = IsExisted(ch);
                if (fCh != null)
                {
                    throw new Exception($"Channel name: '{ch.ChannelName}' is existed");
                }

                foreach (Channel item in Channels)
                {
                    if (item.ChannelId == ch.ChannelId)
                    {
                        item.ChannelName = ch.ChannelName;
                        item.ChannelTypes = ch.ChannelTypes;
                        item.Description = ch.Description;
                        item.Mode = ch.Mode;
                        item.Model = ch.Model;
                        item.ChannelAddress = ch.ChannelAddress;
                        item.ConnectionType = ch.ConnectionType;
                        item.IsActive = ch.IsActive;
                        item.Devices = ch.Devices;

                    }
                }
            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(GetType().Name, ex.Message);
            }
        }

        public void DeleteByObject(Channel ch)
        {
            try
            {
                if (ch == null)
                {
                    throw new NullReferenceException("The Channel is null reference exception");
                }

                foreach (Channel item in Channels)
                {
                    if (item.ChannelId == ch.ChannelId)
                    {
                        Channels.Remove(item);
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(GetType().Name, ex.Message);
            }
        }


        public Channel IsExisted(Channel ch)
        {
            Channel result = null;
            try
            {
                foreach (Channel item in Channels)
                {
                    if (item.ChannelId != ch.ChannelId && item.ChannelName.Equals(ch.ChannelName))
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


        public Channel GetById(int chId)
        {
            Channel result = null;
            try
            {
                foreach (Channel item in Channels)
                {
                    if (item.ChannelId == chId)
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


        public Channel GetByName(string chName)
        {
            Channel result = null;
            try
            {
                foreach (Channel item in Channels)
                {
                    if (item.ChannelName.Equals(chName))
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

        public List<Channel> GetAll(string XmlPath)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                if (string.IsNullOrEmpty(XmlPath) || string.IsNullOrWhiteSpace(XmlPath))
                {
                    XmlPath = ReadKey(XML_NAME_DEFAULT);
                }

                xmlDoc.Load(XmlPath);
                XmlNodeList nodes = xmlDoc.SelectNodes(ROOT);
                foreach (XmlNode rootNode in nodes)
                {
                    XmlNodeList channelNodeList = rootNode.SelectNodes(CHANNEL);
                    foreach (XmlNode chNode in channelNodeList)
                    {
                        Channel newChannel = new Channel();
                        string connType = chNode.Attributes[Connection_Type].Value;



                        if (newChannel != null)
                        {
                            newChannel.ChannelId = int.Parse(chNode.Attributes[CHANNEL_ID].Value);
                            newChannel.ChannelName = chNode.Attributes[CHANNEL_NAME].Value;
                            newChannel.ConnectionType = connType;
                            newChannel.ChannelTypes = chNode.Attributes[CHANNEL_Types].Value;
                            newChannel.Model = chNode.Attributes[MODEL].Value;
                            newChannel.ChannelAddress = chNode.Attributes[Channel_Address].Value;
                            newChannel.Mode = chNode.Attributes[MODE].Value;
                            newChannel.IsActive = bool.Parse(chNode.Attributes[Active_ID].Value);
                            newChannel.Description = chNode.Attributes[DESCRIPTION].Value;
                            newChannel.Devices = objDeviceBLL.GetAll(chNode);
                            Channels.Add(newChannel);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(GetType().Name, ex.Message);
            }

            return Channels;
        }

        public string ReadKey(string keyName)
        {
            string result = string.Empty;
            try
            {
 
                //string databasePath = Windows.Storage.ApplicationData.Current.LocalFolder.Path + @"\TagCollection.xml";
        

                //if (!File.Exists(databasePath))
                //{
                //    System.IO.File.WriteAllText(databasePath, ScadeDataAccess.ActiveTagCollection());

                //}
 
               
                //result = databasePath;
            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(GetType().Name, ex.Message);
            }

            return result;
        }
    }
}