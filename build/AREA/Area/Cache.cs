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

        public static Network.Events.HttpEventAnswer GetServiceList(Event e)
        {
            return new HttpEventAnswer
            {
                Parent = e,
                Status = new HttpEventStatus { Code = 200, Message = "Success" },
                Data = ServiceList
            }; ;
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
            AreaTree tree = null;

            try
            {
                tree = e.Data as AreaTree;
            }
            catch (Exception err)
            {
                Console.Error.WriteLine(err.Message);
                return HttpEventAnswer.Error(e, 502, "Invalid data argument for event");
            }

            foreach (var item in tree.AreasList)
            {
                string mail = tree.Email;
                ANode root = item.root;
            }

            TreeList.Add(tree);

            return HttpEventAnswer.Success(e, "Tree successfully added for tree");
        }
    }
}
