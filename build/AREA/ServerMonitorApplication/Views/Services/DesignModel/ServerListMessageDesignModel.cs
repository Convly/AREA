using System;
using System.Collections.ObjectModel;

namespace ServerMonitorApplication
{
    public class ServerListMessageDesignModel : ServicesViewModel
    {
        public static ServerListMessageDesignModel Instance => new ServerListMessageDesignModel();

        public void UpdateListMessage(ObservableCollection<ServerMessageDesignModel> m)
        {
            ServerMessages = m;
        }

        public ServerListMessageDesignModel()
        {
            
        }
    }
}
