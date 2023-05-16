using ChatClientSide.Code.Utility;

namespace ChatClientSide.Code.CloudMusic
{
    public class MusicApi
    {
        public string? RequestUrl { get; private set; }

        public MusicApi( )
        {
            RequestUrl = UtilityTools.GetAppSetting( "ServerAddress" );
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage( "Performance" , "CA1822:Mark members as static" , Justification = "<挂起>" )]
        public void Shutdown( )
        {
            UtilityTools.WriteLine( "退出" );
        }
    }
}
