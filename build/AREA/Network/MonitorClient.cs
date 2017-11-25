using NetworkCommsDotNet;
using NetworkCommsDotNet.Connections;
using System;
using Newtonsoft.Json;

namespace Network
{
    /// <summary>
    /// Client class for the Network library.
    /// This class allow the user to start a transmission in order to receive and send requests to a single <see cref="Server"/>.
    /// </summary>
    public class MonitorClient
    {
        private static MonitorClient instance = null;

        /// <summary>
        /// Getter for the singleton instance of a <see cref="MonitorClient"/>
        /// </summary>
        public static MonitorClient Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MonitorClient();
                }
                return instance;
            }
        }

        private MonitorClient() { }

        /// <summary>
        /// Destructor of the <see cref="MonitorClient"/>. When called, it'll shutdown all the transmissions
        /// </summary>
        ~MonitorClient()
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

        private string _serverIP = null;
        private int _serverPort;

        /// <summary>
        /// Static callback method for the server request
        /// </summary>
        public static Func<NetTools.Packet, int> CallBackFct;
        /// <summary>
        /// Static name for the communication channel 
        /// </summary>
        public static string Channel;

        /// <summary>
        /// Launch the ClientNetwork
        /// </summary>
        /// <param name="channel">Name of the channel which will be used for the communication</param>
        /// <param name="callBackFct">Function called when the client receive a message from the server</param>
        /// <param name="serverIP">Ip of the server you want to connect to</param>
        /// <param name="serverPort">Port of the server you want to connect to</param>
        public void Start(string channel, Func<NetTools.Packet, int> callBackFct, string serverIP, int serverPort)
        {
            try
            {
                if (_serverIP == null)
                {
                    _serverIP = serverIP;
                    _serverPort = serverPort;
                    CallBackFct = callBackFct;
                    Channel = channel;
                    NetworkComms.AppendGlobalIncomingPacketHandler<string>(Channel, ServerRequest);
                    Connection.StartListening(ConnectionType.TCP, new System.Net.IPEndPoint(System.Net.IPAddress.Any, 0));
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Send an object to the server (you will minimum need "public string name" in your object
        /// </summary>
        /// <param name="data"></param>
        public void SendDataToServer(Object data)
        {
             NetworkComms.SendObject(Channel, _serverIP, _serverPort, JsonConvert.SerializeObject(data));
        }

        /// <summary>
        /// Trigered function when the server want to communicate with the Monitor
        /// </summary>
        /// <param name="header"></param>
        /// <param name="connection"></param>
        /// <param name="data">Data send by the server</param>
        public static void ServerRequest(PacketHeader header, Connection connection, string data)
        {
            Console.WriteLine("New request received from the server");
            NetTools.Packet dataObject = JsonConvert.DeserializeObject<NetTools.Packet>(data);

            CallBackFct(dataObject);
        }
    }
}
