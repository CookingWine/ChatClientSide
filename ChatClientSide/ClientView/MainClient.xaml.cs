using ChatClientSide.Code;
using ChatClientSide.Code.CloudMusic;
using System.Windows;
using System.Windows.Input;
namespace ChatClientSide.ClientView
{
    /// <summary>
    /// MainClient.xaml 的交互逻辑
    /// </summary>
    public partial class MainClient :Window
    {
        internal Client UserClient { get; private set; }
        internal MusicApi CloudMusic { get; private set; }
        public MainClient( string userName )
        {
            InitializeComponent( );

            #region 
            CloudMusic ??= new MusicApi( );
            UserClient ??= new Client( userName , this );
            #endregion
        }


        /// <summary>
        /// 设置在线用户数量
        /// </summary>
        /// <param name="lineCount"></param>
        public void SetOnLineCount( int lineCount )
        {
            OnLineUserCount.Text = "当前在线数量：" + lineCount;
        }

        #region Private
        ///<summary>拖拽窗口</summary>
        private void Border_MouseDown( object sender , MouseButtonEventArgs e )
        {
            if( e.ChangedButton == MouseButton.Left )
            {
                DragMove( );
            }
        }
        ///<summary>关闭应用</summary>
        private void CloseAppEvent( object sender , RoutedEventArgs e )
        {
            UserClient.Shutdown( );
            CloudMusic.Shutdown( );
            Application.Current.Shutdown( );
        }

        ///<summary>最小化系统托盘</summary>
        private void MinimizeWindow( object sender , RoutedEventArgs e )
        {

        }

        ///<summary>搜索歌曲</summary>
        private void SearchMusic( object sender , RoutedEventArgs e )
        {
            if( string.IsNullOrEmpty( SearchMusicName.Text ) )
            {
                return;
            }
        }
        #endregion
    }
}
