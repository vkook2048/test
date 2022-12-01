using Calculator;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CalcTestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var exp = Expression.Parse("14,5");
            double val = exp.Calculate();
            Assert.AreEqual(14,5, val);
        }

        [TestMethod]
        public void TestMethod2()
        {
            var exp = Expression.Parse("2 + 3");
            double val = exp.Calculate();
            Assert.AreEqual(5, val);
        }

        [TestMethod]
        public void TestMethod3()
        {
            var exp = Expression.Parse("3 - 2");
            double val = exp.Calculate();
            Assert.AreEqual(1, val);
        }

        [TestMethod]
        public void TestMethod4()
        {
            var exp = Expression.Parse("3 - 2 + 5");
            double val = exp.Calculate();
            Assert.AreEqual(6, val);
        }
        
        [TestMethod]
        public void TestMethod5()
        {
            var exp = Expression.Parse("-2 + 3");
            double val = exp.Calculate();
            Assert.AreEqual(1, val);
        }

        [TestMethod]
        public void TestMethod6()
        {
            var exp = Expression.Parse("2 * 3");
            double val = exp.Calculate();
            Assert.AreEqual(6, val);
        }

        [TestMethod]
        public void TestMethod7()
        {
            var exp = Expression.Parse("2 + 2 * 2");
            double val = exp.Calculate();
            Assert.AreEqual(6, val);
        }

        [TestMethod]
        public void TestMethod8()
        {
            var exp = Expression.Parse("2 * 3 - 2 * 2 / 4  + 2 * 1,5 * 6");
            double val = exp.Calculate();
            Assert.AreEqual(23, val);
        }

        [TestMethod]
        public void TestMethod9()
        {
            var exp = Expression.Parse("(2 + 2) * 2");
            double val = exp.Calculate();
            Assert.AreEqual(8, val);
        }

        [TestMethod]
        public void TestMethod10()
        {
            var exp = Expression.Parse("(16+8)/(19-16) + 3* (-3)");
            double val = exp.Calculate();
            Assert.AreEqual(-1, val);
        }

        [TestMethod]
        public void TestMethod11()
        {
            var exp = Expression.Parse("20 / 5 - 14 + (15 / 3 + 4) * (5 - 2 * 12)");
            double val = exp.Calculate();
            Assert.AreEqual(-181, val);
        }

        [TestMethod]
        public void TestMethod12()
        {
            var exp = Expression.Parse("(1 - 6) / (1 + 4) + 3 * (0 - 3)");
            double val = exp.Calculate();
            Assert.AreEqual(-10, val);
        }

        [TestMethod]
        public void TestMethod13()
        {
            var exp = Expression.Parse("((2 + 2) - 3) * 5");
            double val = exp.Calculate();
            Assert.AreEqual(5, val);
        }

        [TestMethod]
        public void TestMethod14()
        {
            var exp = Expression.Parse("(-1)*((2)) + 1");
            double val = exp.Calculate();
            Assert.AreEqual(-1, val);
        }
    }
}
