using Network.Events;
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
                lock (eventList)
                {
                    foreach (var e in eventList)
                    {
                        Console.WriteLine("Bus: About to treat an event of type " + e.GetType());
                        Network.Server.Instance.SendMessageBusEvent(e);
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
    }
}
