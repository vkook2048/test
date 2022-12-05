using Calculator;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CalcTestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod01()
        {
            var exp = Expression.Parse("14,5");
            double val = exp.Calculate();
            Assert.AreEqual(14,5, val);
        }

        [TestMethod]
        public void TestMethod02()
        {
            var exp = Expression.Parse("2 + 3");
            double val = exp.Calculate();
            Assert.AreEqual(5, val);
        }

        [TestMethod]
        public void TestMethod03()
        {
            var exp = Expression.Parse("3 - 2");
            double val = exp.Calculate();
            Assert.AreEqual(1, val);
        }

        [TestMethod]
        public void TestMethod04()
        {
            var exp = Expression.Parse("3 - 2 + 5");
            double val = exp.Calculate();
            Assert.AreEqual(6, val);
        }
        
        [TestMethod]
        public void TestMethod05()
        {
            var exp = Expression.Parse("-2 + 3");
            double val = exp.Calculate();
            Assert.AreEqual(1, val);
        }

        [TestMethod]
        public void TestMethod06()
        {
            var exp = Expression.Parse("2 * 3");
            double val = exp.Calculate();
            Assert.AreEqual(6, val);
        }

        [TestMethod]
        public void TestMethod07()
        {
            var exp = Expression.Parse("2 + 2 * 2");
            double val = exp.Calculate();
            Assert.AreEqual(6, val);
        }

        [TestMethod]
        public void TestMethod08()
        {
            var exp = Expression.Parse("2 * 3 - 2 * 2 / 4  + 2 * 1,5 * 6");
            double val = exp.Calculate();
            Assert.AreEqual(23, val);
        }

        [TestMethod]
        public void TestMethod09()
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

        [TestMethod]
        public void TestMethod15()
        {
            var exp = Expression.Parse("2 ^ 3");
            double val = exp.Calculate();
            Assert.AreEqual(8, val);
        }

        [TestMethod]
        public void TestMethod16()
        {
            var exp = Expression.Parse("2 ^ 3 + 2 ^ 2 - 3 ^ 2");
            double val = exp.Calculate();
            Assert.AreEqual(3, val);
        }

        [TestMethod]
        public void TestMethod17()
        {
            var exp = Expression.Parse("(2 ^ 2 - 3 ^ 0) * 2 ^ 4");
            double val = exp.Calculate();
            Assert.AreEqual(48, val);
        }

        [TestMethod]
        public void TestMethod18()
        {
            var exp = Expression.Parse("(2 + 2) ^ 2");
            double val = exp.Calculate();
            Assert.AreEqual(16, val);
        }

        [TestMethod]
        public void TestMethod19()
        {
            var exp = Expression.Parse("((2 ^ 2 - 3 ^ 0) * 2) ^ 2");
            double val = exp.Calculate();
            Assert.AreEqual(36, val);
        }

        [TestMethod]
        public void TestMethod20()
        {
            var exp = Expression.Parse("(3 + 2) ^ 2 / (7 - 2) ^ (0 + 1) + 3");
            double val = exp.Calculate();
            Assert.AreEqual(8, val);
        }

        [TestMethod]
        public void TestMethod21()
        {
            var exp = Expression.Parse("((3 + 2) ^ 2 / (7 - 2) ^ (0 + 1) + 3) ^ 3 / 4");
            double val = exp.Calculate();
            Assert.AreEqual(128, val);
        }
    }
}
