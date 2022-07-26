namespace BinaryTree.Data
{
    public class MultiplyOperatorNode : OperatorNode
    {
        public MultiplyOperatorNode() : base(OperatorType.Multiply) { }

        public override double Evaluate()
        {
            return Left.Evaluate() * Right.Evaluate();
        }
    }
}
