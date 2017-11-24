using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network.NetTools
{
    public enum ServiceType
    {
        ACTION,
        REACTION
    }

    /// <summary>
    /// Service like (Facebook, Twitter, Slack...)
    /// </summary>
    public class Service
    {
        public Service(string name_, bool accessTokenSecret_, Dictionary<string, ServiceType> actions_, Dictionary<string, ServiceType> reactions_)
        {
            name = name_;
            accessTokenSecret = accessTokenSecret_;
            actions = actions_;
            reactions = reactions_;
        }

        private string name;
        public string Name { get => name; set => name = value; }

        private bool accessTokenSecret = false;
        public bool AccessTokenSecret { get => accessTokenSecret; set => accessTokenSecret = value; }

        private Dictionary<string, ServiceType> actions;
        internal Dictionary<string, ServiceType> Actions { get => actions; set => actions = value; }

        private Dictionary<string, ServiceType> reactions;
        internal Dictionary<string, ServiceType> Reactions { get => reactions; set => reactions = value; }
    }
}
