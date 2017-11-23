using System;
using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Net;
using System.Text.RegularExpressions;

namespace ServerMonitorApplication
{
    public class ConnectionViewModel : ViewModelBase
    {
        #region Public Properties

        /// <summary>
        /// IP Address
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// Port
        /// </summary>
        public string Port { get; set; }

        #endregion

        #region Commands

        /// <summary>
        /// Login view command
        /// </summary>
        public RelayCommand ShowLoginViewCommand { get; private set; }

        #endregion

        #region Private Methods

        private bool CanShowLoginView()
        {
            string patternIPAddress = "^(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)(.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)){3}$";
            string patternPort = "^([0-9]{1,4}|[1-5][0-9]{4}|6[0-4][0-9]{3}|65[0-4][0-9]{2}|655[0-2][0-9]|6553[0-5])$";
            return Regex.IsMatch(IpAddress, patternIPAddress) && Regex.IsMatch(Port, patternPort);
        }

        /// <summary>
        /// Send the new page to display on the main window
        /// </summary>
        private void ShowLoginView()
        {
            if (CanShowLoginView())
            {
                Messenger.Default.Send(new ChangePage(typeof(LoginViewModel)));
            }
        }

        #endregion

        #region Constructor

        public ConnectionViewModel()
        {
            ShowLoginViewCommand = new RelayCommand(ShowLoginView);
        }

        #endregion
    }
}
