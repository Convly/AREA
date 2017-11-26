using System;
using TwitterService;

namespace Service
{
    class Program
    {
        public static int Callback(Network.NetTools.Packet obj)
        {
            return (Service.Callback(obj));
        }

        static void Main(string[] args)
        {
            try
            {
                Network.Client client = Network.Client.Instance;
                Network.NetTools.IService service = new Service("TwitterApi", new TwitterController());
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
