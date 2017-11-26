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

            User ui1 = new User("ui1", "katsuni");
            User ui2 = new User("ui2", "katsuni");
            User ui3 = new User("ui3", "katsuni");
            GetAvailableServicesEvent e = new GetAvailableServicesEvent(HttpEventSource.EXT, HttpEventType.COMMAND, ui1, null);
            Thread.Sleep(15000);
            Console.WriteLine("Go!");
            server.Dispatcher.Trigger(e);
            e.OwnerInfos = ui2;
            server.Dispatcher.Trigger(e);
            e.OwnerInfos = ui3;
            server.Dispatcher.Trigger(e);
            Console.ReadKey();
        }
    }
}
