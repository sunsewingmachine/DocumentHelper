using Microsoft.Win32;

namespace DocumentHelper
{
    public static class Reg
    {
        public static void SaveSetting(string key, string value)
        {
            //RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\" + "E3" + Glob.GetAssemblyShortName());
            //key.SetValue("handimage", handImage);

            var regkey = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\" + "E3" + Glob.GetAssemblyShortName());
            regkey?.SetValue(key, value);
        }

        public static void SaveSetting(string key, int value)
        {
            //RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\" + "E3" + Glob.GetAssemblyShortName());
            //key.SetValue("handimage", handImage);

            var regkey = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\" + "E3" + Glob.GetAssemblyShortName());
            regkey?.SetValue(key, value);
        }


        public static void SaveSetting(string key, bool value)
        {
            var regkey = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\" + "E3" + Glob.GetAssemblyShortName());
            regkey?.SetValue(key, value);
        }

        public static string GetSetting(string key, string value)
        {
            var regKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\" + "E3" + Glob.GetAssemblyShortName());
            if (regKey == null) return value;

            var saved = regKey.GetValue(key, value).ToString();
            return saved;
        }

        public static bool GetSetting(string key, bool value)
        {
            var regKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\" + "E3" + Glob.GetAssemblyShortName());
            if (regKey == null) return value;

            var saved = regKey.GetValue(key, value).ToString();
            return bool.TryParse(saved, out bool x)? x : false;
        }

        public static int GetSetting(string key, int value)
        {
            var regKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\" + "E3" + Glob.GetAssemblyShortName());
            if (regKey == null) return value;

            var saved = regKey.GetValue(key, value).ToString();
            return int.TryParse(saved, out var ret)? ret : 0;
        }

    }
}