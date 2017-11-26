using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Network.NetTools;
using Network.Events;

namespace Area
{
    public class Cache
    {
        private static List<Service> ServiceList = new List<Service>();
        private static List<User> UserList = new List<User>();
        private static List<AreaTree> TreeList = new List<AreaTree>();

        public static void UpdateServicesList()
        {

        }

        public static void AddNewService(Service service)
        {
            ServiceList.Add(service);
        }

        public static HttpEventAnswer GetAreaTreeList(Event e)
        {
            return new HttpEventAnswer
            {
                Parent = e,
                Status = new HttpEventStatus { Code = 200, Message = "Success" },
                Data = TreeList
            };
        }

        public static List<AreaTree> GetAreaTreeList()
        {
            return TreeList;
        }

        public static HttpEventAnswer GetUserList(Event e)
        {
            return new HttpEventAnswer
            {
                Parent = e,
                Status = new HttpEventStatus { Code = 200, Message = "Success" },
                Data = UserList
            };
        }

        public static List<User> GetUserList()
        {
            return UserList;
        }

        public static HttpEventAnswer GetServiceList(Event e)
        {
            return new HttpEventAnswer
            {
                Parent = e,
                Status = new HttpEventStatus { Code = 200, Message = "Success" },
                Data = ServiceList
            };
        }

        public static List<Service> GetServiceList()
        {
            return ServiceList;
        }

        public static HttpEventAnswer AddUser(Event e)
        {
            User user = null;

            try
            {
                user = e.Data as User;
            }
            catch (Exception err)
            {
                Console.Error.WriteLine(err.Message);
                return HttpEventAnswer.Error(e, 502, "Invalid data argument for event");
            }

            if (user == null)
                return HttpEventAnswer.Error(e, 502, "Invalid data argument for event");

            foreach (var item in UserList)
            {
                if (item.Email == user.Email)
                    return HttpEventAnswer.Error(e, 501, "Can't add " + item.Email + ", user already exist");
            }

            UserList.Add(user);

            return HttpEventAnswer.Success(e, "User successfully added");
        }

        public static HttpEventAnswer AddTree(Event e)
        {
            ATreeRoot tree = null;
            User user = null;

            try
            {
                KeyValuePair<User, ATreeRoot> kvp = (KeyValuePair<User, ATreeRoot>)e.Data;
                tree = kvp.Value;
                user = kvp.Key;
            }
            catch (Exception err)
            {
                Console.Error.WriteLine(err.Message);
                return HttpEventAnswer.Error(e, 502, "Invalid data argument for event");
            }

            MessageBus.RegisterReactionForUser(GetUserByMail(user.Email), tree.root.data.serviceName, tree.root.data.eventName);

            AreaTree atr = new AreaTree(user.Email);
            atr.AreasList = new List<ATreeRoot> { tree };
            TreeList.Add(atr);

            return HttpEventAnswer.Success(e, "Tree successfully added for tree");
        }

        public static User GetUserByMail(string mail)
        {
            foreach (var item in UserList)
            {
                if (item.Email == mail)
                    return item;
            }
            return null;
        }
        public static void RemoveService(string serviceName)
        {
            int idx = 0;
            foreach (var s in ServiceList)
            {
                if (s.Name == serviceName)
                {
                    ServiceList.RemoveAt(idx);
                    return;
                }
                idx++;
            }
        }
    }
}
