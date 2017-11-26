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

namespace ServerInterface
{
    /// <summary>
    /// Logique d'interaction pour Connection.xaml
    /// </summary>
    public partial class ConnectionPage : UserControl
    {
        public static ConnectionPage Instance;
        public ConnectionPage()
        {
            InitializeComponent();
            Instance = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Net.Instance.Connection(ip_txtbox.Text, port_txtbox.Text);
            Thread.Sleep(300);
            Net.Instance.Login(name_txtbox.Text);
        }
    }
}
