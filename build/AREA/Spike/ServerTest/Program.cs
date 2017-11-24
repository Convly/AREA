using Network.Events;
using Network.NetTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Area.Server server = Area.Server.Instance;

            User ui = new User("bob@t.fr", "pwd");
            Event e = new GetAvailableServicesEvent(HttpEventSource.EXT, HttpEventType.COMMAND, ui, null);
            foreach (var x in Enumerable.Range(0, 1001))
            {
                server.Dispatcher.Trigger(e);
            }
            Console.ReadKey();
        }
    }
}
