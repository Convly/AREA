using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebClient.Models
{
    /// <summary>
    /// Defines an AREA
    /// </summary>
    public class Area
    {
        /// <summary>
        /// The name of the AREA
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// A tree containing actions and reactions of the AREA
        /// </summary>
        public Tree<string> Tree { get; set; }

        /// <summary>
        /// Serialize the AREA
        /// </summary>
        /// <returns>The stringified AREA</returns>
        public string AreaToJSON()
        {
            return (JsonConvert.SerializeObject(this));
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">The name of the AREA</param>
        public Area(string name)
        {
            Name = name;
            Tree = new Tree<string>("");
        }
    }
}