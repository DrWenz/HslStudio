using HslStudio.Bll.Drivers.Basic;
using HslStudio.ClassLibrary.Systems;
using HslStudio.ClassLibrary.TagManagers;
using HslCommunication.Core.Net;
using HslCommunication.Enthernet;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using static HslStudio.Com.Drivers.XCollection;


namespace HslStudio.Bll.Drivers.Hsl
{
    public class HslCallback
    {
        private static NetComplexClient complexClient;
        private static bool isClientIni = false;
        private static readonly object obj_lock = new object();
        #region Simplify Client

        private static readonly NetSimplifyClient simplifyClient = new NetSimplifyClient("127.0.0.1", 23457);

        #endregion

        #region Push NetClient

        private static readonly NetPushClient pushClient = new NetPushClient("127.0.0.1", 23467, "A");

        public HslCallback()
        {
        }
        #endregion

        public static void Subscribe(Machine xMachine)
        {
            try
            {
                NetComplexInitialization();

                pushClient.CreatePush(SubscribedDataReturned);
            }

            catch (Exception ex)
            {

                EventscadaException?.Invoke(new HslCallback().GetType().Name, ex.Message);
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


        #region Complex Client



        private static void NetComplexInitialization()
        {
            try
            {
                complexClient = new NetComplexClient
                {
                    EndPointServer = new IPEndPoint(
                    IPAddress.Parse("127.0.0.1"), 23456)
                };
                complexClient.AcceptByte += ComplexClient_AcceptByte;
                complexClient.AcceptString += ComplexClient_AcceptString;
                complexClient.ClientStart();
            }
            catch (Exception ex)
            {

                EventscadaException?.Invoke(new HslCallback().GetType().Name, ex.Message);
            }
        }

        private static void ComplexClient_AcceptString(AppSession session, HslCommunication.NetHandle handle, string data)
        {
            // Triggered when receiving string data from the server
        }

        private static void ComplexClient_AcceptByte(AppSession session, HslCommunication.NetHandle handle, byte[] buffer)
        {
            // Triggered when receiving byte data from the server
            if (handle == 1)
            {

            }
            else if (handle == 2)
            {
                // Initialized data


                isClientIni = true;
            }
        }


        #endregion

        private static void SubscribedDataReturned(NetPushClient client, string content)
        {

            if (isClientIni)
            {

                lock (obj_lock)
                {
                    if (client.KeyWord == "A")
                    {
                        Dictionary<string, Tag> values = JsonConvert.DeserializeObject<Dictionary<string, Tag>>(content);
                        objUpdateTegCollection?.Invoke(values);
                        Dictionary<string, Tag> tagsClient = CallbackHelperBasic.Tags;
                        if (tagsClient == null)
                        {
                            throw new ArgumentNullException(nameof(tagsClient));
                        }

                        foreach (KeyValuePair<string, Tag> author in values)
                        {
                            if (tagsClient.ContainsKey(author.Key))
                            {
                                tagsClient[author.Key].Value = author.Value.Value;
                                tagsClient[author.Key].TimeSpan = author.Value.TimeSpan;
                            }
                        }

                        objUpdateTegCollection?.Invoke(values);
                    }


                }
            }
        }
        public static void Unsubscribe(Machine xMachine)
        {
            try
            {
                pushClient?.ClosePush();
                complexClient?.ClientClose();

                System.Threading.Thread.Sleep(100);
            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(new HslCallback().GetType().Name, ex.Message);
            }

        }

        public static void WriteTag(string tagName, dynamic value)
        {
            Newtonsoft.Json.Linq.JObject json = new Newtonsoft.Json.Linq.JObject
            {
                { "Address", new Newtonsoft.Json.Linq.JValue(tagName) },
                { "Value", new Newtonsoft.Json.Linq.JValue(value) }
            };
            simplifyClient.ReadCustomerFromServer(1, json.ToString());
        }
    }
}
