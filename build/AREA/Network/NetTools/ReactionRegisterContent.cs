using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network.NetTools
{
    /// <summary>
    /// The content of an reaction register
    /// </summary>
    public class ReactionRegisterContent
    {
        /// <summary>
        /// The owner of the <see cref="ReactionRegisterContent"/>
        /// </summary>
        public User Owner { get; set; }

        /// <summary>
        /// The reaction's name
        /// </summary>
        public string ReactionName { get; set; }

        /// <summary>
        /// The service's name
        /// </summary>
        public string ServiceName { get; set; }
    }
}
