using ChatClientSide.ClientView;
using ChatClientSide.Code.Utility;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ChatClientSide.Code
{

    ///<summary>消息的状态码</summary>
    public enum EMessageCode
    {
        ///<summary>连接服务器</summary>
        Client = 100,
        ///<summary>断开服务器</summary>
        Close = 101,
        ///<summary>异常</summary>
        Error = 404,
    }

    internal class Client
    {
        private readonly MainClient View;
        private TcpClient? m_TcpClinet;
        private NetworkStream? m_NetworkStream;
        private BinaryReader? m_BinaryReader;
        private BinaryWriter? m_BinaryWriter;
        ///<summary>用户ID</summary>
        internal long UserID { get; }

        ///<summary>用户名</summary>
        public string? UserName { get; private set; }

        ///<summary>是否连接服务器</summary>
        private bool m_IsConnectServer = false;

        ///<summary>是否开始接受信息</summary>
        private bool m_ReceiveInformation = false;

        internal Client( string username , MainClient view )
        {
            View = view;
            UserID = UtilityTools.GuidToLongID( );
            UpdateUserName( username );
            StartConnectServer( );
        }

        /// <summary>
        /// 更新用户名
        /// </summary>
        /// <param name="userName">用户名</param>
        public void UpdateUserName( string userName )
        {
            UserName = userName;
            View.UserName.Text = UserName;
        }

        /// <summary>
        /// 开始连接服务器
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage( "Style" , "IDE0090:使用 \"new(...)\"" , Justification = "<挂起>" )]
        private void StartConnectServer( )
        {
            if( m_IsConnectServer )
            {
                return;
            }
            m_IsConnectServer = true;
            IPHostEntry? remoteHost = Dns.GetHostEntry( UtilityTools.GetAppSetting( "ServerAddress" ) );
            m_TcpClinet = new TcpClient( );
            //TCP连接服务器
            m_TcpClinet.Connect( remoteHost.HostName , UtilityTools.GetAppSettingInt( "ChatPort" ) );
            if( m_TcpClinet != null )
            {
                m_NetworkStream = m_TcpClinet.GetStream( );
                m_BinaryReader = new BinaryReader( m_NetworkStream );
                m_BinaryWriter = new BinaryWriter( m_NetworkStream );
                SendMessages( "连接服务器" , EMessageCode.Client );
                //创建一个线程用来接收信息
                Thread thread = new Thread( ReceiveMessages );
                thread.Start( );
                thread.IsBackground = true;
            }
            else
            {

            }
        }

        /// <summary>
        /// 接收信息
        /// </summary>
        private void ReceiveMessages( )
        {
            while( m_ReceiveInformation )
            {
                try
                {
                    if( m_BinaryReader != null )
                    {
                        //接收到的信息
                        string recMeg = m_BinaryReader.ReadString( );

                        var temp = UtilityTools.GetChatInformation( recMeg );

                        if( temp != null )
                        {
                            //userName不为空表示为其他用户发送信息 为空时表示为服务器发送的信息一般为更新用户在线数量
                            if( !string.IsNullOrEmpty( temp.UserName ) )
                            {
                                AnalyticInformationUser( temp );
                            }
                            else
                            {
                                AnalyticInformationServer( temp );

                            }
                        }
                    }
                }
                catch
                {

                    return;
                }
            }
        }

        /// <summary>
        /// 发送信息
        /// </summary>
        /// <param name="message">消息类型</param>
        /// <param name="code">状态码</param>
        private void SendMessages( string message , EMessageCode code )
        {
            string info = UtilityTools.SendMessage( UserID , UserName , message , (int)code );
            try
            {
                if( m_BinaryWriter != null )
                {
                    m_BinaryWriter.Write( info );
                    m_BinaryWriter.Flush( );

                }
            }
            catch
            {

                return;
            }
        }

        /// <summary>
        /// 解析用户发送的信息
        /// </summary>
        /// <param name="data"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage( "Performance" , "CA1822:Mark members as static" , Justification = "<挂起>" )]
        private void AnalyticInformationUser( ChatInformation data )
        {

        }

        /// <summary>
        /// 解析服务器发送的信息
        /// </summary>
        /// <param name="data"></param>
        private void AnalyticInformationServer( ChatInformation data )
        {
            int count = int.Parse( data.Message );
            View.SetOnLineCount( count );
        }

        /// <summary>
        /// 关闭服务器连接
        /// </summary>
        private void CloseClinet( )
        {
            SendMessages( "断开连接" , EMessageCode.Close );
            m_IsConnectServer = false;
            m_ReceiveInformation = false;
            m_BinaryReader?.Close( );
            m_BinaryWriter?.Close( );
            m_TcpClinet?.Close( );
        }

        ///<summary>退出应用</summary>
        public void Shutdown( )
        {
            CloseClinet( );

            UtilityTools.WriteLine( "断开服务器连接" );
        }
    }
}
