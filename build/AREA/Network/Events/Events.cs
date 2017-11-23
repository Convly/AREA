using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Network.NetTools;

namespace Network.Events
{
    /// <summary>
    /// Event for recuperation of available services
    /// </summary>
    public class GetAvailableServicesEvent : Event
    {
        /// <summary>
        /// Get available services
        /// </summary>
        /// <param name="source_"></param>
        /// <param name="type_"></param>
        /// <param name="owner_"></param>
        public GetAvailableServicesEvent(HttpEventSource source_, HttpEventType type_, NetTools.User owner_)
            : base(source_, type_, owner_)
        {

        }
    }
}
