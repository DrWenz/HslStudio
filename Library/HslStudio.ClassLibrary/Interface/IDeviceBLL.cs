using HslStudio.ClassLibrary.TagManagers;
using System.Collections.Generic;
using System.Xml;

namespace HslStudio.ClassLibrary.Interface
{
    public interface IDeviceBLL
    {
        void Add(Channel ch, Device dv);
        void DeleteById(Channel ch, int DeviceId);
        void DeleteByName(Channel ch, string dvName);
        void DeleteByObject(Channel ch, Device dv);
        List<Device> GetAll(XmlNode chNode = null);
        Device GetById(Channel ch, int DeviceId);
        Device GetByName(Channel ch, string dvName);
        Device IsExisted(Channel ch, Device dv);
        void Update(Channel ch, Device dv);
    }
}