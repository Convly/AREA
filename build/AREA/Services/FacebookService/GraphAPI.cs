using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Network.NetTools;

namespace FacebookService
{
    public class GraphAPI : Network.NetTools.IService
    {
        private static string name = "graphAPI";

        public static void Callback(Packet obj)
        {
            Console.WriteLine("Hello from " + name);
        }

        public string APIName()
        {
            return name;
        }

        public List<string> GetActionList()
        {
            return new List<string>();
        }

        public List<string> GetReactionList()
        {
            return new List<string>();
        }

        public List<User> GetUsersListByAction(string action)
        {
            return new List<User>();
        }

        public List<User> GetUsersListByReaction(string reaction)
        {
            return new List<User>();
        }
    }
}
