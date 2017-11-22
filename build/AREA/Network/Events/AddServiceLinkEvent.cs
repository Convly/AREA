using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network.Events
{
    public class AddServiceLinkEvent : Event
    {
        public AddServiceLinkEvent(HttpEventSource source_, HttpEventType type_, NetTools.UserInfos owner_)
            : base(source_, type_, owner_)
        {

        }
    }
}
