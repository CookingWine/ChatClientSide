using System.Configuration;

namespace ChatClientSide.Code.Utility
{
    public class UtilityTools
    {
        public static string GetAppSetting( string key )
        {
            return ConfigurationManager.AppSettings[key] ?? string.Empty;
        }

        public static int GetAppSettingInt( string key )
        {
            return int.Parse( GetAppSetting( key ) );

        }
    }
}
