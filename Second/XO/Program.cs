using System;

namespace XO
{

    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            while (true)
            {
                game.ShowField();
                int number = game.Current.EnterNumber();
                bool isValid = game.IsValid(number);
                if (!isValid)
                {
                    Console.WriteLine("Wrong meaning. Please try again");
                }
                else
                {
                    game.Move(number);
                    bool isWin = game.IsWin();
                    if (isWin)
                    {
                        game.ShowField();
                        Console.WriteLine($"Game over. Winner: {game.Current.Players}");
                        break;
                    }

                    bool isDraw = game.IsDraw();
                    if (isDraw)
                    {
                        game.ShowField();
                        Console.WriteLine("Game ended in a draw");
                        break;
                    }
                    game.Next();
                }
            }
        }
    }
}
