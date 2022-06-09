using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstWpfApp
{
    class Game
    {
        public Game()
        {
            Field = new string[] { "-", "-", "-", "-", "-", "-", "-", "-", "-" };
            MoveCounter = 0;
        }
        public string[] Field { get; private set; }
        public int MoveCounter { get; private set; }
        /*public void Next()
        {
            int currentIndex = Array.IndexOf(Players, Current);
            currentIndex++;
            if (currentIndex == Players.Length)
                currentIndex = 0;
            Current = Players[currentIndex];
        }*/

        public bool IsWin()
        {
            var winningCombinations = new int[8, 3] { { 0, 1, 2 }, { 3, 4, 5 }, { 6, 7, 8 }, { 0, 3, 6 }, { 1, 4, 7 }, { 2, 5, 8 }, { 0, 4, 8 }, { 2, 4, 6 } };

            for (int i = 0; i < 8; i++)
            {
                int index1 = winningCombinations[i, 0];
                int index2 = winningCombinations[i, 1];
                int index3 = winningCombinations[i, 2];
                if (Field[index1] == Field[index2] && Field[index2] == Field[index3])
                {
                    return true;
                }
            }
            return false;
        }
        public bool IsDraw()
        {
            if (MoveCounter > 8)
            {
                return true;
            }
            return false;
        }

    }
}
