using System;
using System.Collections.Generic;
using System.Text;

namespace _2048_
{
    public class Game2048
    {
        /* public int[][] Board = new int[][] { 
             new int[] { 0, 0, 0, 0 },
             new int[] { 0, 0, 0, 0 },
             new int[] { 0, 0, 0, 0 },
             new int[] { 0, 0, 0, 0 }
         };*/
        static bool IsMoved = true;

        public static int[][] MoveDown(int[][] board)
        {
            int[][] saver = new int[][] {
            new int[] { 0, 0, 0, 0 },
            new int[] { 0, 0, 0, 0 },
            new int[] { 0, 0, 0, 0 },
            new int[] { 0, 0, 0, 0 }
            };
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    saver[i][j] = board[i][j];
                }
            }
            int count = 0;
            while (count < 3)
            {
                board = Move(board);
                count++;
            }
            for (int i = 3; i > 0; i--)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (i > 0 && board[i - 1][j] == board[i][j] && board[i][j] != 0)
                    {
                        board[i][j] += board[i - 1][j];
                        board[i - 1][j] = 0;
                    }
                }
            }
            board = Move(board);
            IsMoved = !IsBoardsEqual(saver, board);
            return board;
        }
        private static int[][] Move(int[][] board)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (i < 3 && board[i + 1][j] == 0)
                    {
                        board[i + 1][j] = board[i][j];
                        board[i][j] = 0;
                    }
                }
            }
            return board;
        }

        public static int[][] Rotate(int[][] board)
        {

            int[][] helper = new int[][] {
            new int[] { 0, 0, 0, 0 },
            new int[] { 0, 0, 0, 0 },
            new int[] { 0, 0, 0, 0 },
            new int[] { 0, 0, 0, 0 } };

            helper[0][3] = board[0][0];
            helper[1][3] = board[0][1];
            helper[2][3] = board[0][2];
            helper[3][3] = board[0][3];

            helper[0][2] = board[1][0];
            helper[1][2] = board[1][1];
            helper[2][2] = board[1][2];
            helper[3][2] = board[1][3];

            helper[0][1] = board[2][0];
            helper[1][1] = board[2][1];
            helper[2][1] = board[2][2];
            helper[3][1] = board[2][3];

            helper[0][0] = board[3][0];
            helper[1][0] = board[3][1];
            helper[2][0] = board[3][2];
            helper[3][0] = board[3][3];

            return helper;
        }


        public static int[][] MoveLeft(int[][] board)
        {
            board = Rotate(board);
            board = Rotate(board);
            board = Rotate(board);
            board = MoveDown(board);
            board = Rotate(board);
            return board;
        }

        public static int[][] MoveUp(int[][] board)
        {
            board = Rotate(board);
            board = Rotate(board);
            board = MoveDown(board);
            board = Rotate(board);
            board = Rotate(board);
            return board;
        }

        public static int[][] MoveRight(int[][] board)
        {
            board = Rotate(board);
            board = MoveDown(board);
            board = Rotate(board);
            board = Rotate(board);
            board = Rotate(board);
            return board;

        }

        private static bool IsBoardsEqual(int[][] board1, int[][] board2)
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

        public static bool CanGenerateNew(int[][] board)
        {
            int count = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (board[i][j] == 0)
                    {
                        count++;
                    }
                }
            }
            if (IsMoved && count > 0 || count == 16)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static int[][] GenerateNewNumber(int[][] board)
        {
            if (CanGenerateNew(board))
            {
                Random rnd = new Random();
                int first = rnd.Next(0, 3);
                int second = rnd.Next(0, 3);
                while (board[first][second] != 0)
                {
                    first = rnd.Next(0, 3);
                    second = rnd.Next(0, 3);
                }
                int[] variants = new int[] { 2, 2, 2, 4, 2, 2 };
                board[first][second] = variants[rnd.Next(0, variants.Length)];
            }
            return board;
        }
        public static bool IsEnd(int[][] board)
        {
            int[][] tester = new int[][] {
            new int[] { 0, 0, 0, 0 },
            new int[] { 0, 0, 0, 0 },
            new int[] { 0, 0, 0, 0 },
            new int[] { 0, 0, 0, 0 }
            };
            int count = 0;

            tester = MoveDown(board);
            if (!IsMoved)
                count++;
            tester = MoveUp(board);
            if (!IsMoved)
                count++;
            tester = MoveLeft(board);
            if (!IsMoved)
                count++;
            tester = MoveRight(board);
            if (!IsMoved)
                count++;
            if(!CanGenerateNew(board))
                count++;

            if (count == 5)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static int[][] AfterTappingAndMoving(int[][] board)
        {
            board = GenerateNewNumber(board);
            IsEnd(board);
            return board;
        }

    }
}
