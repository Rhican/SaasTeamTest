namespace BinaryTree.Data
{
    public class MinusOperatorNode : OperatorNode
    {
        public MinusOperatorNode() : base(OperatorType.Minus) { }

        public override double Evaluate()
        {
            return Left.Evaluate() - Right.Evaluate();
        }
    }
}
