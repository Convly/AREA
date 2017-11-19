using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebClient.Models
{
    public class Area
    {
        public string Name { get; set; }
        public Tree<string> Tree { get; set; }

        public Area(string name)
        {
            Name = name;
            Tree = new Tree<string>("");
        }
    }
}