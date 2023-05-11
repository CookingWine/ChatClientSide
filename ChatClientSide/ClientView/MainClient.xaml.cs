using ChatClientSide.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
            MouseMove += new MouseEventHandler( WindowMouseMoveEvent );
            UserClient ??= new Client( userName );
        }

        private void WindowMouseMoveEvent( object sender , MouseEventArgs e )
        {
            if( e.LeftButton == MouseButtonState.Pressed )
            {
                DragMove( );
            }
        }
    }
}
