using Network.Events;
using Network.NetTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServerTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Area.Server server = Area.Server.Instance;

            User ui = new User("bite@sperme.eu", "katsuni");
            GetAvailableServicesEvent e = new GetAvailableServicesEvent(HttpEventSource.EXT, HttpEventType.COMMAND, ui, null);
            Thread.Sleep(15000);
            Console.WriteLine("Go!");
            server.Dispatcher.Trigger(e);
            server.Dispatcher.Trigger(e);
            server.Dispatcher.Trigger(e);
            Console.ReadKey();
        }
    }
}
