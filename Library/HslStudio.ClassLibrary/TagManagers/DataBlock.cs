using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using SQLite;
namespace HslStudio.ClassLibrary.TagManagers
{

    [Table("tblDataBlock")]
    [DataContract]
    [Serializable]
    public class DataBlock : INotifyPropertyChanged, ICloneable
    {
        
        [PrimaryKey, AutoIncrement, NotNullAttribute]
        public int ID { get; set; }

        private int _ChannelId;
        private int _DeviceId;
        private int _DataBlockId;
        private string _DataBlockName;
        private string _MemoryType;
        private string _Function;
        private string _Description;
        private List<Tag> _Tags;
        private string _FullPath = string.Empty;
        private bool _IsActive;
        private DataTypes _DataType;
        private ushort _Length;
        private ushort _StartAddress;
        private bool _IsArray;

        public event PropertyChangedEventHandler PropertyChanged;

        // This method is called by the Set accessor of each property.
        // The CallerMemberName attribute that is applied to the optional propertyName
        // parameter causes the property name of the caller to be substituted as an argument.
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        [DataMember]
        [Browsable(true)]
        public int ChannelId { get => _ChannelId; set { _ChannelId = value; NotifyPropertyChanged("ChannelId"); } }

        [DataMember]
        [Browsable(true)]
        public int DeviceId { get => _DeviceId; set { _DeviceId = value; NotifyPropertyChanged("DeviceId"); } }

        [DataMember]
        [Browsable(true)]
        public int DataBlockId { get => _DataBlockId; set { _DataBlockId = value; NotifyPropertyChanged("DataBlockId"); } }

        [DataMember]
        [Browsable(true)]
        public string DataBlockName { get => _DataBlockName; set { _DataBlockName = value; NotifyPropertyChanged("DataBlockName"); } }

        [DataMember]
        [Browsable(true)]
        public DataTypes DataType { get => _DataType; set { _DataType = value; NotifyPropertyChanged("DataType"); } }

        [DataMember]
        [Browsable(true)]
        public ushort Length { get => _Length; set { _Length = value; NotifyPropertyChanged("Length"); } }

        [DataMember]
        [Browsable(true)]
        public ushort StartAddress { get => _StartAddress; set { _StartAddress = value; NotifyPropertyChanged("StartAddress"); } }

        [DataMember]
        [Browsable(true)]
        public string MemoryType { get => _MemoryType; set { _MemoryType = value; NotifyPropertyChanged("MemoryType"); } }

        [DataMember]
        [Browsable(true)]
        public string Function { get => _Function; set { _Function = value; NotifyPropertyChanged("Function"); } }

        [DataMember]
        [Browsable(true)]
        public bool IsArray { get => _IsArray; set { _IsArray = value; NotifyPropertyChanged("IsArray"); } }

        [DataMember]
        [Browsable(true)]
        public bool IsActive { get => _IsActive; set { _IsActive = value; NotifyPropertyChanged("IsActive"); } }

        [DataMember]
        [Browsable(true)]
        public string Description { get => _Description; set { _Description = value; NotifyPropertyChanged("Description"); } }

        [DataMember]
        [Browsable(false)]
        public List<Tag> Tags { get => _Tags; set { _Tags = value; NotifyPropertyChanged("Tags"); } }

        public string FullPath { get => _FullPath; set { _FullPath = value; NotifyPropertyChanged("FullPath"); } }



        public object Clone()
        {
            return this.MemberwiseClone();
        }



    }
}
