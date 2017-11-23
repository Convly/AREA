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
        public static Dictionary<Type, Func<Event, HttpEventAnswer>> Routes;

        public QueriesManager()
        {
            Routes = new Dictionary<Type, Func<Event, HttpEventAnswer>>
            {
                { typeof(Network.Events.GetAvailableServicesEvent), Cache.GetServiceList }
            };
        }

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
