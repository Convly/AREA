using MongoDB.Bson;
using System.Collections.Generic;

namespace Network.NetTools
{
    /// <summary>
    /// A basic structure defining a 2D Position with <see cref="int"/> variables
    /// </summary>
    public class Point
    {
        /// <summary>
        /// The X position
        /// </summary>
        public int x { get; set; }

        /// <summary>
        /// The X position
        /// </summary>
        public int y { get; set; }

        /// <summary>
        /// Constructor of <see cref="Point"/>
        /// </summary>
        public Point()
        {
            x = 0;
            y = 0;
        }
    }
    
    /// <summary>
    /// Defines the data stored in an <see cref="ANode"/>
    /// </summary>
    public class ANodeData
    {
        /// <summary>
        /// The <see cref="ANode"/>'s name
        /// </summary>
        public string name { get; set; }


        /// <summary>
        /// The <see cref="ANode"/>'s service name
        /// </summary>
        public string serviceName { get; set; }

        /// <summary>
        /// The <see cref="ANode"/>'s event name
        /// </summary>
        public string eventName { get; set; }
        
        /// <summary>
        /// The <see cref="ANode"/>'s position in the canvas
        /// </summary>
        public Point pos { get; set; }

        /// <summary>
        /// The <see cref="ANode"/>'s type ("action" or "reaction")
        /// </summary>
        public string type { get; set; }
        
        /// <summary>
        /// Constructor of the <see cref="ANodeData"/>
        /// </summary>
        public ANodeData()
        {
            name = "";
            serviceName = "";
            eventName = "";
            pos = new Point();
            type = "";
        }
    }

    /// <summary>
    /// Defines a node in the AREA's tree structure
    /// </summary>
    public class ANode
    {
        /// <summary>
        /// The data contained in the <see cref="ANode"/>
        /// </summary>
        public ANodeData data { get; set; }

        /// <summary>
        /// A list of the <see cref="ANode"/> children
        /// </summary>
        public List<ANode> children { get; set; }

        /// <summary>
        /// Constructor of <see cref="ANode"/>
        /// </summary>
        public ANode()
        {
            data = new ANodeData();
            children = new List<ANode>();
        }
    }

    /// <summary>
    /// Defines an AREA
    /// </summary>
    public class ATreeRoot
    {
        /// <summary>
        /// The <see cref="ATreeRoot"/>'s name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The root for the tree structure
        /// </summary>
        public ANode root { get; set; }

        /// <summary>
        /// Constructor of <see cref="ATreeRoot"/>
        /// </summary>
        /// <param name="name">The <see cref="ATreeRoot"/>'s name</param>
        public ATreeRoot(string name)
        {
            Name = name;
            root = new ANode();
        }
    }

    /// <summary>
    /// Defines all AREAs for an <see cref="User"/>
    /// </summary>
    public class AreaTree
    {
        /// <summary>
        /// The <see cref="ObjectId"/> <see cref="User"/>'s identifier
        /// </summary>
        public ObjectId Id { get; set; }

        /// <summary>
        /// The <see cref="User"/>'s email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// The list of all AREAs for the <see cref="User"/>
        /// </summary>
        public List<ATreeRoot> AreasList { get; set; }

        /// <summary>
        /// Constructor of <see cref="AreaTree"/>
        /// </summary>
        /// <param name="email">The <see cref="User"/>'s email</param>
        public AreaTree(string email)
        {
            Email = email;
            AreasList = new List<ATreeRoot>();
        }
    }
}