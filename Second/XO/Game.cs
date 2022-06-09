using System;
using System.Collections.Generic;
using System.Text;

namespace XO
{
    enum Players
    {
        x,
        o
    }

    class Game
    {
        public Game()
        {
            Field = new string[] { "-", "-", "-", "-", "-", "-", "-", "-", "-" };
            Players = new Player[] { new Player(XO.Players.x), new Player(XO.Players.o)};
            MoveCounter = 0;
            Current = Players[0];
        }
        public string[] Field { get; private set; }
        public Player[] Players { get; set; }
        public Player Current { get; set; } 
        public int MoveCounter { get; private set; }

        public void ShowField()
        {
            for (int i = 1; i < Field.Length;)
            {
                Console.WriteLine($"{Field[i-1]} {Field[i]} {Field[i+1]}");
                i += 3;
            }
            Console.WriteLine();
        }

        public bool IsWin()
        {
            var winningCombinations = new int[8, 3] { { 0, 1, 2 }, { 3, 4, 5 }, { 6, 7, 8 }, { 0, 3, 6 }, { 1, 4, 7 }, { 2, 5, 8 }, { 0, 4, 8 }, { 2, 4, 6 } };

            for (int i = 0; i < 8; i++)
            {
                int index1 = winningCombinations[i, 0];
                int index2 = winningCombinations[i, 1];
                int index3 = winningCombinations[i, 2];
                if (Field[index1] == Field[index2] && Field[index2] == Field[index3] && Field[index2] != "-")
                {
                    return true;
                }
            }
            return false;
        }
        public bool IsDraw()
        {
            if(MoveCounter > 8)
            {
                return true;
            }
            return false;
        }

        public void Next()
        {
            int currentIndex = Array.IndexOf(Players, Current);
            currentIndex++;
            if (currentIndex == Players.Length)
                currentIndex = 0;
            Current = Players[currentIndex];
        }
        public void Move(int number)
        {
            Field[number] = Current.Players.ToString();
        }
        public bool IsValid(int number)
        {
            if (Field[number] == "-")
            {
                return true;
            }
            return false;
        }


    }
}
