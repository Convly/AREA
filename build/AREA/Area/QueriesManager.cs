using Network.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Area
{
    /// <summary>
    /// Defines the class which will manage the get queries
    /// </summary>
    public class QueriesManager
    {
        public static HttpEventAnswer ProcessEvent(Event e)
        {
            return HttpEventAnswer.Error(e, 500, "Hello from the other side");
        }
    }
}
