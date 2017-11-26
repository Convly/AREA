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
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Members
        
        /// <summary>
        /// Connection page instance
        /// </summary>
        public static ConnectionPage conntectionInstance = new ConnectionPage();

        /// <summary>
        /// Services page instance
        /// </summary>
        public static ServicesPage servicesInstance = new ServicesPage();

        /// <summary>
        /// Main window instance
        /// </summary>
        public static MainWindow Instance;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            Instance = this;
            ContentControl.Content = conntectionInstance;
        }

        #endregion
    }
}
