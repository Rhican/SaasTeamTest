using System;

namespace BinaryTree.Data
{
    public abstract class Node
    {
        public Node Parent { get; set; }

        public virtual double Evaluate()
        {
            throw new NotSupportedException();
        }

    }
}
