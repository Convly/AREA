using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebClient.Models
{
    public delegate void TreeIterator<T>(T nodeData);

    /// <summary>
    /// Defines an <see cref="Tree{T}"/>
    /// </summary>
    public class Tree<T>
    {
        /// <summary>
        /// The data contained in the current node
        /// </summary>
        public T data;

        /// <summary>
        /// The list of children in the current node
        /// </summary>
        public LinkedList<Tree<T>> children;

        /// <summary>
        /// Constructor of a <see cref="Tree{T}"/>
        /// </summary>
        /// <param name="data">The data to be filled in the node</param>
        public Tree(T data)
        {
            this.data = data;
            children = new LinkedList<Tree<T>>();
        }

        /// <summary>
        /// Add a child with some data
        /// </summary>
        /// <param name="data">The data to be filled in the new node</param>
        public void AddChild(T data)
        {
            children.AddFirst(new Tree<T>(data));
        }

        /// <summary>
        /// Get child by an index
        /// </summary>
        /// <param name="idx">The index</param>
        /// <returns>The node at given index or null if not found</returns>
        public Tree<T> GetChild(int idx)
        {
            int i = 0;
            foreach (Tree<T> node in children)
            {
                if (i == idx)
                    return (node);
                i++;
            }
            return (null);
        }

        /// <summary>
        /// Iterate in the tree
        /// </summary>
        /// <param name="node">The node to be recursively iterated</param>
        /// <param name="action">A delegate function called in each node iterated</param>
        public void Iterate(Tree<T> node, TreeIterator<T> action)
        {
            action(node.data);
            foreach (Tree<T> child in node.children)
                Iterate(child, action);
        }
    }
}
