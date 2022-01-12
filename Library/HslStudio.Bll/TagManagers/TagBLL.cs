using HslStudio.ClassLibrary.Interface;
using HslStudio.ClassLibrary.TagManagers;
using System;
using System.Collections.Generic;
using System.Xml;
using static HslStudio.Com.Drivers.XCollection;
namespace HslStudio.Bll.TagManagers
{

    public class TagBLL : ITagBLL
    {
        public const string CHANNEL_ID = "ChannelId";
        public const string DEVICE_ID = "DeviceId";
        public const string DATABLOCK_ID = "DataBlockId";
        public const string TAG = "Tag";
        public const string TAG_ID = "TagId";
        public const string TAG_NAME = "TagName";
        public const string ADDRESS = "Address";
        public const string DATA_TYPE = "DataType";


        #region Singleton

        // For implementation refer to: http://geekswithblogs.net/BlackRabbitCoder/archive/2010/05/19/c-system.lazylttgt-and-the-singleton-design-pattern.aspx        
        private static readonly Lazy<TagBLL> _instance = new Lazy<TagBLL>(() => new TagBLL());

        public static TagBLL Instance => _instance.Value;

        #endregion

        public void Add(DataBlock db, Tag tg)
        {
            try
            {
                if (tg == null)
                {
                    throw new NullReferenceException("The Tag is null reference exception");
                }

                IsExisted(db, tg);
                db.Tags.Add(tg);
            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(GetType().Name, ex.Message);
            }
        }


        public void Update(DataBlock db, Tag tg)
        {
            try
            {
                if (tg == null)
                {
                    throw new NullReferenceException("The Tag is null reference exception");
                }

                IsExisted(db, tg);
                foreach (Tag item in db.Tags)
                {
                    if (item.TagId == tg.TagId)
                    {
                        item.TagId = tg.TagId;
                        item.TagName = tg.TagName;
                        item.Address = tg.Address;
                        item.DataType = tg.DataType;

                        break;
                    }
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
                Tag result = GetById(db, tgId);
                if (result == null)
                {
                    throw new KeyNotFoundException("Tag Id is not found exception");
                }

                db.Tags.Remove(result);
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
                Tag result = GetByName(db, tgName);
                if (result == null)
                {
                    throw new KeyNotFoundException("Tag name is not found exception");
                }

                db.Tags.Remove(result);
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
                if (tg == null)
                {
                    throw new NullReferenceException("The Tag is null reference exception");
                }

                foreach (Tag item in db.Tags)
                {
                    if (item.TagId == tg.TagId)
                    {
                        db.Tags.Remove(item);
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(GetType().Name, ex.Message);
            }
        }


        public Tag IsExisted(DataBlock db, Tag tg)
        {
            Tag result = null;
            try
            {
                foreach (Tag item in db.Tags)
                {
                    if (item.TagId != tg.TagId && item.TagName.Equals(tg.TagName))
                    {
                        throw new InvalidOperationException($"Tag name: '{tg.TagName}' is existed");
                    }

                    if (item.TagId != tg.TagId && item.Address.Equals(tg.Address))
                    {
                        throw new InvalidOperationException($"Tag address: '{tg.Address}' is existed");
                    }
                }
            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(GetType().Name, ex.Message);
            }

            return result;
        }


        public Tag GetById(DataBlock db, int tgId)
        {
            Tag result = null;
            try
            {
                foreach (Tag item in db.Tags)
                {
                    if (item.TagId == tgId)
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


        public Tag GetByName(DataBlock db, string tgName)
        {
            Tag result = null;
            try
            {
                foreach (Tag item in db.Tags)
                {
                    if (item.TagName.Equals(tgName))
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



        public Tag GetByAddress(DataBlock db, ushort tgAddress)
        {
            Tag result = null;
            try
            {
                foreach (Tag item in db.Tags)
                {
                    if (item.Address.Equals(tgAddress))
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


        public List<Tag> GetAll(XmlNode dbNote)
        {
            List<Tag> dbList = new List<Tag>();
            try
            {
                foreach (XmlNode item in dbNote)
                {
                    Tag tg = new Tag
                    {
                        ChannelId = int.Parse(dbNote.Attributes[CHANNEL_ID].Value),
                        DeviceId = int.Parse(dbNote.Attributes[DEVICE_ID].Value),
                        DataBlockId = int.Parse(dbNote.Attributes[DATABLOCK_ID].Value),
                        TagId = int.Parse(item.Attributes[TAG_ID].Value),
                        TagName = item.Attributes[TAG_NAME].Value,
                        Address = item.Attributes[ADDRESS].Value,
                        DataType = (DataTypes)System.Enum.Parse(typeof(DataTypes), string.Format("{0}", item.Attributes[DATA_TYPE].Value)),
                        Description = item.Attributes[ChannelBLL.DESCRIPTION].Value
                    };
                    dbList.Add(tg);
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