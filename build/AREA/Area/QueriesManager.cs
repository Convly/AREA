using Network.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Area
{
    /// <summary>
    /// Defines the class which will manage the get queries
    /// </summary>
    public class QueriesManager
    {
        /// <summary>
        /// Routes
        /// </summary>
        public static Dictionary<Type, Func<Event, HttpEventAnswer>> Routes;

        /// <summary>
        /// QueriesManager
        /// </summary>
        public QueriesManager()
        {
            Routes = new Dictionary<Type, Func<Event, HttpEventAnswer>>
            {
                { typeof(Network.Events.GetAvailableServicesEvent), Cache.GetServiceList }
            };
        }

        /// <summary>
        /// ProcessEvent
        /// </summary>
        /// <param name="e">The event to be added</param>
        /// <returns>An <see cref="HttpEventAnswer"/></returns>
        public static HttpEventAnswer ProcessEvent(Event e)
        {
            foreach (var route in Routes)
            {
                if (route.Key == e.GetType())
                    return route.Value(e);
            }
            return HttpEventAnswer.Error(e, 501, "Event not found");
        }
    }
}
