using System;
using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace ServerMonitorApplication
{
    public class ConnectionViewModel : ViewModelBase
    {
        #region Commands

        /// <summary>
        /// Login view command
        /// </summary>
        public RelayCommand ShowLoginViewCommand { get; private set; }

        #endregion

        #region Private Methods

        /// <summary>
        /// Send the new page to display on the main window
        /// </summary>
        private void ShowLoginView()
        {
            Messenger.Default.Send(new ChangePage(typeof(LoginViewModel)));
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
