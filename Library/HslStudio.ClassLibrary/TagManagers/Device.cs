using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using SQLite;

namespace HslStudio.ClassLibrary.TagManagers
{



    [Table("tblDevice")]
    [DataContract]
    [Serializable]
    public class Device : INotifyPropertyChanged, ICloneable
    {
        private int _ChannelId;
        private int _DeviceId;
        private string _DeviceName;
        private string _Description;
        private bool _IsActived;
        private string _FullPath = string.Empty;
        private List<DataBlock> _DataBlocks;
        private Channel _Channel;
        private short _SlaveId;
        private ConnectionState _DeviceState;

        [PrimaryKey, AutoIncrement, NotNullAttribute]
        public int ID { get; set; }

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
        public string DeviceName { get => _DeviceName; set { _DeviceName = value; NotifyPropertyChanged("DeviceName"); } }

        [DataMember]
        [Browsable(true)]
        public short SlaveId { get => _SlaveId; set => _SlaveId = value; }


        [DataMember]
        [Browsable(true)]
        public ConnectionState States { get => _DeviceState; set { _DeviceState = value; NotifyPropertyChanged("States"); } }

        [DataMember]
        [Browsable(true)]
        public string Description { get => _Description; set { _Description = value; NotifyPropertyChanged("Description"); } }

        [DataMember]
        [Browsable(true)]
        public bool IsActive { get => _IsActived; set { _IsActived = value; NotifyPropertyChanged("IsActive"); } }

        [DataMember]
        [Browsable(true)]
        public string FullPath { get => _FullPath; set { _FullPath = value; NotifyPropertyChanged("FullPath"); } }

        [DataMember]
        [Browsable(false)]
        public List<DataBlock> DataBlocks { get => _DataBlocks; set { _DataBlocks = value; NotifyPropertyChanged("DataBlocks"); } }

        public Channel Channel { get => _Channel; set => _Channel = value; }

        public int TagCount
        {
            get
            {
                int result = 0;
                if (_DataBlocks != null)
                {
                    foreach (DataBlock xDataBlock in _DataBlocks)
                    {
                        if (xDataBlock.Tags != null)
                            result += xDataBlock.Tags.Count;
                    }
                }
                return result;
            }
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }


    }
}
