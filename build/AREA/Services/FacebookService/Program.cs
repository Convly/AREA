using Network.NetTools;
using System;
using System.Collections.Generic;

namespace Service
{
    class Program
    {
        public static string api = "GraphAPI";



        static void Main(string[] args)
        {
            try
            {
                Network.Client client = Network.Client.Instance;
                Network.NetTools.IService service = new Service(api, new Controller());
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
