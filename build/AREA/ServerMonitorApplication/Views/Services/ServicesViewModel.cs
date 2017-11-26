using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Threading;

namespace ServerMonitorApplication
{
    /// <summary>
    /// Actions of the <see cref="ServicesView"/>
    /// </summary>
    public class ServicesViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private ObservableCollection<ServerMessageDesignModel> serverMessages;

        public ObservableCollection<ServerMessageDesignModel> ServerMessages
        {
            get
            {
                return serverMessages;
            }
            set
            {
                if (serverMessages == value)
                {
                    return;
                }
                serverMessages = value;
                Console.Error.WriteLine("Begin display");
                /*for (int i = 0; i != serverMessages.Count; ++i)
                {
                    Console.Error.WriteLine(serverMessages[i].Username);
                }*/
                Console.Error.WriteLine("End display");
            }
        }

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public ServicesViewModel()
        {
                ServerMessages = new ObservableCollection<ServerMessageDesignModel>
            {
                new ServerMessageDesignModel
                {
                    Username = "test1",
                    Content = "fuck fuck",
                    Time = "okeoak"
                },
                new ServerMessageDesignModel
                {
                    Username = "test2",
                    Content = "fuck fuck",
                    Time = "okeoak"
                },
                new ServerMessageDesignModel
                {
                    Username = "test3",
                    Content = "fuck fuck",
                    Time = "okeoak"
                },
            };
        }

        #endregion
    }
}
