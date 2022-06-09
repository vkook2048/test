using System;
using System.Collections.Generic;
using System.Text;

namespace StickGame
{
    abstract class Player
    {
        protected Player()
        {

        }

        public Players Players { get; protected set; }

        public abstract int GetSticks(int sticksCount);


    }
}
