using _2048_;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    // Разработка с помощью тестов подразумевает, что ты сначала делаешь маленький шаг и тестируешь его
    // ты не можешь представить сейчас сразу код целиком, который у тебя получится в итоге
    // ты ожидаешь того что тот код, который ты пишешь на певом шаге, в итоге будет другим
    // ты будешь менять его на следующих шагах, при этом заботясь, чтобы все тесты, которые написаны тобой ранее, продолжили работать
    // и да, порой и сами тесты будут меняться, этого тоже не стоит бояться
    // ты не должна думать "я могу написать код и написать тест, но в итоге мне придется изменить и код и тест, как бы мне так написать код и тест чтобы потом меньше было нужно менять"
    // ты должна думать так "сейчас я могу написать вот такой кусочек кода и тест на него, я напишу его прямо сейчас, и если потом мне придется поменять код или даже сам тест - не страшно,
    //     ведь на следующем шаге у меня будет от чего оттолкнуться, ведь у меня уже больше рабочего кода"

    // итак, это первый шаг, пока не будем думать что будет после него, просто напишем код который двигает числа по полю
    // тесты я уже написал, твоя задача заставить их работать
    // для этого тебе нужно написать код в MoveDown и Rotate
    // двигайся сверху вниз по коду заставляя тесты работать по очереди
    // следи чтобы тесты которые ты уже заставила работать не прекращали работать после твоих изменений

    // просто добавил еще одну стргоку для комита

    [TestClass]
    public class UnitTest1
    {
        bool IsBoardsEqual(int[][] board1, int[][] board2)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (board1[i][j] != board2[i][j])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        [TestMethod]
        public void T00_Dummy()
        {
            var board = new int[][] {
            new int[] { 0, 0, 0, 0 },
            new int[] { 0, 0, 0, 0 },
            new int[] { 0, 0, 0, 0 },
            new int[] { 0, 0, 0, 0 }
            };

            board = Game2048.MoveDown(board);

            var expected = new int[][] {
            new int[] { 0, 0, 0, 0 },
            new int[] { 0, 0, 0, 0 },
            new int[] { 0, 0, 0, 0 },
            new int[] { 0, 0, 0, 0 }
            };

            Assert.IsTrue(IsBoardsEqual(expected, board));
        }


        [TestMethod]
        public void T01_TestMove1()
        {
            var board = new int[][] {
            new int[] { 0, 0, 0, 0 },
            new int[] { 0, 0, 2, 0 },
            new int[] { 0, 0, 0, 2 },
            new int[] { 0, 0, 0, 0 }
            };

            board = Game2048.MoveDown(board);

            var expected = new int[][] {
            new int[] { 0, 0, 0, 0 },
            new int[] { 0, 0, 0, 0 },
            new int[] { 0, 0, 0, 0 },
            new int[] { 0, 0, 2, 2 }
            };

            Assert.IsTrue(IsBoardsEqual(expected, board), $"{board[2][2]}");
        }

        [TestMethod]
        public void T02_TestMove2()
        {
            var board = new int[][] {
            new int[] { 0, 0, 8, 0 },
            new int[] { 0, 4, 2, 0 },
            new int[] { 0, 2, 4, 4 },
            new int[] { 0, 8, 0, 0 }
            };

            board = Game2048.MoveDown(board);

            var expected = new int[][] {
            new int[] { 0, 0, 0, 0 },
            new int[] { 0, 4, 8, 0 },
            new int[] { 0, 2, 2, 0 },
            new int[] { 0, 8, 4, 4 }
            };

            Assert.IsTrue(IsBoardsEqual(expected, board));
        }

        [TestMethod]
        public void T03_TestCalculateMove1()
        {
            var board = new int[][] {
            new int[] { 0, 0, 0, 0 },
            new int[] { 0, 0, 0, 0 },
            new int[] { 0, 0, 4, 2 },
            new int[] { 0, 0, 4, 2 }
            };

            board = Game2048.MoveDown(board);

            var expected = new int[][] {
            new int[] { 0, 0, 0, 0 },
            new int[] { 0, 0, 0, 0 },
            new int[] { 0, 0, 0, 0 },
            new int[] { 0, 0, 8, 4 }
            };

            Assert.IsTrue(IsBoardsEqual(expected, board));
        }

        [TestMethod]
        public void T04_TestCalculateMove2()
        {
            var board = new int[][] {
            new int[] { 0, 0, 4, 2 },
            new int[] { 0, 0, 0, 0 },
            new int[] { 0, 0, 4, 4 },
            new int[] { 0, 0, 2, 4}
            };

            board = Game2048.MoveDown(board);

            var expected = new int[][] {
            new int[] { 0, 0, 0, 0 },
            new int[] { 0, 0, 0, 0 },
            new int[] { 0, 0, 8, 2 },
            new int[] { 0, 0, 2, 8 }
            };

            Assert.IsTrue(IsBoardsEqual(expected, board));
        }

        [TestMethod]
        public void T05_TestCalculateMove3()
        {
            var board = new int[][] {
            new int[] { 0, 4, 2, 2 },
            new int[] { 0, 4, 2, 4 },
            new int[] { 0, 4, 4, 2 },
            new int[] { 0, 4, 4, 4 }
            };

            board = Game2048.MoveDown(board);

            var expected = new int[][] {
            new int[] { 0, 0, 0, 2 },
            new int[] { 0, 0, 0, 4 },
            new int[] { 0, 8, 4, 2 },
            new int[] { 0, 8, 8, 4 }
            };

            Assert.IsTrue(IsBoardsEqual(expected, board), $"{board[0][1]} {board[1][1]} {board[2][1]} {board[3][1]}");
        }

        [TestMethod]
        public void T06_TestRotate1()
        {
            var board = new int[][] {
            new int[] { 0, 2, 4, 8 },
            new int[] { 16, 32, 64, 128 },
            new int[] { 256, 512, 1024, 2048 },
            new int[] { 4096, 8192, 16384, 32768 }
            };

            board = Game2048.Rotate(board);

            var expected = new int[][] {
            new int[] { 4096, 256, 16, 0 },
            new int[] { 8192, 512, 32, 2 },
            new int[] { 16384, 1024, 64, 4 },
            new int[] { 32768, 2048, 128, 8 }
            };

            Assert.IsTrue(IsBoardsEqual(expected, board), $"{board[3][3]}");
        }

        [TestMethod]
        public void T07_TestRotate2()
        {
            var board = new int[][] {
            new int[] { 0, 2, 4, 8 },
            new int[] { 16, 32, 64, 128 },
            new int[] { 256, 512, 1024, 2048 },
            new int[] { 4096, 8192, 16384, 32768 }
            };

            board = Game2048.Rotate(board);
            board = Game2048.Rotate(board);
            board = Game2048.Rotate(board);
            board = Game2048.Rotate(board);

            var expected = new int[][] {
            new int[] { 0, 2, 4, 8 },
            new int[] { 16, 32, 64, 128 },
            new int[] { 256, 512, 1024, 2048 },
            new int[] { 4096, 8192, 16384, 32768 }
            };

            Assert.IsTrue(IsBoardsEqual(expected, board));
        }

        [TestMethod]
        public void T08_TestMoveRight()
        {
            var board = new int[][] {
            new int[] { 2, 0, 4, 0 },
            new int[] { 2, 2, 4, 0 },
            new int[] { 4, 4, 4, 4 },
            new int[] { 0, 0, 0, 0 }
            };

            board = Game2048.MoveRight(board);

            var expected = new int[][] {
            new int[] { 0, 0, 2, 4 },
            new int[] { 0, 0, 4, 4 },
            new int[] { 0, 0, 8, 8 },
            new int[] { 0, 0, 0, 0 }
            };

            Assert.IsTrue(IsBoardsEqual(expected, board));
        }

        [TestMethod]
        public void T09_TestMoveUp()
        {
            var board = new int[][] {
            new int[] { 0, 0, 4, 2 },
            new int[] { 0, 2, 0, 2 },
            new int[] { 0, 2, 0, 2 },
            new int[] { 0, 0, 4, 2 }
            };

            board = Game2048.MoveUp(board);

            var expected = new int[][] {
            new int[] { 0, 4, 8, 4 },
            new int[] { 0, 0, 0, 4 },
            new int[] { 0, 0, 0, 0 },
            new int[] { 0, 0, 0, 0 }
            };

            Assert.IsTrue(IsBoardsEqual(expected, board));
        }

        [TestMethod]
        public void T10_TestMoveLeft()
        {
            var board = new int[][] {
            new int[] { 0, 2, 0, 2 },
            new int[] { 4, 4, 4, 4 },
            new int[] { 2, 4, 4, 0 },
            new int[] { 4, 0, 4, 2 }
            };

            board = Game2048.MoveLeft(board);

            var expected = new int[][] {
            new int[] { 4, 0, 0, 0 },
            new int[] { 8, 8, 0, 0 },
            new int[] { 2, 8, 0, 0 },
            new int[] { 8, 2, 0, 0 }
            };

            Assert.IsTrue(IsBoardsEqual(expected, board));
        }
    }
}
