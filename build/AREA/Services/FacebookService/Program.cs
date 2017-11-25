using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookService
{
    class Program
    {
        public static int Callback(Network.NetTools.Packet obj)
        {
            return 0;
        }

        static void Main(string[] args)
        {
            Network.Client client = Network.Client.Instance;

            client.Start("MessageBus", Callback, args[1], int.Parse(args[2]));
        }
    }
}
