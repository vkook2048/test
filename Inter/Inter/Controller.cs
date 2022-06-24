using System;
using System.Collections.Generic;
using System.Text;

namespace Inter
{
    class Controller
    {
        public Game Game { get; private set; }

        public void StartNewGame(IView view)
        {
            Game = new Game(view);
            Game.Start();
        }

        public void UserClick(int index)
        {
            Game.UserClick(index);
        }
    }
}
