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
                if (Network.Server.OldServices.Count > 0)
                    RemoveUSelessServicesFromCache();
                if (Network.Server.NewServices.Count > 0)
                    UpdateRegistrationForServices();
                lock (eventList)
                {
                    foreach (var e in eventList)
                    {
                        Console.WriteLine("Bus: About to treat an event of type " + e.GetType());
                        if (e.GetType() == typeof(TriggerReactionEvent))
                        {
                            ServiceReactionContent src = e.Data as ServiceReactionContent;
                            ComputeActionsTrigger(src);
                        }
                    }
                    eventList.Clear();
                }
            }
        }

        private void ComputeActionsTrigger(ServiceReactionContent src)
        {
            foreach (var ATree in Cache.GetAreaTreeList())
            {
                if (ATree.Email == src.User.Email)
                {
                    foreach (var tree in ATree.AreasList)
                    {
                        if (tree.root.data.eventName == src.Name)
                        {
                            foreach (var action in tree.root.children)
                            {
                                ServiceActionContent sac = new ServiceActionContent(action.data.eventName, src.ReactionContent);
                                Packet p = new Packet
                                {
                                    Name = "Server",
                                    Key = 0,
                                    Data = new KeyValuePair<PacketCommand, object>(PacketCommand.ACTION, sac)
                                };
                                
                                Network.Server.Instance.SendMessageBusEventToService(p, action.data.serviceName);
                            }
                        }
                    }
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
            Packet p = new Packet { Name = "Server", Key = 0, Data = new KeyValuePair<PacketCommand, object>(PacketCommand.REACTION_REGISTER, rrc) };
            Network.Server.Instance.SendMessageBusEventToService(p, serviceName);
        }

        public static bool IsServiceInList(List<Service> list, string serviceName)
        {
            foreach (var service in list)
            {
                if (service.Name == serviceName)
                    return true;
            }

            return false;
        }

        public static void RemoveUSelessServicesFromCache()
        {
            lock (Network.Server.OldServices)
            {
                List<string> sl = Network.Server.OldServices;

                foreach (var serviceName in sl)
                {
                    Cache.RemoveService(serviceName);
                }

                Network.Server.OldServices.Clear();
            }
        }

        public static void UpdateRegistrationForServices()
        {
            List<Service> sl = Network.Server.NewServices;
            lock (Network.Server.NewServices)
            {
                foreach (var service in sl)
                {
                    Cache.AddNewService(service);
                }

                foreach (var Atree in Cache.GetAreaTreeList())
                {
                    User user = Cache.GetUserByMail(Atree.Email);
                    foreach (var tree in Atree.AreasList)
                    {
                        if (IsServiceInList(sl, tree.root.data.serviceName))
                            RegisterReactionForUser(user, tree.root.data.serviceName, tree.root.data.eventName);
                    }
                }

                Network.Server.NewServices.Clear();
            }
        }
    }
}
