using System;
using System.Collections.Generic;
using Network.Events;

namespace Area
{
    /// <summary>
    /// Handle new events and route them to 
    /// </summary>
    public class Dispatcher
    {
        private Dictionary<HttpEventType, Func<Event, HttpEventAnswer>> routes = new Dictionary<HttpEventType, Func<Event, HttpEventAnswer>> { };

        /// <summary>
        /// Getter and Setter for the <see cref="Dispatcher"/> routes
        /// </summary>
        public Dictionary<HttpEventType, Func<Event, HttpEventAnswer>> Routes { get => routes; set => routes = value; }

        /// <summary>
        /// Default constructor for <see cref="Dispatcher"/>
        /// </summary>
        public Dispatcher()
        {
            this.Routes.Add(HttpEventType.COMMAND, Area.CommandsManager.ProcessEvent);
            this.Routes.Add(HttpEventType.QUERY, Area.QueriesManager.ProcessEvent);
        }

        /// <summary>
        /// EntryPoint of the Dispatcher which handle and route events
        /// </summary>
        /// <param name="e"><see cref="Event"/> to be routed</param>
        /// <returns>A <see cref="HttpEventAnswer"/> object which define the answer of the server for the event</returns>
        public HttpEventAnswer Trigger(Event e)
        {
            Console.WriteLine("Server application dispatcher triggered for event of type " + e.GetType());

            HttpEventAnswer ans = (this.Routes.TryGetValue(e.Type, out Func<Event, HttpEventAnswer> route))
                ? this.Routes[e.Type](e)
                : HttpEventAnswer.Error(e, 400, "Unknown type '" + e.Type + "'");

            string msg = e.GetType() + ": " + ans.Status.Code + " (" + ans.Status.Message + ")";
            //AddMonitorEventMessage();
            return ans;
        }

        public static void AddMonitorEventMessage(string name, int source, string content)
        {
            Network.Server.EventFlow.Add(new KeyValuePair<string, KeyValuePair<int, string>>
                (
                    name, new KeyValuePair<int, string>(source, content))
                );
        }
    }
}
