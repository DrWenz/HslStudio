using HslStudio.Bll.Drivers.Basic;
using HslStudio.ClassLibrary.Systems;
using HslStudio.ClassLibrary.TagManagers;
using HslStudio.Com.Drivers;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Text;
using static HslStudio.Com.Drivers.XCollection;

namespace HslStudio.Bll.Drivers.Wcf
{
    public class wcfCallback
    {

        private bool PLC_RunState = false;
        private readonly string IPAddress;
        public wcfCallback()
        {


        }
        public wcfCallback(string iPAddress)
        {
            IPAddress = iPAddress;


        }


        private void PLC_Run()
        {
            while (PLC_RunState)
            {
                UpdateCollection();
                UpdateCollectionDataBlock();
                UpdateCollectionDevice();
            }
        }
        public void Subscribe(Machine xMachine)
        {
            try
            {

                lock (xMachine)
                {
                    if (!XCollection.ClientId.ContainsKey(xMachine.MachineId))
                    {

                        XCollection.ClientId.Add(xMachine.MachineId, new wcfCallback());
                        eventLoggingMessage?.Invoke(string.Format("Added Callback Channel: HashCode: {0} |  IPAddress: {1}", xMachine.MachineId, xMachine.IPAddress));
                        EventChannelCount?.Invoke(1, true);

                    }

                }
                PLC_RunState = true;
                Task.Run(new Action(() =>
                {
                    PLC_Run();
                }));


            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(new wcfCallback().GetType().Name, ex.Message);
            }

        }


        public void Unsubscribe(Machine xMachine)
        {
            try
            {
                PLC_RunState = false;
                eventLoggingMessage?.Invoke(string.Format("Removed Callback Channel:HashCode: {0} |  IPAddress: {1}", xMachine.MachineId, xMachine.IPAddress));
                EventChannelCount?.Invoke(1, false);

                if (XCollection.ClientId.ContainsKey(xMachine.MachineId))
                {
                    lock (XCollection.ClientId[xMachine.MachineId])
                    {
                        if (XCollection.ClientId.ContainsKey(xMachine.MachineId))
                        {
                            XCollection.ClientId.Remove(xMachine.MachineId);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(new wcfCallback().GetType().Name, ex.Message);
            }


        }


        public async void WriteTag(string tagName, dynamic Value)
        {
            try
            {
                Uri uri = new Uri($"http://{IPAddress}:8090/DriverService/Driver/WritePLC?TagName={tagName}&ValueTag={Value}");

                string message = string.Empty;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = uri;
                    client.DefaultRequestHeaders
                        .Accept
                        .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var content = new StringContent(string.Empty, Encoding.UTF8);
                    var response = await client.PostAsync(uri, content);
                    response.EnsureSuccessStatusCode();
                    message = await response.Content.ReadAsStringAsync();
                }


            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(new wcfCallback().GetType().Name, ex.Message);

            }


        }

        public async void UpdateCollection()
        {

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response1 = await client.GetAsync($"http://{IPAddress}:8090/DriverService/Driver/GetCollectionTag");
                    response1.EnsureSuccessStatusCode();
                    string result = await response1.Content.ReadAsStringAsync();
                    if (result == string.Empty)
                    {
                        return;
                    }

                    Dictionary<string, Tag> Tags = JArray.Parse(result)
                                           .Select(x => x.ToObject<KeyValuePair<string, Tag>>())
                                             .ToDictionary(x => x.Key, x => x.Value);
                    if (Tags == null)
                    {
                        return;
                    }

                    Dictionary<string, Tag> tagsClient = CallbackHelperBasic.Tags;
                    if (tagsClient == null)
                    {
                        throw new ArgumentNullException(nameof(tagsClient));
                    }

                    foreach (KeyValuePair<string, Tag> author in Tags.Where(author => tagsClient.ContainsKey(author.Key)).Select(author => author))
                    {
                      
                            tagsClient[author.Key].Value = author.Value.Value;
                            tagsClient[author.Key].TimeSpan = author.Value.TimeSpan;
                         
                    }
                    objUpdateTegCollection?.Invoke(tagsClient);

                }
            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(GetType().Name, ex.Message);
            }


        }

