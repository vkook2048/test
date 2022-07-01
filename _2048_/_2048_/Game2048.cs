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
            return board;
        }

        public static int[][] Rotate(int[][] board)
        {
            // TODO
            return board;
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
