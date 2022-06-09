using System;
using System.Collections.Generic;
using System.Text;

namespace StickGame
{
    class UserPlayer : Player
    {
        public UserPlayer()
            :base()
        {
            Players = Players.User;
        }

        public override int GetSticks(int sticksCount)
        {
            Console.WriteLine($"Sticks left: {sticksCount}");
            Console.WriteLine("Please enter how many sticks you want to take (from 1 to 3)");
            return int.Parse(Console.ReadLine());
        }
    }
}
