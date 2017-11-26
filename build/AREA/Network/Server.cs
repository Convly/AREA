using NetworkCommsDotNet;
using NetworkCommsDotNet.Connections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using Newtonsoft.Json;
using Network.Lock;
using Network.NetTools;

namespace Network
{
    /// <summary>
    /// Defines the network information for a server's client.
    /// </summary>
    public class InfosClient
    {
        /// <summary>
        /// A <see cref="string"/> for the IP of the client
        /// </summary>
        public string   _ip;
        /// <summary>
        /// An <see cref="int"/> for the port of the client
        /// </summary>
        public int      _port;
    }

    /// <summary>
    /// Defines a Server class for the Nexus Network library.
    /// </summary>
    public class Server
    {
        private static Server instance = null;

        /// <summary>
        /// A getter and a setter for the server singleton instance
        /// </summary>
        public static Server Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Server();
                }
                return instance;
            }
            set
            {
                instance = value;
            }
        }

        /// <summary>
        /// A getter and a setter for the stored LockerManager wich will manage all the synchronous network operations
        /// </summary>
        public LockManager Lock_m { get => lock_m; set => lock_m = value; }

        private Server() { }

        /// <summary>
        /// Destructor for the <see cref="Server"/> class. It will shutdwon the connection.
        /// </summary>
        ~Server()
        {
            try
            {
                NetworkComms.Shutdown();
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Callback used for the communication with the monitors
        /// </summary>
        public static Func<NetTools.Packet, int> MonitorRequestCallback;
        /// <summary>
        /// Callback used for the communication with the message bus
        /// </summary>
        public static Func<NetTools.Packet, int> MessageBusNotificationCallback;
        /// <summary>
        /// Server password used for monitors and services registration
        /// </summary>
        public static string SERVER_PASS;
        /// <summary>
        /// List of monitors currently connected to the server
        /// </summary>
        public static List<Service> NewServices = new List<Service>();
        public static Dictionary<string, InfosClient> Monitors = new Dictionary<string, InfosClient>();
        public static Dictionary<string, NetTools.Service> Services = new Dictionary<string, NetTools.Service>();
        public static List<NetTools.EventContent> EventFlow = new List<NetTools.EventContent>();

        private LockManager     lock_m = new LockManager();
        private string          _serverIP;
        private int             _serverPort;

        /// <summary>
        /// Get ip of the pc
        /// </summary>
        /// <returns></returns>
        public IPAddress GetIpAddr()
        {
            foreach (IPAddress addr in Dns.GetHostAddresses(Dns.GetHostName()))
            {
                if (addr.AddressFamily == AddressFamily.InterNetwork)
                {
                    return addr;
                }
            }
            return null;
        }

        /// <summary>
        /// Init and start the server
        /// </summary>
        /// <param name="mrcb">Callback used for monitors requests</param>
        /// <param name="mbncb">Callback used for message bus requests</param>
        /// <param name="pass">Password of the server</param>
        /// <returns></returns>
        public string Start(Func<NetTools.Packet, int> mrcb, Func<NetTools.Packet, int> mbncb, string pass)
        {
            try
            {
                SERVER_PASS = pass;
                MonitorRequestCallback = mrcb;
                MessageBusNotificationCallback = mbncb;
                NetworkComms.AppendGlobalIncomingPacketHandler<string>("Monitor", MonitorRequest);
                NetworkComms.AppendGlobalIncomingPacketHandler<string>("MessageBus", MessageBusNotification);
                _serverIP = GetIpAddr().ToString();
                _serverPort = 9898;
                Connection.StartListening(ConnectionType.TCP, new System.Net.IPEndPoint(IPAddress.Parse(_serverIP), _serverPort));
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
                return null;
            }
            return _serverIP + ":" + _serverPort;
        }

        /// <summary>
        /// Stop the server by closing all packet handlers and stop the request listening. It also delete all the client's infos
        /// </summary>
        public void Stop()
        {
            Console.WriteLine("Server stopped!");
            NetworkComms.RemoveGlobalIncomingPacketHandler<string>("Monitor", MonitorRequest);
            NetworkComms.RemoveGlobalIncomingPacketHandler<string>("MessageBus", MessageBusNotification);
            Connection.StopListening();
        }

        /// <summary>
        /// Remove a monitor from the connected users by it's name
        /// </summary>
        /// <param name="name">The name of the monitor which will be removed</param>
        /// <returns></returns>
        public bool DeleteMonitor(string name)
        {
            try
            {
                Monitors.Remove(name);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Remove a monitor from the connected users by it's name
        /// </summary>
        /// <param name="name">The name of the service which will be removed</param>
        /// <returns></returns>
        public bool DeleteService(string name)
        {
            try
            {
                if (Services.ContainsKey(name))
                    Services.Remove(name);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
                return false;
            }
            return true;
        }

        private void SendDataToMonitorCore(string ip, int port, NetTools.Packet data)
        {
            uint key = this.Lock_m.Add(ip, 1000);
            data.Key = key;

            NetworkComms.SendObject("Monitor", ip, port, JsonConvert.SerializeObject(data));

            this.Lock_m.Lock(key);
        }

        /// <summary>
        /// Send some data (as <see cref="NetTools.Packet"/> to a monitor by its ip and port
        /// </summary>
        /// <param name="ip">IP address of the monitor</param>
        /// <param name="port">Port of the monitor</param>
        /// <param name="data">Data sent to the monitor</param>
        public void SendDataToMonitor(string ip, int port, NetTools.Packet data)
        {
            try
            {
                this.SendDataToMonitorCore(ip, port, data);
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
            }
        }

        public void SendDataToMonitor(NetTools.Packet data)
        {
            foreach (var user in Monitors)
            {
                try
                {
                    this.SendDataToMonitor(user.Key, data);
                }
                catch (Exception err)
                {
                    Console.Error.WriteLine(err.Message);
                }
            }
        }

        /// <summary>
        /// Send some data (as <see cref="NetTools.Packet"/> to a monitor by its name
        /// </summary>
        /// <param name="name">Name of the monitor</param>
        /// <param name="data">Data sent to the monitor</param>
        public void SendDataToMonitor(string name, NetTools.Packet data)
        {
            InfosClient value;

            if (!Monitors.TryGetValue(name, out value))
            {
                throw new Exception("Can't find player " + name + " in the monitor list");
            }

            try
            {
                this.SendDataToMonitor(value._ip, value._port, data);
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
            }
            
        }

        public bool SendMessageBusEventCore(NetTools.Packet data, string ip, int port)
        {
            uint key = this.Lock_m.Add(ip, 1000);
            data.Key = key;

            NetworkComms.SendObject("MessageBus", ip, port, JsonConvert.SerializeObject(data));

            this.Lock_m.Lock(key);
            return true;
        }

        /// <summary>
        /// Send some data to the services through the message bus channel
        /// </summary>
        /// <param name="data">Dqtq sent to the services</param>
        /// <returns></returns>
        public bool SendMessageBusEvent(NetTools.Packet data)
        {
            foreach (var service in Services)
            {
                try
                {
                    SendMessageBusEventCore(data, service.Value.Infos._ip, service.Value.Infos._port);
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e.Message);
                }
            }
            return true;
        }

        public bool SendMessageBusEventToService(NetTools.Packet data, string serviceName)
        {
            try
            {
                NetTools.Service service = Services[serviceName];
                Object data_s = JsonConvert.SerializeObject(data);
                SendMessageBusEventCore(data, service.Infos._ip, service.Infos._port);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
                DeleteService(serviceName);
            }
            return true;
        }

        /// <summary>
        /// Callback called when the messagebus got a new message
        /// </summary>
        /// <param name="header"></param>
        /// <param name="connection"></param>
        /// <param name="data"></param>
        public static void MessageBusNotification(PacketHeader header, Connection connection, string data)
        {
            string clientIP = connection.ConnectionInfo.RemoteEndPoint.ToString().Split(':').First();
            int clientPort = int.Parse(connection.ConnectionInfo.RemoteEndPoint.ToString().Split(':').Last());

            NetTools.Packet dataObject = null;

            try
            {
                dataObject = JsonConvert.DeserializeObject<NetTools.Packet>(data);

                if (dataObject.Data.Key == NetTools.PacketCommand.C_UNLOCK && dataObject.Key != 0)
                {
                    Console.WriteLine("Service: Unlock key " + dataObject.Key);
                    Server.Instance.Lock_m.Unlock(dataObject.Key);
                    return;
                }

                string name = dataObject.Name.ToString();

                if (dataObject.Data.Key == NetTools.PacketCommand.C_REGISTER)
                {
                    if (Services.ContainsKey(name))
                    {
                        Console.WriteLine("Service: Disconnect old service " + name + " (" + clientIP + ":" + clientPort + ")");
                        Server.Instance.SendMessageBusEventToService(new NetTools.Packet { Name = name, Data = new KeyValuePair<NetTools.PacketCommand, object>(NetTools.PacketCommand.S_DISCONNECT, null) }, name);
                        Services.Remove(name);
                    }
                    Console.WriteLine("Service: New connection registered for " + name + " (" + clientIP + ":" + clientPort + ")");

                    Service newService = JsonConvert.DeserializeObject<Service>(dataObject.Data.Value.ToString());
                    newService.Infos = new InfosClient { _ip = clientIP, _port = clientPort };
                    Services.Add(name, newService);
                    NewServices.Add(newService);
                    Server.Instance.SendMessageBusEventToService(new NetTools.Packet { Name = name, Data = new KeyValuePair<NetTools.PacketCommand, object>(NetTools.PacketCommand.S_LOGIN_SUCCESS, null) }, name);
                    return;
                }

                if (!Services.ContainsKey(name))
                {
                    string err = "Invalid command for unregistered connection";

                    Server.Instance.SendMessageBusEventCore(new NetTools.Packet { Name = name, Data = new KeyValuePair<NetTools.PacketCommand, object>(NetTools.PacketCommand.ERROR, err) }, clientIP, clientPort);
                    Console.Error.WriteLine("Error: " + err);
                    return;
                }
                else
                {
                    Console.WriteLine("New request sent from a monitor by " + clientIP + ":" + clientPort);
                    MessageBusNotificationCallback(dataObject);
                }

            }
            catch (Exception err)
            {
                Console.Error.WriteLine(err.Message);
            }
        }

        /// <summary>
        /// Callback called when a monitor send some data
        /// </summary>
        /// <param name="header"></param>
        /// <param name="connection"></param>
        /// <param name="data"></param>
        public static void  MonitorRequest(PacketHeader header, Connection connection, string data)
        {
            string  clientIP = connection.ConnectionInfo.RemoteEndPoint.ToString().Split(':').First();
            int     clientPort = int.Parse(connection.ConnectionInfo.RemoteEndPoint.ToString().Split(':').Last());

            try
            {
                NetTools.Packet dataObject = JsonConvert.DeserializeObject<NetTools.Packet>(data);

                if (dataObject.Data.Key == NetTools.PacketCommand.C_UNLOCK && dataObject.Key != 0)
                {
                    Console.WriteLine("Monitor: Unlock key " + dataObject.Key);
                    Server.Instance.Lock_m.Unlock(dataObject.Key);
                    return;
                }
                else if (dataObject.Data.Key == NetTools.PacketCommand.C_PING)
                {
                    Server.Instance.SendDataToMonitor(clientIP, clientPort, new NetTools.Packet { Name = "Server", Data = new KeyValuePair<NetTools.PacketCommand, object>(NetTools.PacketCommand.S_PONG, null) });
                    return;
                }

                string name = dataObject.Name.ToString();

                if (dataObject.Data.Key == NetTools.PacketCommand.C_REGISTER && dataObject.Data.Value.ToString() == SERVER_PASS)
                {
                    if (Monitors.ContainsKey(name))
                    {
                        Console.WriteLine("Disconnect old client " + name + " (" + clientIP + ":" + clientPort + ")");
                        Server.Instance.SendDataToMonitor(name, new NetTools.Packet { Name = name, Data = new KeyValuePair<NetTools.PacketCommand, object>(NetTools.PacketCommand.S_DISCONNECT, null) });
                        Monitors.Remove(name);
                    }
                    Console.WriteLine("New connection registered for " + name + " (" + clientIP + ":" + clientPort + ")");
                    Monitors.Add(name, new InfosClient { _ip = clientIP, _port = clientPort });
                    Server.Instance.SendDataToMonitor(name, new NetTools.Packet { Name = name, Data = new KeyValuePair<NetTools.PacketCommand, object>(NetTools.PacketCommand.S_LOGIN_SUCCESS, null) });
                    return;
                }

                if (!Monitors.ContainsKey(name))
                {
                    string err = (dataObject.Data.Key == NetTools.PacketCommand.C_REGISTER) ? "Bad password" : "Invalid command for unregistered connection";

                    Server.Instance.SendDataToMonitor(clientIP, clientPort, new NetTools.Packet { Name = name, Data = new KeyValuePair<NetTools.PacketCommand, object>(NetTools.PacketCommand.ERROR, err) });
                    Console.Error.WriteLine("Error: " + err);
                    return;
                }
                else
                {
                    Console.WriteLine("New request sent from a monitor by " + clientIP + ":" + clientPort);
                    MonitorRequestCallback(dataObject);
                }
            }
            catch (Exception err)
            {
                Console.Error.WriteLine(err.Message);
            }
        }


    }
}
