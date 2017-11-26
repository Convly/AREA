using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using System;

namespace Network.NetTools
{
    /// <summary>
    /// This class is used by a service to inform the Server that an action has been triggered and that a reaction has been created.
    /// </summary>
    public class ServiceReactionContent
    {
        /// <summary>
        /// The name of the initial action.
        /// </summary>
        public string Name;
        
        /// <summary>
        /// The name of the User who want a reaction.
        /// </summary>
        public User User;
        
        /// <summary>
        /// The reaction content
        /// </summary>
        public object ReactionContent;
    }
}
