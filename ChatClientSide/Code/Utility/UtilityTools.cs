using System;
using System.Configuration;
using System.Diagnostics;
using System.Text.Json;

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

        public static void WriteLine( string message )
        {
            Trace.WriteLine( message );
        }

        public static long GetTimeStamp( bool bflag = false )
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime( 1970 , 1 , 1 , 0 , 0 , 0 , 0 );
            long ret;
            if( bflag )
                ret = Convert.ToInt64( ts.TotalSeconds );
            else
                ret = Convert.ToInt64( ts.TotalMilliseconds );
            return ret;
        }

        /// <summary>
        /// 发送信息
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="code">状态码</param>
        /// <returns></returns>
        public static string SendMessage( long id , string name , string message , int code )
        {
            var ChatInfo = new ChatInformation( )
            {
                UserID = id ,
                UserName = name ,
                CreationTime = DateTime.Now.ToString( ) ,
                Message = message ,
                Code = code
            };
            return JsonSerializer.Serialize( ChatInfo );
        }

        /// <summary>
        /// 获取消息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static ChatInformation? GetChatInformation( string message )
        {
            return JsonSerializer.Deserialize<ChatInformation>( message );
        }

        /// <summary>
        /// 根据GUID获取19位的唯一数字序列
        /// </summary>
        /// <returns></returns>
        public static long GuidToLongID( )
        {
            byte[] buffer = Guid.NewGuid( ).ToByteArray( );
            return BitConverter.ToInt64( buffer , 0 );
        }

    }

    public class ChatInformation
    {
        ///<summary>用户唯一ID</summary>
        public long? UserID { get; set; }

        ///<summary>发送用户</summary>
        public string? UserName { get; set; }

        ///<summary>创建时间</summary>
        public string? CreationTime { get; set; }

        ///<summary>消息</summary>
        public string? Message { get; set; }

        ///<summary>状态码</summary>
        public int Code { get; set; }
    }
}
