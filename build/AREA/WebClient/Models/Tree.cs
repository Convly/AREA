using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebClient.Models
{
    public delegate void TreeIterator<T>(T nodeData);

    public class Tree<T>
    {
        public T data;
        public LinkedList<Tree<T>> children;

        public Tree(T data)
        {
            this.data = data;
            children = new LinkedList<Tree<T>>();
        }

        public void AddChild(T data)
        {
            children.AddFirst(new Tree<T>(data));
        }

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

        public void Iterate(Tree<T> node, TreeIterator<T> action)
        {
            action(node.data);
            foreach (Tree<T> child in node.children)
                Iterate(child, action);
        }
    }
}
