using ChatClientSide.ClientView;
using System.Windows;
using System.Windows.Input;

namespace ChatClientSide
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow :Window
    {
        public MainWindow( )
        {
            InitializeComponent( );
        }
        ///<summary>拖拽窗口</summary>
        private void Border_MouseDown( object sender , MouseButtonEventArgs e )
        {
            if( e.ChangedButton == MouseButton.Left )
            {
                DragMove( );
            }
        }
        private void UserLogic( object sender , RoutedEventArgs e )
        {
            if( string.IsNullOrEmpty( UserName.Text ) )
            {
                MessageBox.Show( "用户名为空" , "请输入用户名" , MessageBoxButton.OKCancel , MessageBoxImage.Question );
                return;
            }
            MainClient mainClient = new MainClient( UserName.Text );
            Application.Current.MainWindow = mainClient;
            Close( );
            mainClient.Show( );
        }

        private void ShutdownApp( object sender , RoutedEventArgs e )
        {
            Application.Current.Shutdown( );
        }
    }
}
