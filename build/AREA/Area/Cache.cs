using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Area
{
    public class Cache
    {
        private List<Network.NetTools.NetTools.Service> serviceList;

        public List<Network.NetTools.Service> ServiceList { get => serviceList; set => serviceList = value; }

        public void UpdateServicesList()
        {

        }
    }
}
