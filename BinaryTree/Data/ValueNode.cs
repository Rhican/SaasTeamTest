namespace BinaryTree.Data
{
    public class ValueNode : Node
    {
        public int Value { get; }

        public ValueNode(int value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return Value + "";
        }

        public override double Evaluate()
        {
            return Value;
        }
    }
}
