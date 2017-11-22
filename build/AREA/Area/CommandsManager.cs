using Network.Events;

namespace Area
{
    /// <summary>
    /// Defines a manager used to handle HTTP commands
    /// </summary>
    public class CommandsManager
    {
        private EventFactory evtFactory = new EventFactory();

        /// <summary>
        /// <see cref="EventFactory"/> used to manage the <see cref="Event"/>s
        /// </summary>
        public EventFactory EvtFactory { get => evtFactory; set => evtFactory = value; }

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

        public static HttpEventAnswer ServiceEventCallback(Event e)
        {
            return HttpEventAnswer.Error(e, 500, "Hello from ServiceEventCallback");
        }

        public static HttpEventAnswer ExtEventCallback(Event e)
        {
            return MessageBus.Add(e);
        }

        public static HttpEventAnswer AutomaticEventCallback(Event e)
        {
            return HttpEventAnswer.Error(e, 500, "Hello from AutomaticEventCallback");
        }
    }
}
