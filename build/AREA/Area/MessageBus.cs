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

        public MessageBus()
        {
            this.run = true;
            new Thread(() => this.TreatAll()).Start();
        }

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

        public static HttpEventAnswer Add(Event e)
        {
            lock (eventList)
            {
                eventList.Add(e);
            }
            return HttpEventAnswer.Success(e, "Command Sent With Success");
        }
    }
}
