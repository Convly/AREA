using Caliburn.Micro;
using System.Windows;
using ServerMonitorApplication.ViewModels;

namespace ServerMonitorApplication
{
    /// <summary>
    /// Main window launcher (application created with the Composite Application Library)
    /// </summary>
    public class Bootstrapper : BootstrapperBase
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public Bootstrapper()
        {
            Initialize();
        }

        #endregion

        #region BootstrapperBase Method

        /// <summary>
        /// Startup application main window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<WindowViewModel>();
        }

        #endregion
    }
}
