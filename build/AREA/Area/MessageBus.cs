using Network.Events;
using Network.NetTools;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Area
{
    /// <summary>
    /// Defines a bus designed to transfer Event between services and a server.
    /// </summary>
    public class MessageBus
    {
        private bool run = false;
        /// <summary>
        /// The list of event to be processed
        /// </summary>
        public static List<Event> eventList = new List<Event>();

        private void TreatAll()
        {
            while (this.run)
            {
                if (Network.Server.NewServices.Count > 0)
                    UpdateRegistrationForServices();
                lock (eventList)
                {
                    foreach (var e in eventList)
                    {
                        ServiceReactionContent src = e.Data as ServiceReactionContent;
                        if (e.Data)
                        Console.WriteLine("Bus: About to treat an event of type " + e.GetType());
                        //Network.Server.Instance.SendMessageBusEvent(e);
                    }
                    eventList.Clear();
                }
            }
        }

        /// <summary>
        /// Main constructor for the message bus
        /// </summary>
        public MessageBus()
        {
            this.run = true;
            new Thread(() => this.TreatAll()).Start();
        }

        /// <summary>
        /// Destructor for the message bus
        /// </summary>
        ~MessageBus()
        {
            this.run = false;
        }

        /// <summary>
        /// Callback used when a service want to communicate with the server
        /// </summary>
        /// <param name="obj">The <see cref="Network.NetTools.Packet"/> sent by the service</param>
        /// <returns></returns>
        public static int MessageBusCallback(Network.NetTools.Packet obj)
        {
            Event e = JsonConvert.DeserializeObject<Event>(obj.Data.Value.ToString());
            CommandsManager.ProcessEvent(e);
            return 0;
        }

        /// <summary>
        /// Add an event into the message bus
        /// </summary>
        /// <param name="e">The event to be added</param>
        /// <returns>A success answer</returns>
        public static HttpEventAnswer Add(Event e)
        {
            lock (eventList)
            {
                eventList.Add(e);
            }
            string msg = "Command " + e.GetType() + ": Sent With Success";
            var ans = HttpEventAnswer.Success(e, msg);
            Server.AddMonitorEventMessage(e.OwnerInfos.Email, (int)e.Source, e.GetType() + ": " + ans.Status.Code + " (" + ans.Status.Message + ")");
            return ans;
        }

        public static void RegisterReactionForUser(User user, string serviceName, string reactionName)
        {
            ReactionRegisterContent rrc = new ReactionRegisterContent { Owner = user, ReactionName = reactionName, ServiceName = serviceName };
            Packet p = new Packet { Name = "Server", Key = 0, Data = new KeyValuePair<PacketCommand, object>(PacketCommand.S_REGISTER_USER_REACTION, rrc) };
            Network.Server.Instance.SendMessageBusEventToService(p, serviceName);
        }

        public static void UpdateRegistrationForServices()
        {
            List<string> sl = Network.Server.NewServices;

            foreach (var Atree in Cache.GetAreaTreeList())
            {
                User user = Cache.GetUserByMail(Atree.Email);
                foreach (var tree in Atree.AreasList)
                {
                    if (sl.Contains(tree.root.data.serviceName))
                        RegisterReactionForUser(user, tree.root.data.serviceName, tree.root.data.eventName);
                }
            }
        }
    }
}
