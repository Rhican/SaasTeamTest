using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestProject
{
    [TestClass]
    public class BinaryTreeTest
    {
        [TestMethod]
        [Owner("ZehuaRong")]
        [TestCategory("BinaryTree")]
        public void Op1_Addition()
        {
            double result = BinaryTree.BinaryTreeProgram.Process("1+1");
            Assert.AreEqual(1 + 1, result);
            result = BinaryTree.BinaryTreeProgram.Process("1+(2+3)++4");
            Assert.AreEqual(1 + (2 + 3) + 4, result);
            result = BinaryTree.BinaryTreeProgram.Process("1+2+3++4");
            Assert.AreEqual(1 + 2 + 3 + 4, result);
        }

        [TestMethod]
        [Owner("ZehuaRong")]
        [TestCategory("BinaryTree")]
        public void Op2_Substraction()
        {
            double result = BinaryTree.BinaryTreeProgram.Process("10-1");
            Assert.AreEqual(10 - 1, result);
            result = BinaryTree.BinaryTreeProgram.Process("100 - (1 - 2) -3");
            Assert.AreEqual(100 - (1 - 2) - 3, result);
            result = BinaryTree.BinaryTreeProgram.Process("1+2+3+-4");
            Assert.AreEqual(1 + 2 + 3 + -4, result);
        }

        [TestMethod]
        [Owner("ZehuaRong")]
        [TestCategory("BinaryTree")]
        public void Op3_Multiplication()
        {
            double result = BinaryTree.BinaryTreeProgram.Process("2*3");
            Assert.AreEqual(2*3, result);
            result = BinaryTree.BinaryTreeProgram.Process("10 + 1*(2*3) - 100");
            Assert.AreEqual(10 + 1 * (2 * 3) - 100, result);
            result = BinaryTree.BinaryTreeProgram.Process("10 * (2 * -3) + 100 * +5");
            Assert.AreEqual(10 * (2 * -3) + 100 * +5, result);
        }

        [TestMethod]
        [Owner("ZehuaRong")]
        [TestCategory("BinaryTree")]
        public void Op4_Division()
        {
            double result = BinaryTree.BinaryTreeProgram.Process("6/3");
            Assert.AreEqual(6 / 3, result);
            result = BinaryTree.BinaryTreeProgram.Process("10 + 90/(12/3) - 100");
            Assert.AreEqual(10 + 90f / (12 / 3) - 100, result);
            result = BinaryTree.BinaryTreeProgram.Process("100 / (20 / -2) + 100 / +5");
            Assert.AreEqual(100f / (20 / -2) + 100f / +5, result);
        }

        [TestMethod]
        [Owner("ZehuaRong")]
        [TestCategory("BinaryTree")]
        [ExpectedException(typeof(DivideByZeroException), "Exception Expected Division by zero")]
        public void OpEx1_DivisionByZero()
        {
            double result = BinaryTree.BinaryTreeProgram.Process("6/0");
        }

        [TestMethod]
        [Owner("ZehuaRong")]
        [TestCategory("BinaryTree")]
        [ExpectedException(typeof(ApplicationException), "Exception Expected Application Exception")]
        public void OpEx2_ApplicationException1()
        {
            double result = BinaryTree.BinaryTreeProgram.Process("1 + ((2 + 3)");
        }

        [TestMethod]
        [Owner("ZehuaRong")]
        [TestCategory("BinaryTree")]
        [ExpectedException(typeof(ApplicationException), "Exception Expected Application Exception")]
        public void OpEx2_ApplicationException2()
        {
            double result = BinaryTree.BinaryTreeProgram.Process("1 + (2 + 3))");
        }

        [TestMethod]
        [Owner("ZehuaRong")]
        [TestCategory("BinaryTree")]
        [ExpectedException(typeof(ApplicationException), "Exception Expected Application Exception")]
        public void OpEx2_ApplicationException3()
        {
            double result = BinaryTree.BinaryTreeProgram.Process("1 + (2 + *3)");
        }

        [TestMethod]
        [Owner("ZehuaRong")]
        [TestCategory("BinaryTree")]
        [ExpectedException(typeof(ApplicationException), "Exception Expected Application Exception")]
        public void OpEx2_ApplicationException4()
        {
            double result = BinaryTree.BinaryTreeProgram.Process("1 + (2 + /3)");
        }

        [TestMethod]
        [Owner("ZehuaRong")]
        [TestCategory("BinaryTree")]
        [ExpectedException(typeof(Exception), "Exception Expected Input Exception")]
        public void OpEx3_InputException1()
        {
            double result = BinaryTree.BinaryTreeProgram.Process("1 + 2a");
        }

        [TestMethod]
        [Owner("ZehuaRong")]
        [TestCategory("BinaryTree")]
        [ExpectedException(typeof(Exception), "Exception Expected Input Exception")]
        public void OpEx3_InputException2()
        {
            double result = BinaryTree.BinaryTreeProgram.Process("1 + 2.12345");
        }

        [TestMethod]
        [Owner("ZehuaRong")]
        [TestCategory("BinaryTree")]
        public void TestExpression()
        {
            double result = BinaryTree.BinaryTreeProgram.Process("((15 ÷ (7 - (1 + 1) ) ) × -3 ) - (2 + (1 + 1))");
            Assert.AreEqual(((15 / (7 - (1 + 1))) * -3 ) -(2 + (1 + 1)), result);
        }
    }
}
