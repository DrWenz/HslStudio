using System.Runtime.Serialization;

namespace HslStudio.ClassLibrary.Systems
{
    [DataContract]
    public class IFaultException
    {
        private string _Message;

        public IFaultException(string msg = null)
        {
            Message = msg;
        }

        public IFaultException(int errorCode = 0, string msg = null)
        {
            ErrorCode = errorCode;
            Message = msg;
        }

        [DataMember]
        public int ErrorCode { get; set; }

        [DataMember]
        public string Message
        {
            get => _Message;
            set => _Message = value;
        }
    }
}
