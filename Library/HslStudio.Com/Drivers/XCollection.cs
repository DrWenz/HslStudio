using HslStudio.ClassLibrary.Systems;
using HslStudio.ClassLibrary.TagManagers;
using HslStudio.Com.Utils;
using System;
using System.Collections.Generic;
namespace HslStudio.Com.Drivers
{
    public static class XCollection
    {
        public static bool ApplicationRUN = false;

        public static List<Channel> Channels = null;

        public static ScadaException EventscadaException;
        public static ScadaLogger EventscadaLogger;
        public static ChannelCount EventChannelCount;
        public static EventLoggingMessage eventLoggingMessage;
        public static Machine CURRENT_MACHINE = null;





        public static PvGridSet EventPvGridChannelGet;


        //=================================================================================

        public static UpdateTegCollection objUpdateTegCollection;
        public static UpdateDataBlockCollection objUpdateDataBlockCollection;
        public static UpdateDeviceCollectionDevice objUpdateDeviceCollection;

        public static Dictionary<string, Tag> Tags { get; set; } = new Dictionary<string, Tag>();
        public static Dictionary<string, Device> Devices { get; set; } = new Dictionary<string, Device>();
        public static Dictionary<string, DataBlock> DataBlocks { get; set; } = new Dictionary<string, DataBlock>();

        public static Dictionary<Guid, object> ClientId = new Dictionary<Guid, object>();


        public static void ClearTag(Device dv)
        {
            foreach (DataBlock db in dv.DataBlocks)
            {
                foreach (Tag tag in db.Tags)
                {
                    switch (tag.DataType)
                    {
                        case DataTypes.Bit:
                            tag.Value = false;
                            break;
                        default:
                            tag.Value = 0;
                            break;
                        case DataTypes.String:
                            tag.Value = string.Empty;
                            break;
                    }
                }
            }
        }
        public static void ClearTag(Tag tag)
        {


            switch (tag.DataType)
            {
                case DataTypes.Bit:
                    tag.Value = false;
                    break;
                default:
                    tag.Value = 0;
                    break;
                case DataTypes.String:
                    tag.Value = string.Empty;
                    break;
            }


        }
    }
}