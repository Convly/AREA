using Caliburn.Micro;
using System.Windows;

namespace ServerMonitorApplication.ViewModels.Pages
{
    /// <summary>
    /// Connection page management
    /// </summary>
    public class ConnectionViewModel : Screen
    {
        #region Private Members

        /// <summary>
        /// IP address for server connection
        /// </summary>
        private string mIpAddress;

        /// <summary>
        /// Port for server connection
        /// </summary>
        private string mPort;

        #endregion

        #region Public Properties

        /// <summary>
        /// IpAddress porperty
        /// </summary>
        public string IpAddress
        {
            get
            {
                return mIpAddress;
            }
            set
            {
                mIpAddress = value;
            }
        }

        /// <summary>
        /// Port property
        /// </summary>
        public string Port
        {
            get
            {
                return mPort;
            }
            set
            {
                mPort = value;
            }
        }

        #endregion

        #region Public Members

        /// <summary>
        /// Validate the connection or not
        /// </summary>
        public void ValidateConnection()
        {
            MessageBox.Show("Ip address : " + mIpAddress + "\nPort : " + mPort);
        }

        #endregion
    }
}
