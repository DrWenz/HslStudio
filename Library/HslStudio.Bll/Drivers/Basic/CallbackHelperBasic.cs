using HslStudio.Bll.TagManagers;
using HslStudio.Bll.TagManagers.DB;
using HslStudio.ClassLibrary.Interface;
using HslStudio.ClassLibrary.TagManagers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static HslStudio.Com.Drivers.XCollection;
namespace HslStudio.Bll.Drivers.Basic
{
    public class CallbackHelperBasic
    {
        public static Dictionary<string, Device> Devices { get; set; } = new Dictionary<string, Device>();
        public static Dictionary<string, Tag> Tags { get; set; } = new Dictionary<string, Tag>();
        public static Dictionary<string, DataBlock> DataBlocks { get; set; } = new Dictionary<string, DataBlock>();
        public static List<Device> DeviceList => Devices.Values.ToList();

        public static Task<bool> LoadTagCollectionAsync(bool xInitialized = false)
        {
            if (!xInitialized)
            {
                return Task.FromResult(false);
            }

            IChannelBLL objChannelBLL;
            switch (BusinessHelper.settingHelper.DatabaseTypes)
            {
                case "SQL": objChannelBLL = DBChannelBLL.Instance; break;
                case "XML": objChannelBLL = ChannelBLL.Instance; break;
                default: objChannelBLL = ChannelBLL.Instance; break;
            }

            try
            {


                if (CallbackHelperBasic.Tags.Count > 1 && objChannelBLL.GetAll(string.Empty) != null)
                {
                    return Task.FromResult(true);
                }

                objChannelBLL.Channels.Clear();
                CallbackHelperBasic.DataBlocks.Clear();
                CallbackHelperBasic.Devices.Clear();
                CallbackHelperBasic.Tags.Clear();

                List<Channel> chList = objChannelBLL.GetAll(string.Empty);
                if (chList == null)
                {
                    return Task.FromResult(false);
                }

                foreach (Channel ch in chList)
                {
                    foreach (Device dv in ch.Devices)
                    {
                        CallbackHelperBasic.Devices.Add($"{ch.ChannelName}.{dv.DeviceName}", dv);
                        foreach (DataBlock db in dv.DataBlocks)
                        {
                            CallbackHelperBasic.DataBlocks.Add($"{ch.ChannelName}.{dv.DeviceName}.{db.DataBlockName}", db);

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
                                CallbackHelperBasic.Tags.Add(
                                    $"{ch.ChannelName}.{dv.DeviceName}.{db.DataBlockName}.{tag.TagName}", tag);
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(new CallbackHelperBasic().GetType().Name, ex.Message);
            }


            return Task.FromResult(true);
        }
    }
}
