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
    public class DBDataBlockBLL : DBHelperBasic, IDataBlockBLL
    {
        #region Singleton

        // For implementation refer to: http://geekswithblogs.net/BlackRabbitCoder/archive/2010/05/19/c-system.lazylttgt-and-the-singleton-design-pattern.aspx        
        private static readonly Lazy<DBDataBlockBLL> _instance = new Lazy<DBDataBlockBLL>(() => new DBDataBlockBLL());

        public static DBDataBlockBLL Instance => _instance.Value;
        #endregion
        public DBDataBlockBLL()
        {
            InitializeDatabase();
        }

        public void Add(Channel ch, Device dv, DataBlock db)
        {
            try
            {
                using (var connection = TryCreateDatabase())
                {
                    connection.Insert(new DataBlock
                    {
                        ChannelId = dv.ChannelId,
                        DeviceId = dv.DeviceId,
                        DataBlockId = db.DataBlockId,
                        DataBlockName = db.DataBlockName,
                        StartAddress = db.StartAddress,
                        Length = db.Length,
                        DataType = db.DataType,
                        MemoryType = db.MemoryType,
                        Function = db.Function,
                        IsArray = db.IsArray,
                        Description = dv.Description


                    }, typeof(DataBlock));
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
                using (var connection = TryCreateDatabase())
                {
                    var result = connection.Table<DataBlock>();
                    result.Where(a => a.ChannelId == dv.ChannelId && a.DeviceId == dv.DeviceId && a.DataBlockId == dbId).Delete();

                }
              
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
                using (var connection = TryCreateDatabase())
                {
                    var result = connection.Table<DataBlock>();
                    result.Where(a => a.ChannelId == dv.ChannelId && a.DeviceId == dv.DeviceId && a.DataBlockName == dbName).Delete();

                }
               
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
                List<Tag> list = new DBTagBLL().GetAll(db);

                using (var connection = TryCreateDatabase())
                {
                    var result = connection.Table<DataBlock>();
                    result.Where(a => a.ChannelId == dv.ChannelId && a.DeviceId == dv.DeviceId && a.DataBlockId == db.DataBlockId).Delete();

                }
              
                foreach (Tag item in list)
                {
                    new DBTagBLL().DeleteByObject(db, item);
                }
            }
            catch (Exception ex)
            {

                EventscadaException?.Invoke(GetType().Name, ex.Message);
            }

           
        }

        internal List<DataBlock> GetAll(Device dv)
        {
            List<DataBlock> dataBlocks = new List<DataBlock>();
            DataBlock dataBlock = new DataBlock();
            try
            {
                using (var connection = TryCreateDatabase())
                {
                    dataBlocks = connection.Table<DataBlock>().Where(x => x.ChannelId == dv.ChannelId && x.DeviceId == dv.DeviceId).ToList();
                    dataBlock.Tags = new DBTagBLL().GetAll(dataBlock);
                  
                }
               

            }
            catch (Exception ex)
            {

                EventscadaException?.Invoke(GetType().Name, ex.Message);
            }

            
            return dataBlocks;
        }

        public List<DataBlock> GetAll(XmlNode dvNode)
        {
            List<DataBlock> dataBlocks = new List<DataBlock>();
            DataBlock dataBlock = new DataBlock();
            try
            {
                using (var connection = TryCreateDatabase())
                {
                    dataBlocks = connection.Table<DataBlock>().ToList();
                    dataBlock.Tags = new DBTagBLL().GetAll(dataBlock);
                 
                }
               
            }
            catch (Exception ex)
            {

                EventscadaException?.Invoke(GetType().Name, ex.Message);
            }

            
            return dataBlocks;
        }

        public DataBlock GetById(Device ch, int dbId)
        {
            DataBlock dataBlock = new DataBlock();
            try
            {
                using (var connection = TryCreateDatabase())
                {
                      dataBlock = connection.Table<DataBlock>().Where(x =>  x.ChannelId==ch.ChannelId && x.DeviceId==ch.DeviceId && x.DataBlockId == dbId).FirstOrDefault();
                        dataBlock.Tags = new DBTagBLL().GetAll(dataBlock);
               
                }
              

            }
            catch (Exception ex)
            {

                EventscadaException?.Invoke(GetType().Name, ex.Message);
            }

           
            return dataBlock;
        }

        public DataBlock GetByName(Device ch, string dbName)
        {
            DataBlock dataBlock = new DataBlock();
            try
            {
                using (var connection = TryCreateDatabase())
                {
                        dataBlock = connection.Table<DataBlock>().Where(x => x.ChannelId == ch.ChannelId && x.DeviceId == ch.DeviceId && x.DataBlockName == dbName).FirstOrDefault();
                        dataBlock.Tags = new DBTagBLL().GetAll(dataBlock);

                }
               

                dataBlock.Tags = new DBTagBLL().GetAll(dataBlock);


            }
            catch (Exception ex)
            {

                EventscadaException?.Invoke(GetType().Name, ex.Message);
            }

            
            return dataBlock;
        }

        public DataBlock IsExisted(Device dv, DataBlock db)
        {
            return db;
        }

        public void Update(Device dv, DataBlock db)
        {
            try
            {

                using (var connection = TryCreateDatabase())
                {
                    var dvr = connection.Table<DataBlock>().Where(a => a.ChannelId == dv.ChannelId && a.DeviceId == dv.DeviceId && a.DataBlockId == db.DataBlockId).FirstOrDefault();
                    
                    dvr.ChannelId = dv.ChannelId;
                    dvr.DeviceId = dv.DeviceId;
                    dvr.DataBlockId = db.DataBlockId;
                    dvr.DataBlockName = db.DataBlockName;
                    dvr.StartAddress = db.StartAddress;
                    dvr.Length = db.Length;
                    dvr.DataType = db.DataType;
                    dvr.MemoryType = db.MemoryType;
                    dvr.Function = db.Function;
                    dvr.IsArray = db.IsArray;
                    dvr.Description = dv.Description;

                    connection.Update(dvr
                     , typeof(DataBlock));
                }
                
            }
            catch (Exception ex)
            {

                EventscadaException?.Invoke(GetType().Name, ex.Message);
            }

            
        }

        public void Add(Device dv, DataBlock db)
        {
            try
            {
                using (var connection = TryCreateDatabase())
                {
                    connection.Insert(new DataBlock
                    {
                        ChannelId = dv.ChannelId,
                        DeviceId = dv.DeviceId,
                        DataBlockId = db.DataBlockId,
                        DataBlockName = db.DataBlockName,
                        StartAddress = db.StartAddress,
                        Length = db.Length,
                        DataType = db.DataType,
                        MemoryType = db.MemoryType,
                        Function = db.Function,
                        IsArray = db.IsArray,
                        Description = dv.Description


                    }, typeof(DataBlock));
                }
              
            }
            catch (Exception ex)
            {

                EventscadaException?.Invoke(GetType().Name, ex.Message);
            }

            
        }

    }
}
