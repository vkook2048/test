using System;
using System.Collections.Generic;
using System.Text;

namespace StickGame
{
    class Game
    {
        public Game()
        {
            SticksCount = 10;
            Players =  new Player[] { new UserPlayer(), new ComputerPlayer() };
            Current = Players[0];
        }

        public int SticksCount { get; private set; }
        public Player[] Players { get; private set; }

        public Player Current { get; private set; }

        private void Next()
        {
            int currentIndex = Array.IndexOf(Players, Current);
            currentIndex++;
            if (currentIndex == Players.Length)
                currentIndex = 0;
            Current = Players[currentIndex];
        }
        public void Take(int count)
        {
            if (count <= 0 || count > 3 || count > SticksCount)
                throw new Exception("Wrong sticks count");

            SticksCount = SticksCount - count;
            Next();
        }

        public bool IsEnd()
        {
            return SticksCount == 0;
        }
    }
}
