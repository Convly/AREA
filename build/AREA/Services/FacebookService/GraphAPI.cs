using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Network.NetTools;
using System.Threading;

public class ThreadPool
{
    private Dictionary<string, Thread> _threads;

    public Dictionary<string, Thread>   Threads { get; set; }

    public void Add(string reaction, void() func)
    {
        Thread newThread = new Thread(new ThreadStart(func));

        newThread.Start();
        _threads.Add(reaction, newThread);
    }

    public void Remove(string action)
    {
        _threads[action].Abort();
        _threads.Remove(action);
    }
}

namespace FacebookService
{
    public class GraphAPI : Network.NetTools.IService
    {
        private static string   _name = "graphAPI";
        private ThreadPool      _threadPool;

        public static int Callback(Packet obj)
        {
            Console.WriteLine("Hello from " + _name);
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
