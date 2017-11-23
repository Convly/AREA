using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network.NetTools
{
    enum ServiceType
    {
        ACTION,
        REACTION
    }

    public class Service
    {
        private string name;
        public string Name { get => name; set => name = value; }

        private Dictionary<string, ServiceType> actions;
        internal Dictionary<string, ServiceType> Actions { get => actions; set => actions = value; }

        private Dictionary<string, ServiceType> reactions;
        internal Dictionary<string, ServiceType> Reactions { get => reactions; set => reactions = value; }
    }
}
