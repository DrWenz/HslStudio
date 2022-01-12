using HslStudio.ClassLibrary.Interface;
using HslStudio.ClassLibrary.TagManagers;
using System;
using System.Collections.Generic;
using System.Xml;
using static HslStudio.Com.Drivers.XCollection;
namespace HslStudio.Bll.TagManagers

{

    public class DeviceBLL : IDeviceBLL
    {
        public const string CHANNEL_ID = "ChannelId";
        public const string DEVICE = "Device";
        public const string DEVICE_ID = "DeviceId";
        public const string DEVICE_NAME = "DeviceName";
        public const string SLAVE_ID = "SlaveId";
        public const string Active_ID = "IsActive";


        private readonly DataBlockBLL objDataBlockBLL;
        public DeviceBLL()
        {
            objDataBlockBLL = new DataBlockBLL();
        }

        #region Singleton

        // For implementation refer to: http://geekswithblogs.net/BlackRabbitCoder/archive/2010/05/19/c-system.lazylttgt-and-the-singleton-design-pattern.aspx        
        private static readonly Lazy<DeviceBLL> _instance = new Lazy<DeviceBLL>(() => new DeviceBLL());

        public static DeviceBLL Instance => _instance.Value;

        #endregion
        public void Add(Channel ch, Device dv)
        {
            try
            {
                if (dv == null)
                {
                    throw new NullReferenceException("The device is null reference exception");
                }

                Device fDv = IsExisted(ch, dv);
                if (fDv != null)
                {
                    throw new Exception($"Device name: '{dv.DeviceName}' is existed");
                }

                ch.Devices.Add(dv);
            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(GetType().Name, ex.Message);
            }
        }


        public void Update(Channel ch, Device dv)
        {
            try
            {
                if (dv == null)
                {
                    throw new NullReferenceException("The Device is null reference exception");
                }

                Device fCh = IsExisted(ch, dv);
                if (fCh != null)
                {
                    throw new Exception($"Device name: '{dv.DeviceName}' is existed");
                }

                foreach (Device item in ch.Devices)
                {
                    if (item.DeviceId == dv.DeviceId)
                    {
                        item.DeviceId = dv.DeviceId;
                        item.DeviceName = dv.DeviceName;
                        item.SlaveId = dv.SlaveId;
                        item.Description = dv.Description;
                        item.IsActive = dv.IsActive;
                        item.DataBlocks = dv.DataBlocks;
                        break;
                    }
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
                Device result = GetById(ch, DeviceId);
                if (result == null)
                {
                    throw new KeyNotFoundException("Device Id is not found exception");
                }

                ch.Devices.Remove(result);
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
                Device result = GetByName(ch, dvName);
                if (result == null)
                {
                    throw new KeyNotFoundException("Device name is not found exception");
                }

                ch.Devices.Remove(result);
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
                if (dv == null)
                {
                    throw new NullReferenceException("The Device is null reference exception");
                }

                foreach (Device item in ch.Devices)
                {
                    if (item.DeviceId == dv.DeviceId)
                    {
                        ch.Devices.Remove(item);
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(GetType().Name, ex.Message);
            }
        }


        public Device IsExisted(Channel ch, Device dv)
        {
            Device result = null;
            try
            {
                foreach (Device item in ch.Devices)
                {
                    if (item.DeviceId != dv.DeviceId && item.DeviceName.Equals(dv.DeviceName))
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


        public Device GetById(Channel ch, int DeviceId)
        {
            Device result = null;
            try
            {
                foreach (Device item in ch.Devices)
                {
                    if (item.DeviceId == DeviceId)
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


        public Device GetByName(Channel ch, string dvName)
        {
            Device result = null;
            try
            {
                foreach (Device item in ch.Devices)
                {
                    if (item.DeviceName.Equals(dvName))
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


        public List<Device> GetAll(XmlNode chNode)
        {
            List<Device> dvList = new List<Device>();
            try
            {
                foreach (XmlNode dvNode in chNode)
                {
                    Device newDevice = new Device
                    {
                        ChannelId = int.Parse(dvNode.Attributes[CHANNEL_ID].Value),
                        DeviceId = int.Parse(dvNode.Attributes[DEVICE_ID].Value),
                        DeviceName = dvNode.Attributes[DEVICE_NAME].Value,
                        SlaveId = short.Parse(dvNode.Attributes[SLAVE_ID].Value),
                        IsActive = bool.Parse(dvNode.Attributes[Active_ID].Value),
                        Description = dvNode.Attributes[ChannelBLL.DESCRIPTION].Value,
                        DataBlocks = objDataBlockBLL.GetAll(dvNode)
                    };
                    dvList.Add(newDevice);
                }
            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(GetType().Name, ex.Message);
            }

            return dvList;
        }


    }
}