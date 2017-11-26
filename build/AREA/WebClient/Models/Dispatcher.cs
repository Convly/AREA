using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Network.NetTools;
using Network.Events;

namespace WebClient.Models
{
    public static class Dispatcher
    {
        /// <summary>
        /// Get the services who are available
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static List<Service> GetAvailableServices(User user)
        {
            Area.Server server = Area.Server.Instance;
            Event e = new GetAvailableServicesEvent(HttpEventSource.EXT, HttpEventType.QUERY, user, null);
            var answer = server.Dispatcher.Trigger(e);
            if (answer.Status.Code != 200)
            {
                Console.Error.WriteLine("Error: GetAvailableServices => " + answer.Status.Message);
                return null;
            }
            return answer.Data as List<Service>;
        }

        /// <summary>
        /// Add a tokenAccess to server
        /// </summary>
        /// <param name="user">user</param>
        /// <returns>true => success</returns>
        public static bool AddTokensAccess(User user)
        {
            Area.Server server = Area.Server.Instance;
            Event e = new AddTokensAccessEvent(HttpEventSource.EXT, HttpEventType.COMMAND, user, null);
            var answer = server.Dispatcher.Trigger(e);
            if (answer.Status.Code != 200)
            {
                Console.Error.WriteLine("Error: AddTokensAccess => " + answer.Status.Message);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Add a user to server
        /// </summary>
        /// <param name="user">user</param>
        /// <returns>true => success</returns>
        public static bool AddUser(User user)
        {
            Area.Server server = Area.Server.Instance;
            Event e = new AddUserEvent(HttpEventSource.EXT, HttpEventType.COMMAND, user, null);
            var answer = server.Dispatcher.Trigger(e);
            if (answer.Status.Code != 200)
            {
                Console.Error.WriteLine("Error: AddUser => " + answer.Status.Message);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Add a tree to server
        /// </summary>
        /// <param name="user">user</param>
        /// <param name="tree">user's tree</param>
        /// <returns>true => success</returns>
        public static bool AddTree(User user, ATreeRoot tree)
        {
            Area.Server server = Area.Server.Instance;
            Event e = new AddTreeEvent(HttpEventSource.EXT, HttpEventType.COMMAND, user, tree);
            var answer = server.Dispatcher.Trigger(e);
            if (answer.Status.Code != 200)
            {
                Console.Error.WriteLine("Error: AddTree => " + answer.Status.Message);
                return false;
            }
            return true;
        }
    }
}