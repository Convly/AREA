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

        public void Add(User user, string reaction, ReactionDelegate func)
        {
            var newThread = new Thread(delegate () { func(user); });

            newThread.Start(user);
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
        private string                      _name;
        private Dictionary<int, ThreadPool> _tasks;
        private Dictionary<int, User>       _users;
        private IController                 _controller;


        public Service(string name, IController controller)
        {
            _name = name;
            _tasks = new Dictionary<int, ThreadPool>();
            _users = new Dictionary<int, User>();
            _controller = controller;
        }

        public int Callback(Packet obj)
        {
            Console.WriteLine("Hello from " + _name);

            var state = true;
            string transaction = "";
            User user = new User("", "");

            switch (state)
            {
                // ACTION
                case true :
                    {
                        _controller.Action(transaction);
                        break;
                    }
                // REACTION
                default:
                    {
                        foreach (var it in _users)
                        {
                            if (it.Value.Email == user.Email)
                            {
                                _tasks[it.Key].Add(user, transaction, _controller.Reaction(transaction));
                                break;
                            }
                        }
                        break;
                    }
            }
            
            return (2);
        }

        public string APIName()
        {
            return (_name);
        }

        public List<string> GetActionList()
        {
            return new List<string>();
        }

        public Func<Packet, int> GetCallback()
        {
            return Callback;
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