        public async void UpdateCollectionDataBlock()
        {

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response1 = await client.GetAsync($"http://{IPAddress}:8090/DriverService/Driver/GetCollectionDataBlock");
                    response1.EnsureSuccessStatusCode();
                    string result = await response1.Content.ReadAsStringAsync();
                    if (result == string.Empty)
                    {
                        return;
                    }

                    Dictionary<string, DataBlock> DataBlocks = JArray.Parse(result)
                                           .Select(x => x.ToObject<KeyValuePair<string, DataBlock>>())
                                             .ToDictionary(x => x.Key, x => x.Value);
                    if (DataBlocks == null)
                    {
                        return;
                    }

                    Dictionary<string, DataBlock> DataBlocksClient = CallbackHelperBasic.DataBlocks;
                    if (DataBlocksClient == null)
                    {
                        throw new ArgumentNullException(nameof(DataBlocksClient));
                    }

                    foreach (KeyValuePair<string, DataBlock> author in DataBlocks.Where(author => DataBlocksClient.ContainsKey(author.Key)).Select(author => author))
                    {
                        DataBlocksClient[author.Key].ChannelId = author.Value.ChannelId;
                        DataBlocksClient[author.Key].DeviceId = author.Value.DeviceId;
                        DataBlocksClient[author.Key].DataBlockId = author.Value.DataBlockId;
                        DataBlocksClient[author.Key].DataBlockName = author.Value.DataBlockName;
                        DataBlocksClient[author.Key].DataType = author.Value.DataType;
                        DataBlocksClient[author.Key].Length = author.Value.Length;
                        DataBlocksClient[author.Key].StartAddress = author.Value.StartAddress;
                        DataBlocksClient[author.Key].MemoryType = author.Value.MemoryType;
                        DataBlocksClient[author.Key].IsActive = author.Value.IsActive;
                        DataBlocksClient[author.Key].IsArray = author.Value.IsArray;
                        DataBlocksClient[author.Key].Tags = author.Value.Tags;
                        List<Tag> list = DataBlocksClient[author.Key].Tags;
                        for (int i = 0; i < list.Count; i++)
                        {
                            list[i].Value = author.Value.Tags[i].Value;
                            list[i].TimeSpan = author.Value.Tags[i].TimeSpan;

                        }
                    }
                    objUpdateDataBlockCollection?.Invoke(DataBlocksClient);
                }
            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(GetType().Name, ex.Message);
            }



        }

        public async void UpdateCollectionDevice()
        {

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response1 = await client.GetAsync($"http://{IPAddress}:8090/DriverService/Driver/GetCollectionDevice");
                    response1.EnsureSuccessStatusCode();
                    string result = await response1.Content.ReadAsStringAsync();
                    if (result == string.Empty)
                    {
                        return;
                    }

                    Dictionary<string, Device> collection = JArray.Parse(result)
                                           .Select(x => x.ToObject<KeyValuePair<string, Device>>())
                                             .ToDictionary(x => x.Key, x => x.Value);
                    if (collection == null)
                    {
                        return;
                    }
                    Dictionary<string, Device> DevicesClient = CallbackHelperBasic.Devices;
                    if (DevicesClient == null)
                    {
                        throw new ArgumentNullException(nameof(DevicesClient));
                    }

                    foreach (KeyValuePair<string, Device> author in collection.Where(author => DevicesClient.ContainsKey(author.Key)).Select(author => author))
                    {
                        DevicesClient[author.Key].ChannelId = author.Value.ChannelId;
                        DevicesClient[author.Key].DeviceId = author.Value.DeviceId;
                        DevicesClient[author.Key].SlaveId = author.Value.SlaveId;
                        DevicesClient[author.Key].DeviceName = author.Value.DeviceName;
                        DevicesClient[author.Key].States = author.Value.States;
                        DevicesClient[author.Key].Description = author.Value.Description;
                        DevicesClient[author.Key].DataBlocks = author.Value.DataBlocks;
                    }
                    objUpdateDeviceCollection?.Invoke(DevicesClient);
                }
            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(GetType().Name, ex.Message);
            }


        }
    }
}
