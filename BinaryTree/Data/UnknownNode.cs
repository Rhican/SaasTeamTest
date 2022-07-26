namespace BinaryTree.Data
{
    public class UnknownNode : OperatorNode
    {
        public UnknownNode() : base(OperatorType.Unknown) { }

        public OperatorNode Recast(OperatorType type)
        {
            OperatorNode node = this;
            switch (type)
            {
                case OperatorType.Plus:
                    node = new PlusOperatorNode(); break;
                case OperatorType.Minus:
                    node = new MinusOperatorNode(); break;
                case OperatorType.Multiply:
                    node = new MultiplyOperatorNode(); break;
                case OperatorType.Divide:
                    node = new DivideOperatorNode(); break;
            }
            node.Parent = this.Parent;
            node.Left = this.Left;
            node.Right = this.Right;
            if (Parent is OperatorNode parent)
            {
                if (parent.Left == this)
                    parent.Left = node;
                else if (parent.Right == this)
                    parent.Right = node;
            }
            return node;
        }
    }
}
