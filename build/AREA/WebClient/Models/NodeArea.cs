using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace WebClient.Models
{
    /// <summary>
    /// Defines a <see cref="NodeArea"/>
    /// </summary>
    public class NodeArea
    {
        /// <summary>
        /// The name of the <see cref="NodeArea"/>
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Constructor of a <see cref="NodeArea"/>
        /// </summary>
        /// <param name="name">The name of the <see cref="NodeArea"/></param>
        public NodeArea(string name)
        {
            Name = name;
        }
    }
}