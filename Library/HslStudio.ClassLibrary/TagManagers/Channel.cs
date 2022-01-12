using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using SQLite;
namespace HslStudio.ClassLibrary.TagManagers
{



    [Table("tblChannel")]
    [DataContract]
    [Serializable]
    public class Channel : INotifyPropertyChanged, ICloneable
    {
        private int _ChannelId = 0;
        private string _ChannelName = string.Empty;
        private string _Description = string.Empty;
        private List<Device> _Devices;
        private string _ChannelTypes;
        private string _Model;
        private string _ChannelAddress;
        private string _ConnectionType;
        private bool _IsActive;
        private string _Mode;

        public event PropertyChangedEventHandler PropertyChanged;

        // This method is called by the Set accessor of each property.
        // The CallerMemberName attribute that is applied to the optional propertyName
        // parameter causes the property name of the caller to be substituted as an argument.
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        [PrimaryKey, AutoIncrement, NotNullAttribute]
        public int CHID { get; set; }


        [DataMember]
        [Browsable(true)]
        public int ChannelId { get => _ChannelId; set { _ChannelId = value; NotifyPropertyChanged("ChannelId"); } }

        [DataMember]
        [Browsable(true)]
        public string ChannelName { get => _ChannelName; set { _ChannelName = value; NotifyPropertyChanged("ChannelName"); } }

        [DataMember]
        [Browsable(true)]
        public string ChannelTypes { get => _ChannelTypes; set { _ChannelTypes = value; NotifyPropertyChanged("ChannelTypes"); } }

        [DataMember]
        [Browsable(true)]
        public string ChannelAddress { get => _ChannelAddress; set { _ChannelAddress = value; NotifyPropertyChanged("ChannelAddress"); } }

        [DataMember]
        [Browsable(true)]
        public string Model { get => _Model; set { _Model = value; NotifyPropertyChanged("Model"); } }

        [DataMember]
        [Browsable(true)]
        public string Mode { get => _Mode; set { _Mode = value; NotifyPropertyChanged("Mode"); } }

        [DataMember]
        [Browsable(true)]
        public string ConnectionType { get => _ConnectionType; set { _ConnectionType = value; NotifyPropertyChanged("ConnectionType"); } }

        [DataMember]
        [Browsable(true)]
        public bool IsActive { get => _IsActive; set { _IsActive = value; NotifyPropertyChanged("IsActive"); } }

        [DataMember]
        [Browsable(true)]
        public string Description { get => _Description; set { _Description = value; NotifyPropertyChanged("Description"); } }

        [DataMember]
        [Browsable(false)]
        public List<Device> Devices { get => _Devices; set { _Devices = value; NotifyPropertyChanged("Devices"); } }


        public int TagCount
        {
            get
            {
                int result = 0;
                if (Devices != null)
                {
                    foreach (Device xDevice in Devices)
                    {
                        result += xDevice.TagCount;
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
