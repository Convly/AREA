using System.Collections.Generic;
using Network.NetTools;
using System.Threading;
using Network.Events;
using System;

namespace Service
{
    /// <summary>
    /// This class allow a better gestion of the current threads.
    /// </summary>
    public class ThreadPool
    {
        private Dictionary<string, Thread> _threads;

        /// <summary>
        /// Return a dictionary of all the current threads, order by name.
        /// </summary>
        public Dictionary<string, Thread> Threads { get; set; }

        /// <summary>
        /// Constructor of the thread pool.
        /// </summary>
        public ThreadPool()
        {
            _threads = new Dictionary<string, Thread>();
        }

        /// <summary>
        /// Create a new thread and add it into the thread pool.
        /// </summary>
        /// <param name="obj">The parameters of the function to launch.</param>
        /// <param name="func">The function to launch into the new thread.</param>
        public void Add(Event obj, ReactionDelegate func)
        {
            var reaction = (ServiceActionContent)obj.Data;
            var newThread = new Thread(delegate () { func(obj); });

            newThread.Start(obj);
            _threads.Add(reaction.Name, newThread);
        }

        /// <summary>
        /// Stop a thread.
        /// </summary>
        /// <param name="reaction">The name of the thread to stop.</param>
        public void Remove(Event obj)
        {
            var reaction = (ServiceActionContent)obj.Data;

            _threads[reaction.Name].Abort();
            _threads.Remove(reaction.Name);
        }
    }

    /// <summary>
    /// This class allow the interaction between the main of the service and the API controller.
    /// </summary>
    public class Service : Network.NetTools.IService
    {
        private static string _name;
        private static Dictionary<User, ThreadPool> _tasks = new Dictionary<User, ThreadPool>();
        private static IController _controller;

        /// <summary>
        /// Consctructor of the class.
        /// </summary>
        /// <param name="name">The name of the API.</param>
        /// <param name="controller">The controller of the API.</param>
        public Service(string name, IController controller)
        {
            _name = name;
            _controller = controller;
        }

        /// <summary>
        /// The function which execute an action or a reaction register.
        /// </summary>
        /// <param name="obj">The parameters of the action / reaction register.</param>
        /// <returns></returns>
        public static int Callback(Packet obj)
        {
            Console.WriteLine("Hello from " + _name);

            Event data = (Event)obj.Data.Value;
            User user = data.OwnerInfos;

            switch (obj.Data.Key)
            {
                case PacketCommand.ACTION:
                    {
                        _controller.Action(data);
                        break;
                    }
                case PacketCommand.REACTION_REGISTER:
                    {
                        foreach (var it in _tasks)
                        {
                            if (it.Key.Email == user.Email)
                            {
                                _tasks[it.Key].Add(data, _controller.Reaction(data));
                                break;
                            }
                        }
                        _tasks.Add(user, new ThreadPool());
                        _tasks[user].Add(data, _controller.Reaction(data));
                        break;
                    }
                default:
                    break;
            }
            return (0);
        }

        /// <summary>
        /// Return the name of the API.
        /// </summary>
        /// <returns>The name of the API.</returns>
        public string APIName()
        {
            return (_name);
        }

        /// <summary>
        /// Return the list of the API's actions.
        /// </summary>
        /// <returns>A list of strings, representing the names of the actions</returns>
        public List<string> GetActionList()
        {
            return (_controller.GetActionList());
        }

        /// <summary>
        /// return the function Callback.
        /// </summary>
        /// <returns>The function Callback.</returns>
        public Func<Packet, int> GetCallback()
        {
            return (Callback);
        }

        /// <summary>
        /// Return the list of the API's reactions.
        /// </summary>
        /// <returns>A list of strings, representing the names of the reactions</returns>
        public List<string> GetReactionList()
        {
            return (_controller.GetReactionList());
        }

        public List<User> GetUsersListByAction(string action)
        {
            List<User> tmp = new List<User>();
            foreach (var users in _tasks)
            {
                foreach (var task in users.Value.Threads)
                {
                    if (task.Key == action)
                    {
                        tmp.Add(users.Key);
                        break;
                    }
                }
            }
            return (tmp);
        }

        public List<User> GetUsersListByReaction(string reaction)
        {
            List<User> tmp = new List<User>();
            foreach (var users in _tasks)
            {
                foreach (var task in users.Value.Threads)
                {
                    if (task.Key == reaction)
                    {
                        tmp.Add(users.Key);
                        break;
                    }
                }
            }
            return (tmp);
        }
    }
}
