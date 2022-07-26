using BinaryTree.Data;
using System;
using System.Text.RegularExpressions;

namespace BinaryTree
{
    public class BinaryTreeProgram
    {
        static void Main(string[] args)
        {
            string testInput = "((15 ÷ (7 - (1 + 1) ) ) × -3 ) - (2 + (1 + 1))";

            while (true)
            {
                Console.WriteLine("\n\nEnter Expression: ");
                testInput = Console.ReadLine();
                if (testInput.Length == 0) return;
                try
                {
                    Process(testInput);
                }
                catch (ApplicationException ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
                catch (DivideByZeroException)
                {
                    Console.WriteLine("Error: Division by Zero!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }

        }

        public static double Process(string testInput)
        {
            Regex regex = new Regex("[.0-9]+|[×x*÷/+-]|[()]");
            int level = 0;
            OperatorNode current = null;
            MatchCollection matchedtokens = regex.Matches(testInput);

            CheckInvalidInput(testInput);
            CheckOpenClose(matchedtokens);
            foreach (var part in matchedtokens)
            {
                string value = part.ToString();
                UnknownNode node;
                switch (value)
                {
                    default: // numbers
                        HandleNumbers(ref level, ref current, value);
                        break;
                    case "(":
                        current = OpenNewNode(current);
                        break;
                    case ")":
                        current = CloseNode(current);
                        break;
                    case "+":
                        node = HandlePlus(ref current);
                        break;
                    case "-":
                        node = HandleMinus(ref level, ref current);
                        break;
                    case "*":
                    case "×":
                    case "x":// ambigious symbol, from readline
                        node = HandleMultiply(ref level, ref current);
                        break;
                    case "/":
                    case "÷":
                        node = HandleDivide(ref level, ref current);
                        break;
                }
            }

            OperatorNode root = (current.Operator != OperatorType.Unknown) ? current : current.Left as OperatorNode;
            root.UpdateDepth();

            Console.WriteLine("Print (Text) Binary Tree.");
            root.Print(root.Depth, "", null, false);

            double result = root.Evaluate();
            Console.WriteLine("\n\nFormated Expression: " + root.ToString() + " = " + result);
            return result;
        }

        private static void CheckInvalidInput(string testInput)
        {
            Regex notRegex = new Regex("[^ \t.0-9()×x*÷/+-]");
            MatchCollection shouldNotMatchedtokens = notRegex.Matches(testInput);
            if (shouldNotMatchedtokens.Count > 0)
                throw new Exception("Invalid Input!");
        }

        private static void CheckOpenClose(MatchCollection matchedtokens)
        {
            Console.Write("Tokens: ");
            int stackCount = 0;
            foreach (var part in matchedtokens)
            {
                Console.Write(part.ToString() + " ");
                if (part.ToString() == "(")
                    stackCount++;
                else if (part.ToString() == ")")
                {
                    if (stackCount <= 0)
                    {
                        Console.WriteLine("\n\n");
                        throw new ApplicationException("Invalid Expression, Unexpected Close!");
                    }
                    stackCount--;
                }
            }
            Console.WriteLine("\n\n");

            if (stackCount != 0)
                throw new ApplicationException("Invalid Expression, Open and Close are not matched!");
        }

        private static UnknownNode HandleDivide(ref int level, ref OperatorNode current)
        {
            UnknownNode node = (current as UnknownNode);
            if (node != null) current = node.Recast(OperatorType.Divide);
            else if (current != null && node == null &&
                    (current.Left == null || current.Right == null))
                throw new ApplicationException("Invalid Expression, Divide after an operator!");
            else if (current.Operator == OperatorType.Plus || current.Operator == OperatorType.Minus)
            {
                var newNode = new DivideOperatorNode();
                newNode.Left = current.Right;
                current.Right = newNode;
                newNode.Parent = current;
                current = newNode;
                level++;
            }
            else
            {
                var newNode = new DivideOperatorNode();
                newNode.Left = current;
                current.Parent = newNode;
                current = newNode;
            }

            return node;
        }

        private static UnknownNode HandleMultiply(ref int level, ref OperatorNode current)
        {
            UnknownNode node = (current as UnknownNode);
            if (node != null) current = node.Recast(OperatorType.Multiply);
            else if (current != null && node == null &&
                    (current.Left == null || current.Right == null))
                throw new ApplicationException("Invalid Expression, Mutiply after an operator!");
            else if (current.Operator == OperatorType.Plus || current.Operator == OperatorType.Minus)
            {
                var newNode = new MultiplyOperatorNode();
                newNode.Left = current.Right;
                current.Right = newNode;
                newNode.Parent = current;
                current = newNode;
                level++;
            }
            else
            {
                var newNode = new MultiplyOperatorNode();
                newNode.Left = current;
                current.Parent = newNode;
                current = newNode;
            }

            return node;
        }

        private static UnknownNode HandleMinus(ref int level, ref OperatorNode current)
        {
            UnknownNode node = (current as UnknownNode);
            if (node != null) current = node.Recast(OperatorType.Minus);
            else if (current != null && node == null &&
                    (current.Left == null || current.Right == null)) // Already has one operator, this is a negative sign of the next value
            {
                var newNode = new MinusOperatorNode();
                newNode.Parent = current;
                newNode.Left = new ValueNode(0);
                if (current.Right == null)
                    current.Right = newNode;
                else if (current.Left == null)
                    current.Left = newNode;
                current = newNode;
                level++;
            }
            else
            {
                var newNode = new MinusOperatorNode();
                newNode.Left = current;
                current.Parent = newNode;
                current = newNode;
            }

            return node;
        }

        private static UnknownNode HandlePlus(ref OperatorNode current)
        {
            UnknownNode node = (current as UnknownNode);
            if (node != null) current = node.Recast(OperatorType.Plus);
            else if (current != null && node == null &&
                    (current.Left == null || current.Right == null)) // Already has one operator, this is a positive sign of the next value
            {
                // ignore;
            }
            else
            {
                var newNode = new PlusOperatorNode();
                newNode.Left = current;
                current.Parent = newNode;
                current = newNode;
            }

            return node;
        }

        private static OperatorNode CloseNode(OperatorNode current)
        {
            if (current != null && !typeof(UnknownNode).IsInstanceOfType(current))
            {
                if (current.Parent == null)
                {
                    var parent = new UnknownNode();
                    current.Parent = parent;
                    parent.Left = current;
                }
                current = current.Parent as OperatorNode;
            }

            return current;
        }

        private static OperatorNode OpenNewNode(OperatorNode current)
        {
            var newNode = new UnknownNode();
            if (current != null)
            {
                if (typeof(UnknownNode).IsInstanceOfType(current))
                    current.Left = newNode;
                else
                    current.Right = newNode;

                newNode.Parent = current;
            }

            current = newNode;
            return current;
        }

        private static void HandleNumbers(ref int level, ref OperatorNode current, string value)
        {
            ValueNode valueNode = null;
            if (current == null)
                current = new UnknownNode();
            if (Int32.TryParse(value, out int numValue))
                valueNode = new ValueNode(numValue);
            else
                throw new Exception("Invalid number input!");
            if (current.Left == null)
                current.Left = valueNode;
            else if (current.Right == null /*&& 
                                 !typeof(UnknownNode).IsInstanceOfType(current)*/)
                current.Right = valueNode;

            while (current.Left != null && current.Right != null && level > 0)
            {
                level--;
                current = current?.Parent as OperatorNode;
            }
        }
    }
}
