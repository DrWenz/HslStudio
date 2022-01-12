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
    public class DBTagBLL : DBHelperBasic, ITagBLL
    {
        #region Singleton

        // For implementation refer to: http://geekswithblogs.net/BlackRabbitCoder/archive/2010/05/19/c-system.lazylttgt-and-the-singleton-design-pattern.aspx        
        private static readonly Lazy<DBTagBLL> _instance = new Lazy<DBTagBLL>(() => new DBTagBLL());

        public static DBTagBLL Instance => _instance.Value;

        #endregion
        public DBTagBLL()
        {
            InitializeDatabase();
        }

        public void Add(DataBlock db, Tag tg)
        {
            try
            {
                using (var connection = TryCreateDatabase())
                {
                    connection.Insert(new Tag
                    {
                        ChannelId = db.ChannelId,
                        DeviceId = db.DeviceId,
                        DataBlockId = db.DataBlockId,
                        TagId = tg.TagId,
                        TagName = tg.TagName,
                        Address = tg.Address,
                        DataType = tg.DataType,
                        Description = tg.Description
                       


                    }, typeof(Tag));
                }
                
            }
            catch (Exception ex)
            {

                EventscadaException?.Invoke(GetType().Name, ex.Message);
            }

          
        }

        public void DeleteById(DataBlock db, int tgId)
        {
            try
            {
                using (var connection = TryCreateDatabase())
                {
                    var result = connection.Table<Tag>();
                    result.Where(a => a.ChannelId == db.ChannelId && a.DeviceId == db.DeviceId && a.DataBlockId == db.DataBlockId && a.TagId == tgId).Delete();

                }
                
            }
            catch (Exception ex)
            {

                EventscadaException?.Invoke(GetType().Name, ex.Message);
            }

           
        }

        public void DeleteByName(DataBlock db, string tgName)
        {
            try
            {
                using (var connection = TryCreateDatabase())
                {
                    var result = connection.Table<Tag>();
                    result.Where(a => a.ChannelId == db.ChannelId && a.DeviceId == db.DeviceId && a.DataBlockId == db.DataBlockId && a.TagName == tgName).Delete();

                }
                
            }
            catch (Exception ex)
            {

                EventscadaException?.Invoke(GetType().Name, ex.Message);
            }

            
        }

        public void DeleteByObject(DataBlock db, Tag tg)
        {
            try
            {
                using (var connection = TryCreateDatabase())
                {
                    var result = connection.Table<Tag>();
                    result.Where(a => a.ChannelId == db.ChannelId && a.DeviceId == db.DeviceId && a.DataBlockId == db.DataBlockId && a.TagId == tg.TagId).Delete();

                }
              
            }
            catch (Exception ex)
            {

                EventscadaException?.Invoke(GetType().Name, ex.Message);
            }

            
        }

        internal List<Tag> GetAll(DataBlock db)
        {
            List<Tag> tags = new List<Tag>();

            try
            {
                using (var connection = TryCreateDatabase())
                {
                    tags = connection.Table<Tag>().Where(x => x.ChannelId == db.ChannelId && x.DeviceId == db.DeviceId && x.DataBlockId == db.DataBlockId).ToList();
                    

                }
               
            }
            catch (Exception ex)
            {

                EventscadaException?.Invoke(GetType().Name, ex.Message);
            }
           
            return tags;
        }

        public List<Tag> GetAll(XmlNode dbNote)
        {
            List<Tag> tags = new List<Tag>();

            try
            {
                using (var connection = TryCreateDatabase())
                {
                    tags = connection.Table<Tag>().ToList();
                   
                }
                

            }
            catch (Exception ex)
            {

                EventscadaException?.Invoke(GetType().Name, ex.Message);
            }

          
            return tags;
        }

        public Tag GetByAddress(DataBlock db, ushort tgAddress)
        {
            Tag tag = new Tag();
            try
            {
                using (var connection = TryCreateDatabase())
                {
                      tag = connection.Table<Tag>().Where(x => x.ChannelId == db.ChannelId && x.DeviceId == db.DeviceId && x.DataBlockId == db.DataBlockId && x.Address == tgAddress.ToString()).FirstOrDefault();
                  
                }
                

            }
            catch (Exception ex)
            {

                EventscadaException?.Invoke(GetType().Name, ex.Message);
            }

           
            return tag;
        }
        public Tag GetById(DataBlock db, int tgId)
        {
            Tag tag = new Tag();
            try
            {
                using (var connection = TryCreateDatabase())
                {
                 tag = connection.Table<Tag>().Where(x => x.ChannelId == db.ChannelId && x.DeviceId == db.DeviceId && x.DataBlockId == db.DataBlockId && x.TagId == tgId).FirstOrDefault();

                  
                }
               

            }
            catch (Exception ex)
            {

                EventscadaException?.Invoke(GetType().Name, ex.Message);
            }

          
            return tag;
        }

        public Tag GetByName(DataBlock db, string tgName)
        {
            Tag tag = new Tag();
            try
            {
                using (var connection = TryCreateDatabase())
                {
                    tag = connection.Table<Tag>().Where(x => x.ChannelId == db.ChannelId && x.DeviceId == db.DeviceId && x.DataBlockId == db.DataBlockId && x.TagName == tgName).FirstOrDefault();

                   
                }
                

            }
            catch (Exception ex)
            {

                EventscadaException?.Invoke(GetType().Name, ex.Message);
            }

           
            return tag;
        }

        public Tag IsExisted(DataBlock db, Tag tg)
        {
            return tg;
        }

        public void Update(DataBlock db, Tag tg)
        {
            try
            {
                using (var connection = TryCreateDatabase())
                {
                    var dvr = connection.Table<Tag>().Where(a => a.ChannelId == db.ChannelId && a.DeviceId == db.DeviceId && a.DataBlockId == db.DataBlockId && a.TagId == tg.TagId).FirstOrDefault();
                  
                    dvr.ChannelId = db.ChannelId;
                    dvr.DeviceId = db.DeviceId;
                    dvr.DataBlockId = db.DataBlockId;
                    dvr.TagId = tg.TagId;
                    dvr.TagName = tg.TagName;
                    dvr.Address = tg.Address;
                    dvr.DataType = tg.DataType;
                    dvr.Description = tg.Description;
                    connection.Update(dvr
                     , typeof(Tag));
                }
              
            }
            catch (Exception ex)
            {

                EventscadaException?.Invoke(GetType().Name, ex.Message);
            }

          
        }
    }
}
