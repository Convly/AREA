using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace ServerInterface
{
    public class Net
    {
        public int checkConnection = 0;

        #region Singleton
        /// <summary>
        /// A single instance of net object
        /// </summary>
        private static readonly Net instance = new Net();
        /// <summary>
        /// Property
        /// </summary>
        public static Net Instance { get { return instance; } }
        #endregion
        #region Properties
        /// <summary>
        /// Username copy
        /// </summary>
        public static string Username { get; set; }

        #endregion

        #region Callback
        public static int Callback(Network.NetTools.Packet obj)
        {
            Network.MonitorClient.Instance.SendDataToServer(new Network.NetTools.Packet
            {
                Name = Username,
                Key = obj.Key,
                Data = new KeyValuePair<Network.NetTools.PacketCommand, object>(Network.NetTools.PacketCommand.C_UNLOCK, null)
            });
            switch (obj.Data.Key)
            {
                case Network.NetTools.PacketCommand.S_EVENT:
                    DataMessageConverter(obj.Data.Value);
                    break;
                case Network.NetTools.PacketCommand.S_DISCONNECT:
                    App.Current.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Send, new Action(delegate ()
                    {
                        MainWindow.Instance.ContentControl.Content = MainWindow.conntectionInstance;
                    }));
                    break;
                case Network.NetTools.PacketCommand.S_PONG:
                    Net.Instance.checkConnection++;
                    Console.WriteLine("connect");
                    break;
                case Network.NetTools.PacketCommand.S_LOGIN_SUCCESS:
                    Net.Instance.checkConnection++;
                    Console.WriteLine("login");
                    break;
                case Network.NetTools.PacketCommand.ERROR:
                    Net.Instance.checkConnection = 0;
                    break;
            }
            if (Net.Instance.checkConnection >= 2)
            {
                App.Current.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Send, new Action(delegate ()
                {
                    MainWindow.Instance.ContentControl.Content = MainWindow.servicesInstance;
                }));
            }
            return 0;
        }
        #endregion

        #region Public Methods
        public bool Connection(string ipAddress, string port)
        {
            try
            {
                Network.MonitorClient monitorClient = Network.MonitorClient.Instance;
                monitorClient.Start("Monitor", Callback, ipAddress, int.Parse(port));
                monitorClient.SendDataToServer(new Network.NetTools.Packet { Name = null, Key = 0, Data = new KeyValuePair<Network.NetTools.PacketCommand, object>(Network.NetTools.PacketCommand.C_PING, null) });
            }
            catch (Exception ex)
            {
                Console.Error.Write(ex.Message);
                return false;
            }
            return true;
        }

        public bool Login(string username)
        {
            try
            {
                Username = username;
                Network.MonitorClient monitorClient = Network.MonitorClient.Instance;
                monitorClient.SendDataToServer(new Network.NetTools.Packet { Name = Username, Key = 0, Data = new KeyValuePair<Network.NetTools.PacketCommand, object>(Network.NetTools.PacketCommand.C_REGISTER, "root") });
                return true;
            }
            catch (Exception ex)
            {
                Console.Error.Write(ex.Message);
                return false;
            }
        }

        #endregion

        private static void DataMessageConverter(Object data)
        {
            App.Current.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Send, new Action(delegate ()
            {
                Network.NetTools.EventContent obj = JsonConvert.DeserializeObject<Network.NetTools.EventContent>(data.ToString());
                var action = new ServerMessageDesignModel
                {
                    Name = obj.Name,
                    Content = obj.Content,
                    Time = DateTime.Now.ToString("dd-MM-yyyy : HH-mm-ss"),
                };
                ServicesPage.Instance.msgList.Add(action);

                string text = "";

                foreach (var smdm in ServicesPage.Instance.msgList)
                {
                    text += smdm.Name + " : " + smdm.Time + " : " + smdm.Content + "\n";
                }

                ServicesPage.Instance.serverMessage.Text = text;
            }));
        }
    }
}
