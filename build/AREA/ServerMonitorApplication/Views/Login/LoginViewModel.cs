using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Windows;

namespace ServerMonitorApplication
{
    /// <summary>
    /// Actions on the login view
    /// </summary>
    public class LoginViewModel : ViewModelBase
    {
        #region Properties

        /// <summary>
        /// Username
        /// </summary>
        public static string Username { get; set; }

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
        /// Inputs verfications
        /// </summary>
        /// <returns></returns>
        private bool CanShowServicesView()
        {
            bool loged = false;
            loged = !string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password);
            loged = Net.Instance.Login(Username, Password);
            return loged;
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

        /// <summary>
        /// Constructor
        /// Initialize the command <see cref="ShowServicesViewCommand"/> to <see cref="ShowServicesView"/>
        /// </summary>
        public LoginViewModel()
        {
            ShowServicesViewCommand = new RelayCommand(ShowServicesView);
        }

        #endregion
    }
}
