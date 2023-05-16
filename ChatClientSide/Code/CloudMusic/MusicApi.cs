using ChatClientSide.Code.Utility;

namespace ChatClientSide.Code.CloudMusic
{
    public class MusicApi
    {
        public static MusicApi? Instance { get; private set; }

        public string? RequestUrl { get; private set; }

        public MusicApi( )
        {
            Instance = this;
            RequestUrl = UtilityTools.GetAppSetting( "MusicUrl" );
        }
    }
}
