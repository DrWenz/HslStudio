using HslStudio.ClassLibrary.Interface;
using HslStudio.ClassLibrary.TagManagers;
using SQLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Xml;
using static HslStudio.Com.Drivers.XCollection;
namespace HslStudio.Bll.TagManagers.DB
{
    public class DBDeviceBLL : DBHelperBasic, IDeviceBLL
    {
        #region Singleton

        // For implementation refer to: http://geekswithblogs.net/BlackRabbitCoder/archive/2010/05/19/c-system.lazylttgt-and-the-singleton-design-pattern.aspx        
        private static readonly Lazy<DBDeviceBLL> _instance = new Lazy<DBDeviceBLL>(() => new DBDeviceBLL());

        public static DBDeviceBLL Instance => _instance.Value;
        #endregion
        public DBDeviceBLL()
        {
            InitializeDatabase();
        }

        public void Add(Channel ch, Device dv)
        {
            try
            {
                using (var connection =  TryCreateDatabase())
                {
                    connection.Insert(new Device { ChannelId = ch.ChannelId,
                        DeviceId = dv.DeviceId,
                        SlaveId = dv.SlaveId,
                        DeviceName = dv.DeviceName,
                        Description = dv.Description
                       

                    }, typeof(Device));
                }
             
            }
            catch (Exception ex)
            {

                EventscadaException?.Invoke(GetType().Name, ex.Message);
            }

          
        }

        public void DeleteById(Channel ch, int DeviceId)
        {
            try
            {
                using (var connection =   TryCreateDatabase())
                {
                    var result = connection.Table<Device>();
                   result.Where(a => a.ChannelId == ch.ChannelId && a.DeviceId == DeviceId).Delete();
                   
                }
              
            }
            catch (Exception ex)
            {

                EventscadaException?.Invoke(GetType().Name, ex.Message);
            }

           
        }

        public void DeleteByName(Channel ch, string dvName)
        {
            try
            {
                using (var connection = TryCreateDatabase())
                {
                    var result = connection.Table<Device>();
                    result.Where(a => a.ChannelId == ch.ChannelId && a.DeviceName == dvName).Delete();

                }
              
            }
            catch (Exception ex)
            {

                EventscadaException?.Invoke(GetType().Name, ex.Message);
            }

           
        }

        public void DeleteByObject(Channel ch, Device dv)
        {
            try
            {
                List<DataBlock> list = new DBDataBlockBLL().GetAll(dv);

                using (var connection = TryCreateDatabase())
                {
                    var result = connection.Table<Device>();
                    result.Where(a => a.ChannelId == ch.ChannelId && a.DeviceId == dv.DeviceId && a.DeviceName == dv.DeviceName).Delete();

                }
               
                foreach (DataBlock item in list)
                {
                    new DBDataBlockBLL().DeleteByObject(dv, item);
                }
            }
            catch (Exception ex)
            {

                EventscadaException?.Invoke(GetType().Name, ex.Message);
            }

           
        }

        public List<Device> GetAll(XmlNode chNode)
        {
            List<Device> devices = new List<Device>();

            try
            {
              

                    using (var connection = TryCreateDatabase())
                    {
                        devices = connection.Table<Device>().ToList();
                    }
                  
               

            }
            catch (Exception ex)
            {

                EventscadaException?.Invoke(GetType().Name, ex.Message);
            }

           
            return devices;
        }

        public List<Device> GetAll(Channel ch)
        {
            List<Device> devices = new List<Device>();

            try
            {

                using (var connection = TryCreateDatabase())
                {
                    devices = connection.Table<Device>().Where(x => x.ChannelId == ch.ChannelId).ToList();
                }

               
            }
            catch (Exception ex)
            {

                EventscadaException?.Invoke(GetType().Name, ex.Message);
            }

           
            return devices;
        }

        public Device GetById(Channel ch, int DeviceId)
        {
            Device device = new Device();
            try
            {
                using (var connection = TryCreateDatabase())
                {
                        device = connection.Table<Device>().Where(x => x.ChannelId == ch.ChannelId && x.DeviceId == DeviceId).FirstOrDefault();
                        device.DataBlocks = new DBDataBlockBLL().GetAll(device);
 
                }
               

            }
            catch (Exception ex)
            {

                EventscadaException?.Invoke(GetType().Name, ex.Message);
            }

          
            return device;
        }

        public Device GetByName(Channel ch, string dvName)
        {
            Device device = new Device();
            try
            {
                using (var connection = TryCreateDatabase())
                {
                        device = connection.Table<Device>().Where(x => x.ChannelId == ch.ChannelId && x.DeviceName == dvName).FirstOrDefault();
                        device.DataBlocks = new DBDataBlockBLL().GetAll(device);

                }
                

            }
            catch (Exception ex)
            {

                EventscadaException?.Invoke(GetType().Name, ex.Message);
            }

            
            return device;
        }

        public Device IsExisted(Channel ch, Device dv)
        {
            return dv;
        }

        public void Update(Channel ch, Device dv)
        {
            try
            {
                using (var connection = TryCreateDatabase())
                {
                    var dvr = connection.Table<Device>().Where(a => a.ChannelId ==ch.ChannelId && a.DeviceId == dv.DeviceId).FirstOrDefault();
                    
                    dvr.ChannelId = ch.ChannelId;
                    dvr.DeviceId = dv.DeviceId;
                    dvr.SlaveId = dv.SlaveId;
                    dvr.DeviceName = dv.DeviceName;
                    dvr.Description = dv.Description;

                    connection.Update(dvr, typeof(Device));
                }
                
            }
            catch (Exception ex)
            {

                EventscadaException?.Invoke(GetType().Name, ex.Message);
            }

          
        }


    }
}
