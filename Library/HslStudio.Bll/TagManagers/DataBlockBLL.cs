using HslStudio.ClassLibrary.Interface;
using HslStudio.ClassLibrary.TagManagers;
using System;
using System.Collections.Generic;
using System.Xml;
using static HslStudio.Com.Drivers.XCollection;
namespace HslStudio.Bll.TagManagers
{

    public class DataBlockBLL : IDataBlockBLL
    {
        public const string CHANNEL_ID = "ChannelId";
        public const string DEVICE_ID = "DeviceId";
        public const string DATABLOCK = "DataBlock";
        public const string DATABLOCK_ID = "DataBlockId";
        public const string DATABLOCK_NAME = "DataBlockName";
        public const string Function = "Function";
        public const string START_ADDRESS = "StartAddress";
        public const string MemoryType = "MemoryType";
        public const string LENGTH = "Length";
        public const string DATA_TYPE = "DataType";
        public const string Is_Array = "IsArray";
        public const string Active_ID = "IsActive";

        private readonly TagBLL objTagBLL;
        public DataBlockBLL()
        {
            objTagBLL = new TagBLL();
        }

        #region Singleton

        // For implementation refer to: http://geekswithblogs.net/BlackRabbitCoder/archive/2010/05/19/c-system.lazylttgt-and-the-singleton-design-pattern.aspx        
        private static readonly Lazy<DataBlockBLL> _instance = new Lazy<DataBlockBLL>(() => new DataBlockBLL());

        public static DataBlockBLL Instance => _instance.Value;

        #endregion


        public void Add(Device dv, DataBlock db)
        {
            try
            {
                if (db == null)
                {
                    throw new NullReferenceException("The DataBlock is null reference exception");
                }

                DataBlock fDv = IsExisted(dv, db);
                if (fDv != null)
                {
                    throw new Exception($"DataBlock name: '{db.DataBlockName}' is existed");
                }

                dv.DataBlocks.Add(db);
            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(GetType().Name, ex.Message);
            }
        }
        public void Add(Channel ch, Device dv, DataBlock db)
        {
            Device result = null;
            try
            {
                if (db == null)
                {
                    throw new NullReferenceException("The DataBlock is null reference exception");
                }

                foreach (Device item in ch.Devices)
                {
                    if (item.DeviceId == dv.DeviceId && item.DeviceName.Equals(dv.DeviceName))
                    {
                        result = item;
                        break;
                    }
                }

                DataBlock fDv = IsExisted(result, db);
                if (fDv != null)
                {
                    throw new Exception($"DataBlock name: '{db.DataBlockName}' is existed");
                }

                result.DataBlocks.Add(db);
            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(GetType().Name, ex.Message);
            }
        }

        public void Update(Device dv, DataBlock db)
        {
            try
            {
                if (db == null)
                {
                    throw new NullReferenceException("The DataBlock is null reference exception");
                }

                DataBlock fCh = IsExisted(dv, db);
                if (fCh != null)
                {
                    throw new Exception($"DataBlock name: '{db.DataBlockName}' is existed");
                }

                foreach (DataBlock item in dv.DataBlocks)
                {
                    if (item.DataBlockId == db.DataBlockId)
                    {
                        item.ChannelId = db.ChannelId;
                        item.DeviceId = db.DeviceId;
                        item.DataBlockId = db.DataBlockId;
                        item.DataBlockName = db.DataBlockName;
                        item.Description = db.Description;
                        item.Function = db.Function;
                        item.StartAddress = db.StartAddress;
                        item.MemoryType = db.MemoryType;
                        item.Length = db.Length;
                        item.IsArray = db.IsArray;
                        item.IsActive = db.IsActive;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(GetType().Name, ex.Message);
            }
        }


        public void DeleteById(Device dv, int dbId)
        {
            try
            {
                DataBlock result = GetById(dv, dbId);
                if (result == null)
                {
                    throw new KeyNotFoundException("DataBlock Id is not found exception");
                }

                dv.DataBlocks.Remove(result);
            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(GetType().Name, ex.Message);
            }
        }


        public void DeleteByName(Device dv, string dbName)
        {
            try
            {
                DataBlock result = GetByName(dv, dbName);
                if (result == null)
                {
                    throw new KeyNotFoundException("DataBlock name is not found exception");
                }

                dv.DataBlocks.Remove(result);
            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(GetType().Name, ex.Message);
            }
        }


        public void DeleteByObject(Device dv, DataBlock db)
        {
            try
            {
                if (db == null)
                {
                    throw new NullReferenceException("The DataBlock is null reference exception");
                }

                foreach (DataBlock item in dv.DataBlocks)
                {
                    if (item.DataBlockId == db.DataBlockId)
                    {
                        dv.DataBlocks.Remove(item);
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(GetType().Name, ex.Message);
            }
        }


        public DataBlock IsExisted(Device dv, DataBlock db)
        {
            DataBlock result = null;
            try
            {
                foreach (DataBlock item in dv.DataBlocks)
                {
                    if (item.DataBlockId != db.DataBlockId && item.DataBlockName.Equals(db.DataBlockName))
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


        public DataBlock GetById(Device ch, int dbId)
        {
            DataBlock result = null;
            try
            {
                foreach (DataBlock item in ch.DataBlocks)
                {
                    if (item.DataBlockId == dbId)
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


        public DataBlock GetByName(Device ch, string dbName)
        {
            DataBlock result = null;
            try
            {
                foreach (DataBlock item in ch.DataBlocks)
                {
                    if (item.DataBlockName.Equals(dbName))
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


        public List<DataBlock> GetAll(XmlNode dvNode)
        {
            List<DataBlock> dbList = new List<DataBlock>();
            try
            {
                foreach (XmlNode dbNote in dvNode)
                {
                    DataBlock db = new DataBlock
                    {
                        ChannelId = int.Parse(dbNote.Attributes[CHANNEL_ID].Value),
                        DeviceId = int.Parse(dbNote.Attributes[DEVICE_ID].Value),
                        DataBlockId = int.Parse(dbNote.Attributes[DATABLOCK_ID].Value),
                        DataBlockName = dbNote.Attributes[DATABLOCK_NAME].Value,
                        Function = $"{dbNote.Attributes[Function].Value}",
                        StartAddress = ushort.Parse(dbNote.Attributes[START_ADDRESS].Value),
                        MemoryType = $"{dbNote.Attributes[MemoryType].Value}",
                        Length = ushort.Parse(dbNote.Attributes[LENGTH].Value),
                        DataType = (DataTypes)System.Enum.Parse(typeof(DataTypes), string.Format("{0}", dbNote.Attributes[DATA_TYPE].Value)),
                        IsArray = bool.Parse(dbNote.Attributes[Is_Array].Value),
                        IsActive = bool.Parse(dbNote.Attributes[Active_ID].Value),
                        Description = dbNote.Attributes[ChannelBLL.DESCRIPTION].Value,
                        Tags = objTagBLL.GetAll(dbNote)
                    };
                    dbList.Add(db);
                }
            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(GetType().Name, ex.Message);
            }

            return dbList;
        }




    }
}