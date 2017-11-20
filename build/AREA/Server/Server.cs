using System;
using System.Collections.Generic;
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
            this.EventFactory = new EventFactory();
            this.Bus = new MessageBus();
            this.Queries = new QueriesManager();
            this.Commands = new CommandsManager();
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
