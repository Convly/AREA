using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network.NetTools
{
    /// <summary>
    /// The content of an Event
    /// </summary>
    public class EventContent
    {
        /// <summary>
        /// The name of the Event
        /// </summary>
        public string Name { get; set;}

        /// <summary>
        /// The content of the Event
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// The type of the Event
        /// </summary>
        public int Type { get; set; }
    }
}
