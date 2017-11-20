using Area.Events;

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

        private bool ProcessEvent(Event e)
        {
            return true;
        }
    }
}
