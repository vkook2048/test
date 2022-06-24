using System;
using System.Collections.Generic;
using System.Text;

namespace Inter
{
    public class Game
    {
        string[] players { get; set; }
        
        int current { get; set; }
        public string[] Field { get; set; }

        private IView _view;

        public Game(IView view)
        {
            players = new string[] { "X", "O" };
            current = 0;
            Field = new string[] { "", "", "", "", "", "", "", "", "" };
            _view = view;
        }


        public string CurrentPlayer()
        {
            return players[current];
        }
        private void ChangePlayer()
        {
            if (current == 0)
            {
                current++;
            }
            else
            {
                current--;
            }

        }
        public bool IsWin()
        {
            var winningCombinations = new int[8, 3] { { 0, 1, 2 }, { 3, 4, 5 }, { 6, 7, 8 }, { 0, 3, 6 }, { 1, 4, 7 }, { 2, 5, 8 }, { 0, 4, 8 }, { 2, 4, 6 } };

            for (int i = 0; i < 8; i++)
            {
                int index1 = winningCombinations[i, 0];
                int index2 = winningCombinations[i, 1];
                int index3 = winningCombinations[i, 2];
                if (Field[index1] == Field[index2] && Field[index2] == Field[index3] && Field[index2] != "")
                {
                    return true;
                }
            }
            return false;
        }
        public bool IsDraw()
        {
            int count = 0;
            for (int i = 0; i < Field.Length; i++)
            {
                if (Field[i] == "")
                {
                    count++;
                    break;
                }
            }
            if (count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void UserClick(int index)
        {
            if (index > 8 || index < 0  || Field[index] != "")
            {
                throw new Exception("problem with index");
            }
            else
            {
                Field[index] = CurrentPlayer();
                if (!IsWin() && !IsDraw())
                {
                    ChangePlayer();
                }
                if (_view != null)
                    _view.UpdateView(this);

            }
            
        }

        public void Start()
        {
            _view.UpdateView(this);
        }

    }
}
