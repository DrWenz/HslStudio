using HslStudio.ClassLibrary.TagManagers;
using System.Collections.Generic;
using System.Data;

namespace HslStudio.Com.Utils
{

    public delegate void ChannelCount(int channelCount, bool isNew);
    public delegate void EventLoggingMessage(string message);
    public delegate void ScadaLogger(int _Id, string _logType, string _time, string _message);
    public delegate void ScadaException(string classname, string erorr);
    public delegate void EventConnectionChanged(ConnectionState status);
    public delegate void PvGridSet(object Value, bool Visible);


    public delegate void EventConnectionState(ConnectionState connState, string msg);
    //=====================================================================================
    public delegate void EventSelectedDriversChanged(bool isNew);
    public delegate void EventChannelChanged(Channel ch, bool isNew);
    public delegate void EventDeviceChanged(Device dv, bool isNew);
    public delegate void EventDataBlockChanged(DataBlock db, bool IsNew);
    public delegate void EventTagChanged(Tag tg, bool isNew);
    //=====================================================================================
    public delegate void UpdateTegCollection(Dictionary<string, Tag> Tags);
    public delegate void UpdateDataBlockCollection(Dictionary<string, DataBlock> DataBlocks);
    public delegate void UpdateDeviceCollectionDevice(Dictionary<string, Device> collection);


    public delegate void OnDriverIsReadyChanged(bool xIsReady);
}
