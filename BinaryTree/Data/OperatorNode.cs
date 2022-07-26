using System;
using System.Collections.Generic;

namespace BinaryTree.Data
{
    public class OperatorNode : Node
    {
        public Node Left { get; set; }
        public Node Right { get; set; }

        public OperatorNode(OperatorType oper)
        {
            Operator = oper;
        }
        public OperatorType Operator { get; }

        public int Depth { get; protected set; }

        protected int CalDepth(int baseDepth)
        {
            Depth = baseDepth - 1;
            (Left as OperatorNode)?.CalDepth(Depth);
            (Right as OperatorNode)?.CalDepth(Depth);
            return Depth;
        }

        public void UpdateDepth()
        {
            CalDepth(0);
        }

        public void Print(int depth, string padding = "", SortedDictionary<int, string> result = null, bool isFull = true)
        {
            bool isRoot = false;
            if (result == null)
            {
                result = new SortedDictionary<int, string>();
                isRoot = true;
            }

            if (isFull)
            {
                if (result.ContainsKey(Math.Abs(Depth)) == false)
                    result[Math.Abs(Depth)] = padding + " " + Left + " " + ToSymbol(Operator) + " " + Right;
                else
                    result[Math.Abs(Depth)] += "    " + padding + " " + Left + " " + ToSymbol(Operator) + " " + Right;
            }
            else //slim
            {
                UpdatePrintResult(result, (result.ContainsKey(Math.Abs(Depth)) == false));
            }

            var l = (Left as OperatorNode)?.Depth;
            var r = (Right as OperatorNode)?.Depth;
            if (l != null)
                (Left as OperatorNode)?.Print(l.Value, padding + "   ", result, isFull);
            if (r != null)
                (Right as OperatorNode)?.Print(r.Value, padding + "   ", result, isFull);


            int maxDepth = 0;
            MaxDepth(ref maxDepth);
            maxDepth = Math.Abs(maxDepth);
            Depth = depth - 1;
            if (isRoot)
            {
                PrintAsText(result, maxDepth);
            }
        }

        private void UpdatePrintResult(SortedDictionary<int, string> result, bool isNew)
        {
            int childDepth = Math.Abs(Depth - 1);
            if (Left as ValueNode != null && result.ContainsKey(childDepth) == false)
            {
                result[childDepth] = Padding(this, false) + "  " + Left.ToString() + "  ";
            }
            else if (Left as ValueNode != null)
            {
                result[childDepth] += "  " + Left.ToString() + "  ";
            }

            if (isNew) result[Math.Abs(Depth)] = Padding(this, false);
            result[Math.Abs(Depth)] += "  " + ToSymbol(Operator) + "  ";

            if (Right as ValueNode != null && result.ContainsKey(childDepth) == false)
            {
                result[childDepth] = Padding(this, true) + "  " + Right.ToString() + "  ";
            }
            else if (Right as ValueNode != null)
            {
                result[childDepth] += "  " + Right.ToString() + "  ";
            }
        }

        private void PrintAsText(SortedDictionary<int, string> result, int maxDepth)
        {
            for (int i = 1; i <= result.Count; i++)
            {
                bool found = false;
                Console.Write((i) + ":");
                foreach (var pair in result)
                {
                    if (pair.Key == i)
                    {
                        Console.Write(StartLinePadding(i, maxDepth) + pair.Value.ToString());
                        found = true;
                    }
                }
                Console.WriteLine("");
                if (!found) break;
            }
        }

        private string Padding(OperatorNode node, bool isRight = false)
        {
            string padding = (!isRight) ? "" : "   ";
            Int64 flags = 0;
            int count = 0;
            for (int flag = 1; node.Parent != null; flag <<= 1)
            {
                OperatorNode parent = node.Parent as OperatorNode;
                if (parent.Right == node) flags += flag;
                count++;
                node = parent;
            }
            if (count > 1)
                for (flags -= (count - 1) >> 1; flags > 0; flags--)
                    padding += "      ";
            return padding;
        }

        private string StartLinePadding(int depth, int max)
        {
            string padding = "   ";
            int dif = Math.Abs(max - depth);
            for (int i = 0; i < dif; i++)
            {
                padding += "   ";
            }
            return padding;
        }
        private void MaxDepth(ref int depth)
        {
            if (Left as OperatorNode != null) (Left as OperatorNode).MaxDepth(ref depth);
            if (Right as OperatorNode != null) (Right as OperatorNode).MaxDepth(ref depth);
            if (Depth < depth) depth = Depth;
        }

        private static readonly Lazy<Dictionary<OperatorType, string>> Symbols = new Lazy<Dictionary<OperatorType, string>>(() => new Dictionary<OperatorType, string>()
                {
                    { OperatorType.Plus, "+" },
                    { OperatorType.Minus, "-" },
                    { OperatorType.Multiply, "×" },
                    { OperatorType.Divide, "÷" }
                });
        private static string ToSymbol(OperatorType oper)
        {
            if (Symbols.Value.ContainsKey(oper)) return Symbols.Value[oper];
            return "?";
        }

        public override string ToString()
        {
            return "(" + Left + ToSymbol(Operator) + Right + ")";
        }

    }
}
