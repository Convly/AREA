using System;

namespace Service
{
    class Program
    {
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
