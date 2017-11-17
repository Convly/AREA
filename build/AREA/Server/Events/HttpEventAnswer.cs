using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Area.Events
{
    public class HttpEventAnswer
    {
        public static HttpEventAnswer Error(Event e, int code, string message)
        {
            HttpEventStatus status = new HttpEventStatus { Code = code, Message = message };
            return new HttpEventAnswer { Parent = e, Data = null, Type = null, Status = status };
        }

        public Event Parent { get; set; }
        public  Object Data{ get; set; }
        public Type Type { get; set; }
        public HttpEventStatus Status { get; set; }
    }
}
