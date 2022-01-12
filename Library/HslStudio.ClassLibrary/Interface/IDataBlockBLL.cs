using HslStudio.ClassLibrary.TagManagers;
using System.Collections.Generic;
using System.Xml;

namespace HslStudio.ClassLibrary.Interface
{
    public interface IDataBlockBLL
    {
        void Add(Channel ch, Device dv, DataBlock db);
        void Add(Device dv, DataBlock db);
        void DeleteById(Device dv, int dbId);
        void DeleteByName(Device dv, string dbName);
        void DeleteByObject(Device dv, DataBlock db);
        List<DataBlock> GetAll(XmlNode dvNode = null);
        DataBlock GetById(Device ch, int dbId);
        DataBlock GetByName(Device ch, string dbName);
        DataBlock IsExisted(Device dv, DataBlock db);
        void Update(Device dv, DataBlock db);
    }
}