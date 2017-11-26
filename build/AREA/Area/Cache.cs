using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Network.NetTools;
using Network.Events;

namespace Area
{
    /// <summary>
    /// Cache
    /// </summary>
    public class Cache
    {
        private static List<Service> ServiceList = new List<Service>();
        private static List<User> UserList = new List<User>();
        private static List<AreaTree> TreeList = new List<AreaTree>();

        /// <summary>
        /// Update the services list
        /// </summary>
        public static void UpdateServicesList()
        {

        }

        /// <summary>
        /// Add a new <see cref="Service"/> to the service list
        /// </summary>
        /// <param name="service">A <see cref="Service"/></param>
        public static void AddNewService(Service service)
        {
            ServiceList.Add(service);
        }

        /// <summary>
        /// Get the AREA Tree list
        /// </summary>
        /// <param name="e">An <see cref="Event"/></param>
        /// <returns>An <see cref="HttpEventAnswer"/></returns>
        public static HttpEventAnswer GetAreaTreeList(Event e)
        {
            return new HttpEventAnswer
            {
                Parent = e,
                Status = new HttpEventStatus { Code = 200, Message = "Success" },
                Data = TreeList
            };
        }

        /// <summary>
        /// Get the AREA tree list
        /// </summary>
        /// <returns>The AREA tree list</returns>
        public static List<AreaTree> GetAreaTreeList()
        {
            return TreeList;
        }

        /// <summary>
        /// Get the User List
        /// </summary>
        /// <param name="e">An <see cref="Event"/></param>
        /// <returns>An <see cref="HttpEventAnswer"/></returns>
        public static HttpEventAnswer GetUserList(Event e)
        {
            return new HttpEventAnswer
            {
                Parent = e,
                Status = new HttpEventStatus { Code = 200, Message = "Success" },
                Data = UserList
            };
        }

        /// <summary>
        /// Get the User list
        /// </summary>
        /// <returns>A list of <see cref="User"/></returns>
        public static List<User> GetUserList()
        {
            return UserList;
        }

        /// <summary>
        /// Get the service list
        /// </summary>
        /// <param name="e">An <see cref="Event"/></param>
        /// <returns>An <see cref="HttpEventAnswer"/></returns>
        public static HttpEventAnswer GetServiceList(Event e)
        {
            return new HttpEventAnswer
            {
                Parent = e,
                Status = new HttpEventStatus { Code = 200, Message = "Success" },
                Data = ServiceList
            };
        }

        /// <summary>
        /// Get the service list
        /// </summary>
        /// <returns>A list of <see cref="Service"/></returns>
        public static List<Service> GetServiceList()
        {
            return ServiceList;
        }

        /// <summary>
        /// Add an <see cref="User"/> to the user list
        /// </summary>
        /// <param name="e">An <see cref="Event"/></param>
        /// <returns>An <see cref="HttpEventAnswer"/></returns>
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

        /// <summary>
        /// Add a tree to the tree list
        /// </summary>
        /// <param name="e">An <see cref="Event"/></param>
        /// <returns>An <see cref="HttpEventAnswer"/></returns>
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

        /// <summary>
        /// Get an <see cref="User"/> by its mail
        /// </summary>
        /// <param name="mail">The <see cref="User"/>'s mail</param>
        /// <returns>An <see cref="User"/></returns>
        public static User GetUserByMail(string mail)
        {
            foreach (var item in UserList)
            {
                if (item.Email == mail)
                    return item;
            }
            return null;
        }

        /// <summary>
        /// Remove a <see cref="Service"/> by its name
        /// </summary>
        /// <param name="serviceName">The <see cref="Service"/>'s name</param>
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
