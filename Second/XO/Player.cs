using System;
using System.Collections.Generic;
using System.Text;

namespace XO
{
    class Player
    {
        public Player(Players player)
        {
            Players = player;
        }

        public Players Players { get; protected set; }

        public  int EnterNumber()
        {
            Console.WriteLine($"Now goes {Players}");
            return int.Parse(Console.ReadLine());
        }

        
    }
}
