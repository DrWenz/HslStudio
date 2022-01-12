using HslStudio.ClassLibrary.TagManagers;
using System.Collections.Generic;
using System.Xml;

namespace HslStudio.ClassLibrary.Interface
{
    public interface ITagBLL
    {
        void Add(DataBlock db, Tag tg);
        void DeleteById(DataBlock db, int tgId);
        void DeleteByName(DataBlock db, string tgName);
        void DeleteByObject(DataBlock db, Tag tg);
        List<Tag> GetAll(XmlNode dbNote = null);
        Tag GetByAddress(DataBlock db, ushort tgAddress);
        Tag GetById(DataBlock db, int tgId);
        Tag GetByName(DataBlock db, string tgName);
        Tag IsExisted(DataBlock db, Tag tg);
        void Update(DataBlock db, Tag tg);
    }
}