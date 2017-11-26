using Network.Events;
using System;
using System.Collections.Generic;

namespace Area
{
    /// <summary>
    /// Defines a manager used to handle HTTP commands
    /// </summary>
    public class CommandsManager
    {
        private static Dictionary<Type, Func<Event, HttpEventAnswer>> extEventsRoutes =
            new Dictionary<Type, Func<Event, HttpEventAnswer>>
            {
                {typeof(AddUserEvent),  Cache.AddUser},
                {typeof(AddTreeEvent),  Cache.AddTree}
            };
        private EventFactory evtFactory = new EventFactory();

        /// <summary>
        /// <see cref="EventFactory"/> used to manage the <see cref="Event"/>s
        /// </summary>
        public EventFactory EvtFactory { get => evtFactory; set => evtFactory = value; }

        /// <summary>
        /// Main entry point to process an <see cref="Event"/> in the server
        /// </summary>
        /// <param name="e">The event to be processed</param>
        /// <returns>An <see cref="HttpEventAnswer"/></returns>
        public static HttpEventAnswer ProcessEvent(Event e)
        {
            switch (e.Source)
            {
                case HttpEventSource.EXT:
                    return ExtEventCallback(e);
                case HttpEventSource.APP:
                    return AutomaticEventCallback(e);
                case HttpEventSource.BUS:
                    break;
                case HttpEventSource.SERVICE:
                    return ServiceEventCallback(e);
                default:
                    break;
            }
            return HttpEventAnswer.Error(e, 500, "Hello from the other side");
        }

        /// <summary>
        /// Callback for <see cref="Event"/> received from a service
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static HttpEventAnswer ServiceEventCallback(Event e)
        {
            return MessageBus.Add(e);
        }

        /// <summary>
        /// Callback for <see cref="Event"/> received from an external client
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static HttpEventAnswer ExtEventCallback(Event e)
        {
            foreach (var route in extEventsRoutes)
            {
                if (route.Key == e.GetType())
                    return route.Value(e);
            }
            return HttpEventAnswer.Error(e, 500, "Unkown event " + e.GetType());
        }

        /// <summary>
        /// Callback for <see cref="Event"/> received from the core app
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static HttpEventAnswer AutomaticEventCallback(Event e)
        {
            return HttpEventAnswer.Error(e, 500, "Hello from AutomaticEventCallback");
        }
    }
}
