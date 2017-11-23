using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Windows;

namespace ServerMonitorApplication
{
    public class LoginViewModel : ViewModelBase
    {
        #region Properties

        /// <summary>
        /// Username
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Password unsecured
        /// </summary>
        public string Password { get; set; }

        #endregion

        #region Commands

        /// <summary>
        /// Login view command
        /// </summary>
        public RelayCommand ShowServicesViewCommand { get; private set; }

        #endregion

        #region Private Methods

        /// <summary>
        /// Inputs verfication
        /// </summary>
        /// <returns></returns>
        private bool CanShowServicesView()
        {
            return !string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password);
        }

        /// <summary>
        /// Send the new page to display on the main window
        /// </summary>
        private void ShowServicesView()
        {
            if (CanShowServicesView())
            {
                Messenger.Default.Send(new ChangePage(typeof(ServicesViewModel)));
            }
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
