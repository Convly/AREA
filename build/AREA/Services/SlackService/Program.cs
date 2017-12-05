using System.Collections.Generic;
using Network.NetTools;
using System;
using SlackService;

namespace Service
{
    class Program
    {
        public static string api = "SimpleSlackBot";
        public static bool Register(IService service, bool accessTokenSecurity)
        {
            Dictionary<string, ServiceType> actions = new Dictionary<string, ServiceType>();
            Dictionary<string, ServiceType> reactions = new Dictionary<string, ServiceType>();

            foreach (var action in service.GetActionList())
                actions.Add(action, ServiceType.ACTION);
            foreach (var reaction in service.GetReactionList())
                reactions.Add(reaction, ServiceType.REACTION);

            Network.NetTools.Service s = new Network.NetTools.Service(api, accessTokenSecurity, actions, reactions, null);
            Packet p = new Packet { Name = api, Key = 0, Data = new KeyValuePair<PacketCommand, object>(PacketCommand.C_REGISTER, s) };
            Network.Client.Instance.SendDataToServer(p);
            return true;
        }

        static void Main(string[] args)
        {
            try
            {
                Network.Client client = Network.Client.Instance;
                Network.NetTools.IService service = new Service(api, new SlackController(api));
                client.Start("MessageBus", service.GetCallback(), args[0], int.Parse(args[1]));
                Register(service, false);
            }
            catch (Exception err)
            {
                Console.Error.WriteLine(err.Message);
            }
            Console.ReadKey();
        }
    }
}
