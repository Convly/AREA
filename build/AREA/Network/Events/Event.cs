using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Network.NetTools;

namespace Network.Events
{
    /// <summary>
    /// Defines the differents type which can be used to create an <see cref="Event"/>
    /// </summary>
    public enum HttpEventType
    {
        /// <summary>
        /// Can be assimilated to a GET method
        /// </summary>
        QUERY = 0,
        /// <summary>
        /// Can be assimilated to a POST method
        /// </summary>
        COMMAND = 1,
        /// <summary>
        /// Used to manipulate integrity of instances
        /// </summary>
        SYS
    }

    /// <summary>
    /// Defines the differents sources which can be used to create an <see cref="Event"/>
    /// </summary>
    public enum HttpEventSource
    {
        /// <summary>
        /// Represent an external client
        /// </summary>
        EXT = 0,
        /// <summary>
        /// Represent an internal source
        /// </summary>
        APP,
        /// <summary>
        /// Event instanciate in the message bus
        /// </summary>
        BUS,
        /// <summary>
        /// Represent an event created in a service
        /// </summary>
        SERVICE
    }

    /// <summary>
    /// Abstract class used to represent the body of an event in the AREA app
    /// </summary>
    public abstract class Event
    {
        private DateTime creationDate = new DateTime().ToLocalTime();
        private HttpEventType type;
        private HttpEventSource source;
        private User ownerInfos;
        private Object data;

        /// <summary>
        /// Defines the date of the creation for the event
        /// </summary>
        public DateTime CreationDate { get => creationDate; set => creationDate = value; }
        /// <summary>
        /// Defines the type of the event
        /// </summary>
        public HttpEventType Type { get => type; set => type = value; }
        /// <summary>
        /// Defines the source type of the event
        /// </summary>
        public HttpEventSource Source { get => source; set => source = value; }
        /// <summary>
        /// Indicate some basics informations about the event's owner
        /// </summary>
        public User OwnerInfos { get => ownerInfos; set => ownerInfos = value; }
        /// <summary>
        /// Indicate some basics informations about the event's owner
        /// </summary>
        public object Data { get => data; set => data = value; }

        /// <summary>
        /// Main constructor for <see cref="Event"/> objects
        /// </summary>
        /// <param name="source_">The source type (<see cref="HttpEventSource"/>)</param>
        /// <param name="type_">The event type (<see cref="HttpEventType"/>)</param>
        /// <param name="owner_">Some basics informations about the event's owner</param>
        /// <param name="data_">Some basics informations about the event's data</param>
        public Event(HttpEventSource source_, HttpEventType type_, User owner_, Object data_)
        {
            this.Source = source_;
            this.Type = type_;
            this.OwnerInfos = owner_;
            this.data = data_;
        }
    }
}
