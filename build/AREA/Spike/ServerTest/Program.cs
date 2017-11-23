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
            Console.ReadKey();
        }
    }
}
