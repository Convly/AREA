using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebClient.Models
{
    public class Area
    {
        public bool IsSelected { get; set; }
        public string Name { get; set; }
        public Tree<string> Tree { get; set; }

        public Area(string name)
        {
            IsSelected = false;
            Name = name;
            Tree = new Tree<string>("");
        }
    }
}