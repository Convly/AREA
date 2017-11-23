using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace ServerMonitorApplication
{
    public class LoginViewModel : ViewModelBase
    {
        #region Commands

        /// <summary>
        /// Login view command
        /// </summary>
        public RelayCommand ShowServicesViewCommand { get; private set; }

        #endregion

        #region Private Methods

        /// <summary>
        /// Send the new page to display on the main window
        /// </summary>
        private void ShowServicesView()
        {
            Messenger.Default.Send(new ChangePage(typeof(ServicesViewModel)));
        }

        #endregion

        #region Constructor

        public LoginViewModel()
        {
            ShowServicesViewCommand = new RelayCommand(ShowServicesView);
        }

        #endregion
    }
}
