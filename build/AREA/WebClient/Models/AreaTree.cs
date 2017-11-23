using MongoDB.Bson;
using System.Collections.Generic;

namespace WebClient.Models
{
    public class Point
    {
        public int x { get; set; }
        public int y { get; set; }

        public Point()
        {
            x = 0;
            y = 0;
        }
    }

    public class ANodeData
    {
        public string name { get; set; }
        public Point pos { get; set; }
        public string type { get; set; }
        
        public ANodeData()
        {
            name = "";
            pos = new Point();
            type = "";
        }
    }

    public class ANode
    {
        public ANodeData data { get; set; }
        public List<ANode> children { get; set; }

        public ANode()
        {
            data = new ANodeData();
            children = new List<ANode>();
        }
    }

    public class ATreeRoot
    {
        public string Name { get; set; }
        public ANode root { get; set; }

        public ATreeRoot(string name)
        {
            Name = name;
            root = new ANode();
        }
    }

    public class AreaTree
    {
        public ObjectId Id { get; set; }
        public string Email { get; set; }
        public List<ATreeRoot> AreasList { get; set; }

        public AreaTree(string email)
        {
            Email = email;
            AreasList = new List<ATreeRoot>();
        }
    }
}