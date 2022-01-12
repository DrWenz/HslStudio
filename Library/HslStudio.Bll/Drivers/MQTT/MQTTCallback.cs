using HslStudio.Bll.Drivers.Basic;
using HslStudio.ClassLibrary.Systems;
using HslStudio.ClassLibrary.TagManagers;
using HslStudio.Com.Drivers;
using HslCommunication;
using HslCommunication.MQTT;
using Newtonsoft.Json;
using System.Net;
using System.Net.Sockets;
using System.Text;
using static HslStudio.Com.Drivers.XCollection;

namespace HslStudio.Bll.Drivers.MQTT
{

    public class MQTTCallback
    {

        public static MqttClient mqttClient;
        private static bool isClientIni = false;
        private static readonly object obj_lock = new object();
        public static void Subscribe(Machine xMachine)
        {

            if (!XCollection.ClientId.ContainsKey(xMachine.MachineId))
            {

                XCollection.ClientId.Add(xMachine.MachineId, new MQTTCallback());

            }
            try
            {

                MqttConnectionOptions options = new MqttConnectionOptions()
                {
                    IpAddress = xMachine.IPAddress,
                    Port = 1883,
                    ClientId = $"{xMachine.MachineId}",
                    KeepAlivePeriod = TimeSpan.FromSeconds(100),
                };
                mqttClient?.ConnectClose();
                mqttClient = new MqttClient(options)
                {
                    LogNet = new HslCommunication.LogNet.LogNetSingle(string.Empty)
                };
                mqttClient.OnMqttMessageReceived += MqttClient_OnMqttMessageReceived;
                mqttClient.OnNetworkError += MqttClient_OnNetworkError;

                OperateResult connect = mqttClient.ConnectServer();

                if (connect.IsSuccess)
                {
                    isClientIni = true;
                }
                else
                {
                    isClientIni = false;
                }
            }

            catch (Exception ex)
            {
                EventscadaException?.Invoke(new MQTTCallback().GetType().Name, ex.Message);
            }

        }

        private static async void MqttClient_OnMqttMessageReceived(MqttClient client, string topic, byte[] payload)
        {
            string msg = Encoding.UTF8.GetString(payload);
            if (isClientIni)
            {
                switch (topic)
                {
                    case "Tag":
                        {
                            Dictionary<string, Tag> TagList = JsonConvert.DeserializeObject<Dictionary<string, Tag>>(msg);


                            Dictionary<string, Tag> tagsClient = CallbackHelperBasic.Tags;
                            if (tagsClient == null)
                            {
                                throw new ArgumentNullException(nameof(tagsClient));
                            }

                            foreach (KeyValuePair<string, Tag> author in TagList)
                            {
                                if (tagsClient.ContainsKey(author.Key))
                                {
                                    
                                        tagsClient[author.Key].Value = author.Value.Value;
                                        tagsClient[author.Key].TimeSpan = author.Value.TimeSpan;
                                     

                                }
                            }

                            objUpdateTegCollection?.Invoke(TagList);


                            break;
                        }

                    case "Device":
                        Dictionary<string, Device> DeviceList = JsonConvert.DeserializeObject<Dictionary<string, Device>>(msg);

                        objUpdateDeviceCollection?.Invoke(DeviceList);
                        break;
                }
            }
        }

        public static void Unsubscribe(Machine xMachine)
        {
            try
            {
                XCollection.ClientId.Clear();
                mqttClient?.ConnectClose();
                System.Threading.Thread.Sleep(100);


            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(new MQTTCallback().GetType().Name, ex.Message);
            }

        }

        private static void MqttClient_OnNetworkError(object sender, EventArgs e)
        {

            if (sender is MqttClient client)
            {

                client.LogNet?.WriteInfo("الشبكة غير طبيعية ، استعد لإعادة الاتصال بعد 10 ثوانٍ。");
                EventscadaException?.Invoke(string.Empty, "الشبكة غير طبيعية ، استعد لإعادة الاتصال بعد 10 ثوانٍ。");
                while (true)
                {

                    System.Threading.Thread.Sleep(10_000);
                    client.LogNet?.WriteInfo("جاهز لإعادة الاتصال بالخادم...");
                    EventscadaException?.Invoke(string.Empty, "جاهز لإعادة الاتصال بالخادم...");
                    OperateResult connect = client.ConnectServer();
                    if (connect.IsSuccess)
                    {

                        client.LogNet?.WriteInfo("تم الاتصال بالخادم بنجاح！");
                        EventscadaException?.Invoke(string.Empty, "تم الاتصال بالخادم بنجاح！");
                        break;
                    }
                    client.LogNet?.WriteInfo("فشل الاتصال ، أعد الاتصال بعد 10 ثوانٍ من التحضير。");
                    EventscadaException?.Invoke(string.Empty, "فشل الاتصال ، أعد الاتصال بعد 10 ثوانٍ من التحضير。");
                }
            }
        }

        public static string GetIPAddress()
        {
            string IPAddress = string.Empty;
            IPAddress[] hostAddresses = Dns.GetHostAddresses(Dns.GetHostName());
            foreach (IPAddress iPAddress in hostAddresses)
            {
                if (iPAddress.AddressFamily == AddressFamily.InterNetwork)
                {
                    IPAddress = $"{iPAddress}";
                    break;
                }
            }
            return IPAddress;
        }
        public static void WriteTag(string tagName, dynamic value)
        {
            Newtonsoft.Json.Linq.JObject json = new Newtonsoft.Json.Linq.JObject
            {
                { "Address", new Newtonsoft.Json.Linq.JValue(tagName) },
                { "Value", new Newtonsoft.Json.Linq.JValue(value) }
            };
            OperateResult send = mqttClient?.PublishMessage(new MqttApplicationMessage()
            {
                QualityOfServiceLevel = MqttQualityOfServiceLevel.AtMostOnce,
                Topic = "A",
                Payload = Encoding.UTF8.GetBytes(json.ToString())
            });

            if (!send.IsSuccess)
            {
                EventscadaException?.Invoke(new MQTTCallback().GetType().Name, "Send Failed:" + send.Message);
            }

        }
    }
}
