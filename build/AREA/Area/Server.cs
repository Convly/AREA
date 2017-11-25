using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Area
{
    /// <summary>
    /// Define micro-services Server main features
    /// </summary>
    public class Server
    {
        /// <summary>
        /// Singleton instance for Server
        /// </summary>
        private static Server instance = null;

        /// <summary>
        /// Getter for the singleton instance of the <see cref="Server"/>
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
        }


        /// <summary>
        /// Main constructor for <see cref="Server"/>
        /// </summary>
        private Server()
        {
            this.Dispatcher = new Dispatcher();
            this.Bus = new MessageBus();
            this.Queries = new QueriesManager();
            this.Commands = new CommandsManager();

            this.Start(new string[] { });
        }

        /// <summary>
        /// Bind the standard output of the server to a specific log file
        /// </summary>
        private void SetLog()
        {
            Random rd = new Random();
            string outName = "AREA_Server_" + DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss") + "__" + rd.Next().ToString() + ".txt";
            FileStream filestream = new FileStream(outName, FileMode.Create);
            var streamwriter = new StreamWriter(filestream);
            streamwriter.AutoFlush = true;
            Console.SetOut(streamwriter);
            Console.SetError(streamwriter);
        }

        private void Start(string[] args)
        {
            //this.SetLog();
            string addr = Network.Server.Instance.Start(MonitorCallback, MessageBus.MessageBusCallback, "root");
            Console.WriteLine("Server running on:" + addr);
        }

        /// <summary>
        /// Default destructor for the server
        /// </summary>
        ~Server()
        {
            Network.Server.Instance.Stop();
        }

        public static void AddMonitorEventMessage(string name, int source, string content)
        {
            var item = new Network.NetTools.EventContent
                {
                    Name = name,
                    Content = content,
                    Type = source
                };
            Network.Server.EventFlow.Add(item);
            Network.Server.Instance.SendDataToMonitor(new Network.NetTools.Packet { Name = "Server", Key = 0, Data = new KeyValuePair<Network.NetTools.PacketCommand, object>(Network.NetTools.PacketCommand.S_EVENT, item) });
        }

        /// <summary>
        /// Callback used when a monitor send a request to the server
        /// </summary>
        /// <param name="data">The <see cref="Network.NetTools.Packet"/> data</param>
        public static int MonitorCallback(Network.NetTools.Packet data)
        {
            return 0;
        }

        private Dispatcher dispatcher;
        private EventFactory eventFactory;
        private MessageBus bus;
        private QueriesManager queries;
        private CommandsManager commands;

        /// <summary>
        /// Getter and Setter for <see cref="Dispatcher"/> member variable of <see cref="Server"/>
        /// </summary>
        public Dispatcher Dispatcher { get => dispatcher; set => dispatcher = value; }
        /// <summary>
        /// Getter and Setter for <see cref="EventFactory"/> member variable of <see cref="Server"/>
        /// </summary>
        public EventFactory EventFactory { get => eventFactory; set => eventFactory = value; }
        /// <summary>
        /// Getter and Setter for <see cref="MessageBus"/> member variable of <see cref="Server"/>
        /// </summary>
        public MessageBus Bus { get => bus; set => bus = value; }
        /// <summary>
        /// Getter and Setter for <see cref="QueriesManager"/> member variable of <see cref="Server"/>
        /// </summary>
        public QueriesManager Queries { get => queries; set => queries = value; }
        /// <summary>
        /// Getter and Setter for <see cref="CommandsManager"/> member variable of <see cref="Server"/>
        /// </summary>
        public CommandsManager Commands { get => commands; set => commands = value; }
    }
}
