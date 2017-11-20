using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientTest
{
    class Program
    {
        public static string UserName = "Herbaux";
        public static int Callback(Network.NetTools.Packet obj)
        {
            Console.WriteLine("Gotcha");
            Network.MonitorClient.Instance.SendDataToServer(new Network.NetTools.Packet
            {
                Name = UserName,
                Key = obj.Key,
                Data = new KeyValuePair<Network.NetTools.PacketCommand, object>(Network.NetTools.PacketCommand.C_UNLOCK, null)
            });
            return 0;
        }

        static void Main(string[] args)
        {
            Network.MonitorClient monitorClient = Network.MonitorClient.Instance;
            monitorClient.Start("Monitor", Callback, args[0], int.Parse(args[1]));
            monitorClient.SendDataToServer(new Network.NetTools.Packet { Name = UserName , Key = 0, Data = new KeyValuePair<Network.NetTools.PacketCommand, object>(Network.NetTools.PacketCommand.C_REGISTER, "rootz") });
            Console.ReadKey();
        }
    }
}
