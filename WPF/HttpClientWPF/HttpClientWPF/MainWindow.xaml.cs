using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using ClientHttp;

namespace HttpClientWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // textBoxMessage.Text=
          
        }

       async private void ButtonSend_Click(object sender, RoutedEventArgs e)
        {
           await ClientHttp.Client.postMessage(textBoxName.Text, textBoxKey.Text, textBoxMessage.Text);
           Thread.Sleep(500);
            textBoxLog.AppendText ("\n"+ClientHttp.Client.UserResponse);
        }


        async private void Button_Click(object sender, RoutedEventArgs e)
        {
           await ClientHttp.Client.getKey(textBoxName.Text);
            Thread.Sleep(500);
            textBoxName.Text = ClientHttp.Client.UserName;
            textBoxKey.Text = ClientHttp.Client.UserKey;
            textBoxLog.AppendText ( "\n"+ClientHttp.Client.UserResponse);
        }
    }
}
