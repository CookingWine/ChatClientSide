using ChatClientSide.ClientView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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

        public void UserLogic( object sender , RoutedEventArgs e )
        {
            if( string.IsNullOrEmpty( UserNameText.Text ) )
            {
                MessageBox.Show( "用户名为空" , "请输入用户名" , MessageBoxButton.OKCancel , MessageBoxImage.Question );
                return;
            }
            MainClient mainClient = new MainClient( UserNameText.Text );
            Application.Current.MainWindow = mainClient;
            Close( );
            mainClient.Show( );
        }

        private void TextBoxPreviewTextInput( object sender , TextCompositionEventArgs e )
        {
            Regex re = new Regex( "[^0-9]" );
            e.Handled = re.IsMatch( e.Text );
        }
    }
}
