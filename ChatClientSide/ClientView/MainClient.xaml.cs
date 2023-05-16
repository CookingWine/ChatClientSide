using ChatClientSide.Code;
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
        
        public MainClient( string userName )
        {
            InitializeComponent( );
            UserName.Text = userName;
            UserClient ??= new Client( userName );
        }

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
            Application.Current.Shutdown( );
        }

        ///<summary>最小化系统托盘</summary>
        private void MinimizeWindow(object sender , RoutedEventArgs e )
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
    }
}
