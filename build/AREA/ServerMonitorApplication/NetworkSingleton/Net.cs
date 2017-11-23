using System;
using System.Collections.Generic;

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

        #endregion

        #region Callback

        public static int Callback(Network.NetTools.Packet obj)
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
                case Network.NetTools.PacketCommand.ERROR:
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
                monitorClient.SendDataToServer(new Network.NetTools.Packet { Name = Username, Key = 0, Data = new KeyValuePair<Network.NetTools.PacketCommand, object>(Network.NetTools.PacketCommand.C_PING, null) });
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

        #endregion
    }
}
