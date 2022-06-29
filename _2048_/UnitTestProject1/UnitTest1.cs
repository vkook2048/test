using _2048_;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Game2048 game = new Game2048();

            // game.Board[0][1] = 2;

            game.Board = new int[][] {
            new int[] { 0, 0, 0, 0 },
            new int[] { 0, 0, 0, 0 },
            new int[] { 0, 0, 2, 2 },
            new int[] { 0, 0, 0, 0 }
            };
            // move right

           var final = new int[][] {
            new int[] { 0, 0, 0, 0 },
            new int[] { 0, 0, 0, 0 },
            new int[] { 0, 0, 0, 4 },
            new int[] { 0, 0, 0, 0 }
            };

            // compare boards
        }
    }
}
