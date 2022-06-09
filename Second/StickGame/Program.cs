using System;

namespace StickGame
{
    enum Players
    {
        User,
        Computer
    }

    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            bool isEnd = false;

            while (!isEnd)
            {
                int sticks = game.SticksCount;
                Console.WriteLine($"Now is's {game.Current.Players}'s turn.");
                int howmanytakes = game.Current.GetSticks(game.SticksCount);
                Console.WriteLine($"{game.Current.Players} takes {howmanytakes}");
                if (howmanytakes > 3 || howmanytakes > sticks)
                {
                    Console.WriteLine("Wrong meaning. Please try again");
                }
                else
                {
                    game.Take(howmanytakes);
                    isEnd = game.IsEnd();
                }
            }

            GameOver(game.Current);
        }  

        static void GameOver(Player current)
        {
            Console.WriteLine("Game over. ");
            Console.Write(current.Players + " wins!");
        }
    }
}
