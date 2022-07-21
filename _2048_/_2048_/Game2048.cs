using System;
using System.Collections.Generic;
using System.Text;

namespace _2048_
{



    public class Game2048
    {
        #region Private Static

        public static int[][] PrivateMoveDown(int[][] board)
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


        public static int[][] PrivateMoveLeft(int[][] board)
        {
            board = Rotate(board);
            board = Rotate(board);
            board = Rotate(board);
            board = PrivateMoveDown(board);
            board = Rotate(board);
            return board;
        }


        public static bool CanMoveUp(int[][] board)
        {
            var moved = PrivateMoveUp(board);
            return !IsBoardsEqual(board, moved);
        }
        public static bool CanMoveDown(int[][] board)
        {
            var moved = PrivateMoveDown(board);
            return !IsBoardsEqual(board, moved);
        }
        public static bool CanMoveLeft(int[][] board)
        {
            var moved = PrivateMoveLeft(board);
            return !IsBoardsEqual(board, moved);
        }
        public static bool CanMoveRight(int[][] board)
        {
            var moved = PrivateMoveRight(board);
            return !IsBoardsEqual(board, moved);
        }

        public static int[][] PrivateMoveUp(int[][] board)
        {
            board = Rotate(board);
            board = Rotate(board);
            board = PrivateMoveDown(board);
            board = Rotate(board);
            board = Rotate(board);
            return board;
        }

        public static int[][] PrivateMoveRight(int[][] board)
        {
            board = Rotate(board);
            board = PrivateMoveDown(board);
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


        public static int[][] GenerateNewNumber(int[][] board)
        {
            List<string> Empty = new List<string>();
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (board[i][j] == 0)
                    {
                        Empty.Add(i.ToString() + j.ToString());
                    }
                }
            }
            if (Empty.Count == 0)
            {

            }
            else
            {
                Random rnd = new Random();
                int index = rnd.Next(0, Empty.Count);
                int[] variants = new int[] { 2, 2, 2, 4, 2, 2 };
                int variant = rnd.Next(0, variants.Length);
                string numberofcell = Empty[index];
                board[int.Parse(numberofcell[0].ToString())][int.Parse(numberofcell[1].ToString())] = variants[variant];
            }

            // Сначала зполучить пустые, потом выбирать из них
            return board;
        }
        public static bool PrivateIsEnd(int[][] board)
        {
            bool areEqual = IsBoardsEqual(board, GenerateNewNumber(board));
            if (areEqual && !CanMoveDown(board) && !CanMoveUp(board) && !CanMoveLeft(board) && !CanMoveRight(board))
                return true;
            else
                return false;
        }

        /*public static int[][] AfterTappingAndMoving(int[][] board)
        {
            board = GenerateNewNumber(board);
            IsEnd(board);
            return board;
        }*/

        #endregion

        public Game2048()
        {
            Board = GenerateNewNumber(Board);
            Board = GenerateNewNumber(Board);
        }

        public int[][] Board = new int[][] 
        {
             new int[] { 0, 0, 0, 0 },
             new int[] { 0, 0, 0, 0 },
             new int[] { 0, 0, 0, 0 },
             new int[] { 0, 0, 0, 0 }
        };

        private bool isEnd = false;

        public void MoveUp()
        {
            if (CanMoveUp(Board))
            {
                Board = PrivateMoveUp(Board);
                isEnd = PrivateIsEnd(Board);
                if (!isEnd)
                {
                    Board = GenerateNewNumber(Board);
                }
            }
        }

        public void MoveDown()
        {
            if (CanMoveDown(Board))
            {
                Board = PrivateMoveDown(Board);
                isEnd = PrivateIsEnd(Board);
                if (!isEnd)
                {
                    Board = GenerateNewNumber(Board);
                }
            }
        }

        public void MoveLeft()
        {
            if (CanMoveLeft(Board))
            {
                Board = PrivateMoveLeft(Board);
                isEnd = PrivateIsEnd(Board);
                if (!isEnd)
                {
                    Board = GenerateNewNumber(Board);
                }
            }
        }

        public void MoveRight()
        {
            if (CanMoveRight(Board))
            {
                Board = PrivateMoveRight(Board);
                isEnd = PrivateIsEnd(Board);
                if (!isEnd)
                {
                    Board = GenerateNewNumber(Board);
                }
            }
        }

        public bool IsEnd()
        {
            return isEnd;
        }

    }
}
