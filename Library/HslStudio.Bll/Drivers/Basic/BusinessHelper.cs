using HslStudio.ClassLibrary.Systems;

namespace HslStudio.Bll.Drivers.Basic
{
    public static class BusinessHelper
    {
        public static int PORT_TCP = 8283;
        public static int PORT_WEB = 8284;
        public static int PORT_WS = 8285;

        // MODE.
        public static bool IS_REMOTE_MODE = false;

        public static string USER_NAME = null;

        public static SettingHelper settingHelper = null;
    }
}
