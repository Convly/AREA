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
    /// <summary>
    /// Design model
    /// </summary>
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
        #region Members

        /// <summary>
        /// Services page instance
        /// </summary>
        public static ServicesPage Instance;

        /// <summary>
        /// Message list
        /// </summary>
        public List<ServerMessageDesignModel> msgList = new List<ServerMessageDesignModel>();

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public ServicesPage()
        {
            InitializeComponent();
            Instance = this;
        }

        #endregion
    }
}
