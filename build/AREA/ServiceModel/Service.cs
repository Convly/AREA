using System.Collections.Generic;
using Network.NetTools;
using System.Threading;
using System;

namespace Service
{
    public class ThreadPool
    {
        private Dictionary<string, Thread> _threads;

        public Dictionary<string, Thread> Threads { get; set; }

        public ThreadPool()
        {
            _threads = new Dictionary<string, Thread>();
        }

        public void Add(Object obj, string reaction, ReactionDelegate func)
        {
            User user = null;

            var newThread = new Thread(delegate () { func(user); });

            newThread.Start(obj);
            _threads.Add(reaction, newThread);
        }

        public void Remove(string reaction)
        {
            _threads[reaction].Abort();
            _threads.Remove(reaction);
        }
    }

    public class Service : Network.NetTools.IService
    {
        private static string _name;
        private static Dictionary<User, ThreadPool> _tasks = new Dictionary<User, ThreadPool>();
        private static IController _controller;

        public Service(string name, IController controller)
        {
            _name = name;
            _controller = controller;
        }

        public static int Callback(Packet obj)
        {
            Console.WriteLine("Hello from " + _name);

            var state = true;
            string transaction = "";
            User user = new User("", "");

            switch (state)
            {
                // ACTION
                case true:
                    {
                        _controller.Action(transaction, obj);
                        break;
                    }
                // REACTION
                default:
                    {
                        foreach (var it in _tasks)
                        {
                            if (it.Key.Email == user.Email)
                            {
                                _tasks[it.Key].Add(obj, transaction, _controller.Reaction(transaction));
                                break;
                            }
                        }
                        _tasks.Add(user, new ThreadPool());
                        _tasks[user].Add(user, transaction, _controller.Reaction(transaction));
                        break;
                    }
            }
            return (0);
        }

        public string APIName()
        {
            return (_name);
        }

        public List<string> GetActionList()
        {
            return (_controller.GetActionList());
        }

        public Func<Packet, int> GetCallback()
        {
            return (Callback);
        }

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
