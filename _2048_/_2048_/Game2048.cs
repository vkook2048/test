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

        public static int[][] MoveDown(int[][] board)
        {
            // TODO
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
            // TODO
            // если что это "поворот"
            int[][] helper = new int[][] {
            new int[] { 0, 0, 0, 0 },
            new int[] { 0, 0, 0, 0 },
            new int[] { 0, 0, 0, 0 },
            new int[] { 0, 0, 0, 0 } };

           /*for (int i = 0; i < 4; i++)
            {
                //for (int j = 4; j > 0; j--)
                {
                    helper[3 - i][3] = board[i][0];
                    helper[3 - i][2] = board[i][1];
                    helper[3 - i][1] = board[i][2];
                    helper[3 - i][0] = board[i][3];
                }
            }*/

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

    }
}
