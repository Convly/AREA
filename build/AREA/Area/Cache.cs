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
        public static List<Service> ServiceList = new List<Service>();


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
    }
}
