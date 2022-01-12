using System;

namespace HslStudio.Com.Tools
{
    public static class ClassLicense
    {

        public static bool ActiveHslCommunication()
        {
            try
            {
                if (!HslCommunication.Authorization.SetAuthorizationCode("f562cc4c-4772-4b32-bdcd-f3e122c534e3"))
                {
                    //active failed
                    Console.WriteLine("Active Failed! it can only use 8 hours");
                    return false;  // quit if active failed
                }
                else
                {
                    return true;
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
