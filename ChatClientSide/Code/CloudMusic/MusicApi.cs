using ChatClientSide.Code.Utility;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace ChatClientSide.Code.CloudMusic
{
    public class MusicApi
    {
        public string? RequestUrl { get; private set; }

        public MusicApi( )
        {
            RequestUrl = UtilityTools.GetAppSetting( "ServerAddress" );
        }

        /// <summary>
        /// 退出应用
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage( "Performance" , "CA1822:Mark members as static" , Justification = "<挂起>" )]
        public void Shutdown( )
        {
            UtilityTools.WriteLine( "退出" );
        }

        /// <summary>
        /// 搜索歌曲
        /// </summary>
        /// <param name="key">歌曲ID</param>
        public void SeachSong( string key )
        {
            //请求的http地址
            string url = RequestUrl + "search?keywords=" + key;

            Stream temp = CloudMusicRequest.CreateGetHttpResponse( url , 0 , null , null ).GetResponseStream();
            using StreamReader reader = new StreamReader( temp , Encoding.UTF8 );
            string result = reader.ReadToEnd( );
            UtilityTools.WriteLine( result );
        }
    }
}
