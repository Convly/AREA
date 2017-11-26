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
        /// <param name="data_"></param>
        public GetAvailableServicesEvent(HttpEventSource source_, HttpEventType type_, NetTools.User owner_, Object data_)
            : base(source_, type_, owner_, data_)
        {

        }
    }

    /// <summary>
    /// Event add tree
    /// </summary>
    public class AddTreeEvent : Event
    {
        /// <summary>
        /// Event add tree
        /// </summary>
        /// <param name="source_"></param>
        /// <param name="type_"></param>
        /// <param name="owner_"></param>
        /// <param name="data_"></param>
        public AddTreeEvent(HttpEventSource source_, HttpEventType type_, NetTools.User owner_, Object data_)
            : base(source_, type_, owner_, data_)
        {

        }
    }

    /// <summary>
    /// Event add token
    /// </summary>
    public class AddTokensAccessEvent : Event
    {
        /// <summary>
        /// Event add tree
        /// </summary>
        /// <param name="source_"></param>
        /// <param name="type_"></param>
        /// <param name="owner_"></param>
        /// <param name="data_"></param>
        public AddTokensAccessEvent(HttpEventSource source_, HttpEventType type_, NetTools.User owner_, Object data_)
            : base(source_, type_, owner_, data_)
        {

        }
    }

    /// <summary>
    /// Event add user
    /// </summary>
    public class AddUserEvent : Event
    {
        /// <summary>
        /// Event add user
        /// </summary>
        /// <param name="source_"></param>
        /// <param name="type_"></param>
        /// <param name="owner_"></param>
        /// <param name="data_"></param>
        public AddUserEvent(HttpEventSource source_, HttpEventType type_, NetTools.User owner_, Object data_)
            : base(source_, type_, owner_, data_)
        {

        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class TriggerReactionEvent : Event
    {
        /// <summary>
        /// Event add user
        /// </summary>
        /// <param name="source_"></param>
        /// <param name="type_"></param>
        /// <param name="owner_"></param>
        /// <param name="data_"></param>
        public TriggerReactionEvent(HttpEventSource source_, HttpEventType type_, NetTools.User owner_, Object data_)
            : base(source_, type_, owner_, data_)
        {

        }
    }
}
