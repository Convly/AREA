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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ServerInterface
{
    public class ServerMessageDesignModel
    {
        public string Name;
        public string Content;
        public string Time;
    }

    /// <summary>
    /// Logique d'interaction pour ServicesPage.xaml
    /// </summary>
    public partial class ServicesPage : UserControl
    {
        public static ServicesPage Instance;

        public List<ServerMessageDesignModel> msgList = new List<ServerMessageDesignModel>();

        public ServicesPage()
        {
            InitializeComponent();
            Instance = this;
        }
    }
}
