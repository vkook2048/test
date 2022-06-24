using Inter;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        class TestView : IView
        {
            public void UpdateView(Game game)
            {
                
            }
        }

        [TestMethod]
        public void TestMethod1()
        {
            var game = new Game(new TestView());
            game.UserClick(1);

            bool catched = false;
            try
            {
                game.UserClick(1);

            }
            catch (Exception exc)
            {
                catched = true;
            }
            if (!catched)
                Assert.Fail("wrong");

        }

        [TestMethod]
        public void TestMethod2()
        {
            var game = new Game(new TestView());
            game.UserClick(1);
            game.UserClick(0);
            game.UserClick(1);
        }

        [TestMethod]
        public void TestMethodDraw()
        {
            var game = new Game(new TestView());
            game.UserClick(0);
            game.UserClick(1);
            game.UserClick(2);
            game.UserClick(3);
            game.UserClick(4);
            game.UserClick(6);
            game.UserClick(5);
            game.UserClick(8);
            game.UserClick(7);
            Assert.IsTrue(game.IsDraw(), "Not Draw");
        }
        [TestMethod]
        public void TestMethodWinnsX()
        {
            var game = new Game(new TestView());
            game.UserClick(4);
            game.UserClick(1);
            game.UserClick(5);
            game.UserClick(3);
            game.UserClick(2);
            game.UserClick(6);
            game.UserClick(8);
            Assert.IsTrue(game.CurrentPlayer() == "X", "Not X");
        }
        [TestMethod]
        public void TestMethodWinnsO()
        {
            var game = new Game(new TestView());
            game.UserClick(3);
            game.UserClick(4);
            game.UserClick(6);
            game.UserClick(0);
            game.UserClick(7);
            game.UserClick(8);
            Assert.IsTrue(game.CurrentPlayer() == "O", "Not O");
        }


        // TODO Test: draw, winn x, winn o |done
    }
}
