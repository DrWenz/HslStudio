using SQLite;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace HslStudio.ClassLibrary.TagManagers
{

   


    
    public delegate void ValueChanged(dynamic value);
    [Table("tblTag")]
    [DataContract]
    [Serializable]
    public class Tag : INotifyPropertyChanged, ICloneable
    {
        

        [PrimaryKey, AutoIncrement, NotNullAttribute]
        public int ID { get; set; }

        private int _TagId;
        private int _DataBlockId;
        private int _DeviceId;
        private int _ChannelId;
        private string _TagName;
        private string _Address;
        private string _FullPath = string.Empty;
        private dynamic _Value;
        private DataTypes _DataType;
        private string _Description;
        private DateTime _TimeSpan = DateTime.Now;

        public event PropertyChangedEventHandler PropertyChanged;
        public ValueChanged ValueChanged = null;

        // This method is called by the Set accessor of each property.
        // The CallerMemberName attribute that is applied to the optional propertyName
        // parameter causes the property name of the caller to be substituted as an argument.
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        [DataMember]
        [Browsable(true)]
        public int TagId { get => _TagId; set { _TagId = value; NotifyPropertyChanged("TagId"); } }

        [DataMember]
        [Browsable(true)]
        public int DataBlockId { get => _DataBlockId; set { _DataBlockId = value; NotifyPropertyChanged("DataBlockId"); } }

        [DataMember]
        [Browsable(true)]
        public int DeviceId { get => _DeviceId; set { _DeviceId = value; NotifyPropertyChanged("DeviceId"); } }

        [DataMember]
        [Browsable(true)]
        public int ChannelId { get => _ChannelId; set { _ChannelId = value; NotifyPropertyChanged("ChannelId"); } }

        [DataMember]
        [Browsable(true)]
        public string TagName { get => _TagName; set { _TagName = value; NotifyPropertyChanged("TagName"); } }


        [DataMember]
        [Browsable(true)]
        public string Address { get => _Address; set { _Address = value; NotifyPropertyChanged("Address"); } }

        [DataMember]
        [Browsable(true)]
        public string FullPath { get => _FullPath; set { _FullPath = value; NotifyPropertyChanged("FullPath"); } }

        [DataMember]
        [Browsable(true)]
        public DataTypes DataType
        {
            get => _DataType;
            set
            {
                _DataType = value; NotifyPropertyChanged("DataType");
                if (!string.IsNullOrEmpty(this.Value) && !string.IsNullOrWhiteSpace(this.Value))
                {
                    switch (_DataType)
                    {
                        case DataTypes.Bit:
                            this.Value = false;
                            break;
                        case DataTypes.String:
                            this.Value = string.Empty;
                            break;
                        default:
                            this.Value = 0;
                            break;
                    }
                }
            }
        }


        [DataMember]
        [Browsable(true)]
        public string Description { get => _Description; set { _Description = value; NotifyPropertyChanged("Description"); } }

        [DataMember]
        [Browsable(true)]
        public dynamic Value { get => _Value; set { _Value = value; NotifyPropertyChanged("Value"); } }


        [DataMember]
        [Browsable(true)]
        public DateTime TimeSpan { get => _TimeSpan; set { _TimeSpan = value; NotifyPropertyChanged("TimeSpan"); } }


        public object Clone()
        {
            return this.MemberwiseClone();
        }



    }
}
