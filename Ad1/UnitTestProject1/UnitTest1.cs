using System;
using Ad1;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod10plus9()
        {
            LargeNumbers l1 = new LargeNumbers("10");
            LargeNumbers l2 = new LargeNumbers("9");
            string summ = l1.Summation(l2).ShowNumber();
            Assert.AreEqual("19", summ, "10+9=" + summ);
        }

        [TestMethod]
        public void TestMethod21plus3()
        {
            LargeNumbers l1 = new LargeNumbers("21");
            LargeNumbers l2 = new LargeNumbers("3");
            string summ = l1.Summation(l2).ShowNumber();
            Assert.AreEqual("24", summ, "21+3=" + summ);
            Assert.AreEqual("21", l1.ToString(), "21=" + l1);
        }

        [TestMethod]
        public void TestMethod3plus21()
        {
            LargeNumbers l1 = new LargeNumbers("3");
            LargeNumbers l2 = new LargeNumbers("21");
            string summ = l1.Summation(l2).ShowNumber();
            Assert.AreEqual("24", summ, "21+3=" + summ);
            Assert.AreEqual("21", l2.ToString(), "21=" + l2);
        }

        [TestMethod]
        public void TestMethod0plus0()
        {
            LargeNumbers l1 = new LargeNumbers("0");
            LargeNumbers l2 = new LargeNumbers("0");
            string summ = l1.Summation(l2).ShowNumber();
            Assert.IsTrue(summ == "0");
        }

        [TestMethod]
        public void TestMethod9plus9()
        {
            LargeNumbers l1 = new LargeNumbers("9");
            LargeNumbers l2 = new LargeNumbers("9");
            string summ = l1.Summation(l2).ShowNumber();
            Assert.IsTrue(summ == "18");
        }

        [TestMethod]
        public void TestMethodRandom()
        {
            Random rnd = new Random();
            for (int i = 0; i < 10000; i++)
            {
                int p1 = rnd.Next(0, int.MaxValue / 2);
                int p2 = rnd.Next(0, int.MaxValue / 2);

                LargeNumbers l1 = new LargeNumbers(p1.ToString());
                LargeNumbers l2 = new LargeNumbers(p2.ToString());

                string summ = l1.Summation(l2).ShowNumber();
                Assert.AreEqual((p1 + p2).ToString(), summ, p1 + " + " + p2 + " = " + summ);
            }
        }
        [TestMethod]
        public void TestMethod0x0()
        {
            LargeNumbers l1 = new LargeNumbers("0");
            LargeNumbers l2 = new LargeNumbers("0");
            string mult = l1.Multiplication(l2).ShowNumber();
            Assert.AreEqual("0", mult, "0*0=" + mult);
        }


        [TestMethod]
        public void TestMethod1x42()
        {
            LargeNumbers l1 = new LargeNumbers("1");
            LargeNumbers l2 = new LargeNumbers("42");
            string mult = l1.Multiplication(l2).ShowNumber();
            Assert.AreEqual("42", mult, "1*42=" + mult);
        }

        [TestMethod]
        public void TestMethod8x0()
        {
            LargeNumbers l1 = new LargeNumbers("8");
            LargeNumbers l2 = new LargeNumbers("0");
            string mult = l1.Multiplication(l2).ShowNumber();
            Assert.AreEqual("0", mult, "8*0=" + mult);
        }

        [TestMethod]
        public void TestMethod8x125()
        {
            LargeNumbers l1 = new LargeNumbers("8");
            LargeNumbers l2 = new LargeNumbers("125");
            string mult = l1.Multiplication(l2).ShowNumber();
            Assert.AreEqual("1000", mult, "8*125=" + mult);
        }

        [TestMethod]
        public void TestMethod12x50()
        {
            LargeNumbers l1 = new LargeNumbers("12");
            LargeNumbers l2 = new LargeNumbers("50");
            string mult = l1.Multiplication(l2).ShowNumber();
            Assert.AreEqual("600", mult, "12*50=" + mult);
        }
    }
}
