using Caliburn.Micro;
using ServerMonitorApplication.ViewModels.Pages;
using System.Diagnostics;

namespace ServerMonitorApplication.ViewModels
{
    /// <summary>
    /// Manage the window
    /// </summary>
    public class WindowViewModel : Conductor<object>
    {
        #region Private Members

        /// <summary>
        /// Header label
        /// </summary>
        private string mHeader = "AREA monitor";

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public WindowViewModel()
        {
            ActivateItem(new ConnectionViewModel());
        }

        #endregion

        #region Public Propeties

        /// <summary>
        /// Header property
        /// </summary>
        public string Header
        {
            get
            {
                return mHeader;
            }
            private set
            {
                mHeader = value;
            }
        }

        #endregion
    }
}
