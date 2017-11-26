using GalaSoft.MvvmLight.Messaging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace ServerMonitorApplication
{
    public sealed class Net
    {
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

        /// <summary>
        /// Password copy
        /// </summary>
        public string Password { get; set; }

        public ObservableCollection<ServerMessageDesignModel> mServerMessage { get; set; }
        #endregion

        public Net()
        {
            mServerMessage = new ObservableCollection<ServerMessageDesignModel>();
        }

        #region Callback

        public int Callback(Network.NetTools.Packet obj)
        {
            Console.WriteLine("Gotcha");
            Network.MonitorClient.Instance.SendDataToServer(new Network.NetTools.Packet
            {
                Name = Username,
                Key = obj.Key,
                Data = new KeyValuePair<Network.NetTools.PacketCommand, object>(Network.NetTools.PacketCommand.C_UNLOCK, null)
            });

            switch (obj.Data.Key)
            {
                case Network.NetTools.PacketCommand.S_EVENT:
                    DataLoggerConverter(obj.Data.Value);
                    Messenger.Default.Send(new ChangePage(typeof(ServicesViewModel)));
                    break;

                case Network.NetTools.PacketCommand.S_DISCONNECT:
                    Messenger.Default.Send(new ChangePage(typeof(ConnectionViewModel)));
                    break;

                case Network.NetTools.PacketCommand.S_PONG:
                    Messenger.Default.Send(new ChangePage(typeof(LoginViewModel)));
                    break;

                case Network.NetTools.PacketCommand.S_LOGIN_SUCCESS:
                    Messenger.Default.Send(new ChangePage(typeof(ServicesViewModel)));
                    break;

                case Network.NetTools.PacketCommand.ERROR:
                    MessageBox.Show(obj.Data.Value.ToString());
                    break;

                default:
                    break;
            }
            return 0;
        }

        #endregion

        #region Public Methods

        public bool Initialize(string ipAddress, string port)
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

        public bool Login(string username, string password)
        {
            try
            {
                Username = username;
                Password = password;
                Network.MonitorClient monitorClient = Network.MonitorClient.Instance;
                monitorClient.SendDataToServer(new Network.NetTools.Packet { Name = Username, Key = 0, Data = new KeyValuePair<Network.NetTools.PacketCommand, object>(Network.NetTools.PacketCommand.C_REGISTER, Password) });
                return true;
            }
            catch (Exception ex)
            {
                Console.Error.Write(ex.Message);
                return false;

            }
        }

        public void DataLoggerConverter(Object data)
        {
            Network.NetTools.EventContent obj = JsonConvert.DeserializeObject<Network.NetTools.EventContent>(data.ToString());
            var action = new ServerMessageDesignModel
            {
                Username = obj.Name,
                Content = obj.Content,
                Time = DateTime.Now.ToString("dd-MM-yyyy : HH-mm-ss"),
            };
            if (action != null)
            {
                mServerMessage.Add(action);
                ServerListMessageDesignModel.Instance.UpdateListMessage(mServerMessage);
            }
        } 
        #endregion
    }
}
