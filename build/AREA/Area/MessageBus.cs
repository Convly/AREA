using Area.Events;
using System;
using System.Collections.Generic;

namespace Area
{
    /// <summary>
    /// Defines a bus designed to transfer Event between services and a server.
    /// </summary>
    public class MessageBus
    {
        private Queue<Event> eventList = new Queue<Event>();

        /// <summary>
        /// Callback used when a service want to communicate with the server
        /// </summary>
        /// <param name="obj">The <see cref="Network.NetTools.Packet"/> sent by the service</param>
        /// <returns></returns>
        public static int MessageBusCallback(Object obj)
        {
            return 0;
        }
    }
}
