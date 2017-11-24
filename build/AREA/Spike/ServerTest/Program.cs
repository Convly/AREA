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
            int x = 0;
            while (x < 4)
            {
                Thread.Sleep(5000);
                Console.WriteLine("AH " + x);
                x++;
            }
            server.Dispatcher.Trigger(e);
            server.Dispatcher.Trigger(e);
            server.Dispatcher.Trigger(e);
            Console.ReadKey();
        }
    }
}
