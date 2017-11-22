using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebClient.Models
{
    /// <summary>
    /// Defines an <see cref="Area"/>
    /// </summary>
    public class Area
    {
        /// <summary>
        /// The name of the <see cref="Area"/>
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// A tree containing actions and reactions of the <see cref="Area"/>
        /// </summary>
        public Tree<string> Tree { get; set; }

        /// <summary>
        /// Serialize the <see cref="Area"/>
        /// </summary>
        /// <returns>The stringified <see cref="Area"/></returns>
        public string AreaToJSON()
        {
            return (JsonConvert.SerializeObject(this));
        }

        /// <summary>
        /// Constructor of an <see cref="Area"/>
        /// </summary>
        /// <param name="name">The name of the <see cref="Area"/></param>
        public Area(string name)
        {
            Name = name;
            Tree = new Tree<string>("");
        }
    }
}