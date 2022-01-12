using HslStudio.ClassLibrary.TagManagers;
using System.Collections.Generic;

namespace HslStudio.ClassLibrary.Interface
{
    public interface IChannelBLL
    {
        List<Channel> Channels { get; set; }
        void Add(Channel ch);
        void DeleteByObject(Channel ch);
        List<Channel> GetAll(string XmlPath = "");
        Channel GetById(int chId);
        Channel GetByName(string chName);
        Channel IsExisted(Channel ch);
        void Update(Channel ch);
        string ReadKey(string xML_NAME_DEFAULT = "");

    }
}