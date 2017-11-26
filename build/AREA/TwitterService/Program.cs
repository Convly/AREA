using System;
using TwitterService;

namespace Service
{
    class Program
    {
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
