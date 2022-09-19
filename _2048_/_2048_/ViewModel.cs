using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;

namespace _2048_
{
    class ViewModel : INotifyPropertyChanged
    {
        Item[][] Items;

        Game2048 _game;

        public ViewModel()
        {
            Items = new Item[][] { new Item[] { Item1, Item2, Item3, Item4 }, new Item[] { Item5, Item6, Item7, Item8 }, new Item[] { Item9, Item10, Item11, Item12 }, new Item[] { Item13, Item14, Item15, Item16 } };
            UpCommand = new MyCommand(_UpCommand);
            DownCommand = new MyCommand(_DownCommand);
            LeftCommand = new MyCommand(_LeftCommand);
            RightCommand = new MyCommand(_RightCommand);
            
        }

        /*
        public void ConnectField()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Items[i][j].Number = _game.Board[i][j].ToString();
                    if (Items[i][j].Number.ToString() == "0")
                    {
                        Items[i][j].Number = "";
                    }
                }
            }
            
        }
        /*
        /*
          if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("IsListShowed"));
                }
        */

        public void StartNewGame()
        {
            _game = new Game2048();
        }
        
        public void _UpCommand(object parameter)
        {
            _game.MoveUp();
        }

        public void _DownCommand(object parameter)
        {
            _game.MoveDown();
        }

        public void _LeftCommand(object parameter)
        {
            _game.MoveLeft();
        }

        public void _RightCommand(object parameter)
        {
            _game.MoveRight();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        /*if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs();
                }
        */

        public Item Item1 { get; set; } = new Item();
        public Item Item2 { get; set; } = new Item();
        public Item Item3 { get; set; } = new Item();
        public Item Item4 { get; set; } = new Item();
        public Item Item5 { get; set; } = new Item();
        public Item Item6 { get; set; } = new Item();
        public Item Item7 { get; set; } = new Item();
        public Item Item8 { get; set; } = new Item();
        public Item Item9 { get; set; } = new Item();
        public Item Item10 { get; set; } = new Item();
        public Item Item11 { get; set; } = new Item();
        public Item Item12 { get; set; } = new Item();
        public Item Item13 { get; set; } = new Item();
        public Item Item14 { get; set; } = new Item();
        public Item Item15 { get; set; } = new Item();
        public Item Item16 { get; set; } = new Item();

        public MyCommand UpCommand { get; set; }
        public MyCommand DownCommand { get; set; }
        public MyCommand LeftCommand { get; set; }
        public MyCommand RightCommand { get; set; }
    }

    public class Item : INotifyPropertyChanged
    {
        public string Number { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    public class MyCommand : ICommand
    {
        public delegate void FuncExecute(object parameter);
        public delegate bool FuncCanExecute(object parameter);


        private FuncExecute _funcExecute = null;
        private FuncCanExecute _funcCanExecute = null;

        public MyCommand()
        {

        }

        public MyCommand(FuncExecute funcExecute)
        {
            _funcExecute = funcExecute;
        }

        public MyCommand(FuncExecute funcExecute, FuncCanExecute funcCanExecute)
        {
            _funcExecute = funcExecute;
            _funcCanExecute = funcCanExecute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            if (_funcCanExecute == null)
            {
                return true;
            }
            return _funcCanExecute(parameter);
        }

        public void Execute(object parameter)
        {
            if (_funcExecute == null)
            {
                return;
            }

            _funcExecute(parameter);
        }
    }
}
