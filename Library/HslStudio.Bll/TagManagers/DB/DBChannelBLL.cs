using HslStudio.ClassLibrary.Interface;
using HslStudio.ClassLibrary.TagManagers;
using static HslStudio.Com.Drivers.XCollection;
namespace HslStudio.Bll.TagManagers.DB
{
    public class DBChannelBLL : DBHelperBasic, IChannelBLL
    {
        #region Singleton

        // For implementation refer to: http://geekswithblogs.net/BlackRabbitCoder/archive/2010/05/19/c-system.lazylttgt-and-the-singleton-design-pattern.aspx        
        private static readonly Lazy<DBChannelBLL> _instance = new Lazy<DBChannelBLL>(() => new DBChannelBLL());

        public static DBChannelBLL Instance => _instance.Value;

        #endregion



        public List<Channel> Channels { get; set; } = new List<Channel>();


        public DBChannelBLL()
        {
            InitializeDatabase();
        }

        public void Add(Channel ch)
        {
            try
            {
                using (var connection = TryCreateDatabase())
                {
                    connection.Insert(ch, typeof(Channel));
                }

                Channels.Add(ch);
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
                List<Device> list = new DBDeviceBLL().GetAll(ch);
                using (var connection = TryCreateDatabase())
                {
                    connection.Delete<Channel>(ch.ChannelId);
                }

                foreach (Device item in list)
                {
                    new DBDeviceBLL().DeleteByObject(ch, item);
                }
            }
            catch (Exception ex)
            {

                EventscadaException?.Invoke(GetType().Name, ex.Message);
            }


        }

        public List<Channel> GetAll(string XmlPath)
        {


            try
            {
                using (var connection = TryCreateDatabase())
                {
                    Channels = connection.Table<Channel>().ToList();
                }

                if (Channels != null)
                {
                    foreach (Channel item in Channels)
                    {
                        item.Devices = new DBDeviceBLL().GetAll(item);
                        if (item.Devices != null && item.Devices.Count > 0)
                        {
                            foreach (Device device in item.Devices)
                            {
                                device.DataBlocks = new DBDataBlockBLL().GetAll(device);
                                if (device.DataBlocks != null && device.DataBlocks.Count > 0)
                                {
                                    foreach (DataBlock dataBlock in device.DataBlocks)
                                    {
                                        dataBlock.Tags = new DBTagBLL().GetAll(dataBlock);
                                    }
                                }
                            }
                        }
                    }
                    return Channels;
                }
                return Channels;
            }
            catch (Exception ex)
            {

                EventscadaException?.Invoke(GetType().Name, ex.Message);
            }


            return null;
        }

        public Channel GetById(int chId)
        {
            Channel channel = new Channel();
            try
            {
                using (var connection = TryCreateDatabase())
                {
                        channel = connection.Table<Channel>().Where(x => x.ChannelId == chId).FirstOrDefault();
                        channel.Devices = new DBDeviceBLL().GetAll(channel);
    
                }



            }
            catch (Exception ex)
            {

                EventscadaException?.Invoke(GetType().Name, ex.Message);
            }

            return channel;


        }

        public Channel GetByName(string chName)
        {
            Channel channel = new Channel();
            try
            {


                using (var connection = TryCreateDatabase())
                {
                        channel = connection.Table<Channel>().Where(x => x.ChannelName == chName).FirstOrDefault();
                        channel.Devices = new DBDeviceBLL().GetAll(channel);
                 
                }




            }
            catch (Exception ex)
            {

                EventscadaException?.Invoke(GetType().Name, ex.Message);
            }


            return channel;
        }

        public Channel IsExisted(Channel ch)
        {
            Channel channel = GetByName(ch.ChannelName);
            if (channel != null)
            {
                return channel;
            }

            return channel;
        }



        public void Update(Channel ch)
        {
            try
            {
               
               
                using (var connection = TryCreateDatabase())
                {
                    Channel channel = GetById(ch.ChannelId);

                    channel.ChannelId = ch.ChannelId;
                    channel.ChannelName = ch.ChannelName;
                    channel.ChannelTypes = ch.ChannelTypes;
                    channel.ConnectionType = ch.ConnectionType;
                    channel.ChannelAddress = ch.ChannelAddress;
                    channel.IsActive = ch.IsActive;
                    channel.Mode = ch.Mode;
                    channel.Model = ch.Model;
                    channel.Description = ch.Description;

                    connection.Update(channel, typeof(Channel));
                }
            }
            catch (Exception ex)
            {

                EventscadaException?.Invoke(GetType().Name, ex.Message);
            }


        }

        public string ReadKey(string xML_NAME_DEFAULT)
        {
            string result = string.Empty;
            try
            {

               
                //string databasePath = Windows.Storage.ApplicationData.Current.LocalFolder.Path + @"\EVStudio_DB.db";
          
                //if (!File.Exists(databasePath))
                //{
                //    System.IO.File.WriteAllBytes(databasePath, ScadeDataAccess.ActiveEVStudio_DB());

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
