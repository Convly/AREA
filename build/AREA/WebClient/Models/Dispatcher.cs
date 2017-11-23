using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Network.NetTools;
using Network.Events;

namespace WebClient.Models
{
    public static class Dispatcher
    {
        static List<Service> GetAvailableServices(User user)
        {
            Area.Server server = Area.Server.Instance;
            Event e = new GetAvailableServicesEvent(HttpEventSource.EXT, HttpEventType.QUERY, user);
            var answer = server.Dispatcher.Trigger(e);
            if (answer.Status.Code != 200)
            {
                Console.Error.WriteLine("Error: GetAvailableServices => " + answer.Status.Message);
                return null;
            }
            return answer.Data as List<Service>;
        }
    }
}