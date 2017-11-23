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

        private Dictionary<string, ServiceType> actionName;
        internal Dictionary<string, ServiceType> ActionName { get => actionName; set => actionName = value; }

    }
}
