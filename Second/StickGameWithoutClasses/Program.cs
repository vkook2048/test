using System;

namespace StickGameWithoutClasses
{
    class Program
    {
        static void Main(string[] args)
        {
            int sticks = 10;
            //int stiks = int.Parse(Console.ReadLine());
            int howmanytakes = 0;
            Random rnd = new Random();
            bool isWin = false;
            int who = 1;
            while (!isWin)
            {
                howmanytakes = EnteringTheNumberOfSticks(who, sticks);
                if ((howmanytakes > sticks && who == 1) || howmanytakes > 3)
                {
                    Console.WriteLine("Wrong meaning. Please try again");
                }
                else if (howmanytakes > sticks && who == 0)
                {

                }
                else
                {
                    if (who == 1)
                    {
                        who--;
                    }
                    else
                    {
                        who++;
                    }
                    sticks -= howmanytakes;
                    isWin = IsWin(sticks);
                }
            }
            GameOver(who);

        }

        static int EnteringTheNumberOfSticks(int who, int stiks)
        {
            if (who == 0)
            {
                Console.WriteLine("Now is's computer's turn. Please wait");
                int howmanytakes = (stiks % 4 == 1) ? 1 : ((stiks - 1) % 4);
                Console.WriteLine($"Computer takes {howmanytakes}");
                return howmanytakes;
            }
            else
            {
                Console.WriteLine($"Sticks left: {stiks}");
                Console.WriteLine("Please enter how many sticks you want to take (from 1 to 3)");
                return int.Parse(Console.ReadLine());
            }
        }
        static bool IsWin(int sticks)
        {
            if (sticks == 0)
            {
                return true;
            }
            return false;
        }
        static void GameOver(int who)
        {
            Console.WriteLine("Game over. ");
            if (who == 1)
            {
                Console.Write("User wins!");
            }
            else
            {
                Console.Write("Computer wins!");
            }
        }
    }
}
