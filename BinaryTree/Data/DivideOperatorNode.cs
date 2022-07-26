using System;

namespace BinaryTree.Data
{
    public class DivideOperatorNode : OperatorNode
    {
        public DivideOperatorNode() : base(OperatorType.Divide) { }

        public override double Evaluate()
        {
            var dividor = Right.Evaluate();
            if (dividor != 0)
                return Left.Evaluate() / Right.Evaluate(); // Warning division by zero
            else
                throw new DivideByZeroException();
        }
    }
}
