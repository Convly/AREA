using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    class Program
    {
        public static int Callback(Network.NetTools.Packet obj)
        {
            return 0;
        }

        static void Main(string[] args)
        {
            try
            {
                Network.Client client = Network.Client.Instance;
                Network.NetTools.IService service = new Service("GraphApi", new Controller());
                client.Start("MessageBus", service.GetCallback(), args[1], int.Parse(args[2]));
            }
            catch (Exception err)
            {
                Console.Error.WriteLine(err.Message);
            }
            Console.ReadKey();
        }
    }
}
