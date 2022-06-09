using System;
using System.Collections.Generic;
using System.Text;

namespace StickGame
{
    class ComputerPlayer : Player
    {
        public ComputerPlayer()
            : base()
        {
            Players = Players.Computer;
        }

        public override int GetSticks(int sticksCount)
        {
            //Console.WriteLine("Now is's computer's turn. Please wait");
            int howmanytakes = (sticksCount % 4 == 1) ? 1 : ((sticksCount - 1) % 4);
            //Console.WriteLine($"Computer takes {howmanytakes}");
            return howmanytakes;
        }
    }
}
