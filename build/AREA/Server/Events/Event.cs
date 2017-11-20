using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Area.Events
{
    public enum HttpEventType
    {
        QUERY = 0,
        COMMAND = 1,
        SYS
    }

    public enum HttpEventSource
    {
        EXT = 0,
        APP,
        BUS,
        SERVICE
    }

    public abstract class Event
    {
        private DateTime creationDate = new DateTime().ToLocalTime();
        private HttpEventType type;
        private HttpEventSource source;
        private UserInfos ownerInfos;

        public DateTime CreationDate { get => creationDate; set => creationDate = value; }
        public HttpEventType Type { get => type; set => type = value; }
        public HttpEventSource Source { get => source; set => source = value; }
        public UserInfos OwnerInfos { get => ownerInfos; set => ownerInfos = value; }

        public Event(HttpEventSource source_, HttpEventType type_, UserInfos owner_)
        {
            this.Source = source_;
            this.Type = type_;
            this.OwnerInfos = owner_;
        }
    }
}
