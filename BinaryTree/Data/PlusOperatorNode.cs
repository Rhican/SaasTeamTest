namespace BinaryTree.Data
{
    public class PlusOperatorNode : OperatorNode
    {
        public PlusOperatorNode() : base(OperatorType.Plus) { }

        public override double Evaluate()
        {
            return Left.Evaluate() + Right.Evaluate();
        }
    }
}
