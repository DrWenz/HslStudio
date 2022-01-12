using HslStudio.ClassLibrary.Systems;
using static HslStudio.Com.Drivers.XCollection;

namespace HslStudio.Com.Utils
{
    public static class RegistryHelper
    {


     

        public static bool IsLocalHost(string hostName)
        {
            bool result = false;
            if (string.IsNullOrEmpty(hostName) || string.IsNullOrWhiteSpace(hostName) || "127.0.0.1".Equals(hostName) || Environment.MachineName.Equals(hostName))
            {
                result = true;
            }
            return result;
        }
        public static void SetAuthenticationRegistry(SettingHelper xAuthentication)
        {
            try
            {
                WriteKey(new string[5] { xAuthentication.HostName, xAuthentication.CommunicationTypes,
                xAuthentication.DatabaseTypes,xAuthentication.ServerName,xAuthentication.Database });

            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke("RegistryHelper", ex.Message);
            }




        }
        public static SettingHelper GetAuthenticationRegistry()
        {
            string[] empty = ReadKey();

            SettingHelper settingHelper = new SettingHelper();
            try
            {
                settingHelper.HostName = empty[0];
                settingHelper.CommunicationTypes = empty[1];
                settingHelper.DatabaseTypes = empty[2];
                settingHelper.ServerName = empty[3];
                settingHelper.Database = empty[4];
            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke("RegistryHelper", ex.Message);
            }

            return settingHelper;
        }
        public static string ReadFile()
        {
            string result = string.Empty;
            try
            {

       
                //string databasePath = ApplicationData.Current.LocalFolder.Path + @"\RegistryHelper.txt";
             
                //if (!File.Exists(databasePath))
                //{
                //    System.IO.File.WriteAllText(databasePath, ScadeDataAccess.ActiveRegistryHelper());

                //}
                // if (File.Exists(databasePath))
                //{
                //    result = databasePath;

                //}
              
            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke("RegistryHelper", ex.Message);
            }

            return result;
        }

        public static string[] ReadKey()
        {
            List<string> empty = new List<string>();
            try
            {
                string[] lines2 = File.ReadAllLines(ReadFile());
                empty.AddRange(lines2);

                return empty.ToArray();
            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke("RegistryHelper", ex.Message);
            }
            return null;
        }

        public static void WriteKey(string[] keyValue)
        {
            try
            {


                using (StreamWriter writer = new StreamWriter(ReadFile(), false))
                {

                    foreach (string item in keyValue)
                    {
                        writer.WriteLine(item);
                    }



                }

            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke("RegistryHelper", ex.Message);
            }
        }
    }
}
