using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _2048_
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Button[] _buttons;
        public MainWindow()
        {
            InitializeComponent();
            
            _buttons = new Button[] { button1, button2, button3, button4, button5, button6, button7, button8, button9, button10, button11, button12, button13, button14, button15, button16 };
            Clear();
        }
        //rnd.Next(0, lines.Length)
        Random rnd = new Random();

        private void button_new_game_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < _buttons.Length; i++)
            {
                _buttons[i].Content = " ";
            }
            int r1 = rnd.Next(0, _buttons.Length);
            int r2 = rnd.Next(0, _buttons.Length);
            while (r1 == r2)
            {
                r2 = rnd.Next(0, _buttons.Length);
            }
            _buttons[r1].Content = 2;
            _buttons[r2].Content = 2;
            button_up.IsEnabled = true;
            button_down.IsEnabled = true;
            button_left.IsEnabled = true;
            button_right.IsEnabled = true;
        }

        private void button_up_Click(object sender, RoutedEventArgs e)
        {
            TapUp();
            AddNewNumber();
        }

        private void button_down_Click(object sender, RoutedEventArgs e)
        {
            TapDown();
            AddNewNumber();
        }

        private void button_left_Click(object sender, RoutedEventArgs e)
        {
            TapLeft();
            AddNewNumber();
        }

        private void button_right_Click(object sender, RoutedEventArgs e)
        {
            TapRight();
            AddNewNumber();
        }
        void TapLeft()
        {
            for (int j = 0; j < 3; j++)            
            {
                MoveLeft();
            }
            for (int i = 0; i < _buttons.Length; i++)
            {
                if (i != 3 && i != 7 && i != 11 && i != 15)
                    if (_buttons[i + 1].Content.ToString() == _buttons[i].Content.ToString() && _buttons[i + 1].Content.ToString() != " " && !_buttons[i].IsEnabled)
                    {
                        _buttons[i].Content = (int)_buttons[i + 1].Content + (int)_buttons[i].Content;
                        _buttons[i + 1].Content = " ";
                        _buttons[i].IsEnabled = true;
                    }
                    
            }
            MoveLeft();
            Clear();
        }
        void TapRight()
        {
            for(int j = 0; j < 3; j++)
            {
                MoveRight();
            }
            for (int i = _buttons.Length - 1; i > 0; i--)
            {
                if (i != 0 && i != 4 && i != 8 && i != 12)

                    if (_buttons[i - 1].Content.ToString() == _buttons[i].Content.ToString() && _buttons[i - 1].Content.ToString() != " " && !_buttons[i].IsEnabled)
                    {
                        _buttons[i].Content = (int)_buttons[i - 1].Content + (int)_buttons[i].Content;
                        _buttons[i - 1].Content = " ";
                        _buttons[i].IsEnabled = true;
                    }
            }
            MoveRight();
            Clear();
        }
       void TapUp()
        {
            for(int j = 0; j < 3; j++)
            {
                MoveUp();
            }
            for (int i = 0; i < _buttons.Length; i++)
            {
                if (i != 12 && i != 13 && i != 14 && i != 15)
                    if (_buttons[i + 4].Content.ToString() == _buttons[i].Content.ToString() && _buttons[i + 4].Content.ToString() != " " && !_buttons[i].IsEnabled)
                    {
                        _buttons[i].Content = (int)_buttons[i + 4].Content + (int)_buttons[i].Content;
                        _buttons[i + 4].Content = " ";
                        _buttons[i].IsEnabled = true;
                    }
            }
            MoveUp();
            Clear();
        }
        void TapDown()
        {
            for(int j = 0; j < 3; j++)
            {
                MoveDown();
            }
            for (int i = _buttons.Length - 1; i > 0; i--)
            {
                if (i != 0 && i != 1 && i != 2 && i != 3)
                    if (_buttons[i - 4].Content.ToString() == _buttons[i].Content.ToString() && _buttons[i - 4].Content.ToString() != " " && !_buttons[i].IsEnabled)
                    {
                        _buttons[i].Content = (int)_buttons[i - 4].Content + (int)_buttons[i].Content;
                        _buttons[i - 4].Content = " ";
                        _buttons[i].IsEnabled = true;
                    }
            }
            MoveDown();
            Clear();
        }
        int count = 0;
        void AddNewNumber()
        {
            int[] numbers = new int[] { 2, 2, 4, 2, 2 };
            int index = rnd.Next(0, numbers.Length);
            int button_index = rnd.Next(0, _buttons.Length);
            while (_buttons[button_index].Content.ToString() != " ")
            {
                count = 0;
                foreach (var item in _buttons)
                {
                    if (item.Content.ToString() != " ")
                        count++;
                }
                if (count == 16)
                {
                    EndGame();
                    break;
                }
                button_index = rnd.Next(0, _buttons.Length);
            }
            _buttons[button_index].Content = numbers[index];
        }
        void MoveLeft()
        {
            for (int i = 0; i < _buttons.Length; i++)
            {
                if (i != 0 && i != 4 && i != 8 && i != 12)
                {
                    while (_buttons[i - 1].Content.ToString() == " " && _buttons[i].Content.ToString() != " ")
                    {
                        _buttons[i - 1].Content = _buttons[i].Content;
                        _buttons[i].Content = " ";
                        if (i != 1 && i != 5 && i != 9 && i != 13)
                        {
                            i--;
                        }
                    }
                }

            }
        }
        void MoveRight()
        {
            for (int i = 0; i < _buttons.Length; i++)
            {
                if (i != 3 && i != 7 && i != 11 && i != 15)
                {
                    while (_buttons[i + 1].Content.ToString() == " " && _buttons[i].Content.ToString() != " ")
                    {
                        _buttons[i + 1].Content = _buttons[i].Content;
                        _buttons[i].Content = " ";
                        if (i != 2 && i != 6 && i != 10 && i != 14)
                        {
                            i++;
                        }
                    }
                }
            }
        }
        void MoveUp()
        {
            for (int i = 0; i < _buttons.Length; i++)
            {
                if (i != 0 && i != 1 && i != 2 && i != 3)
                {
                    while (_buttons[i - 4].Content.ToString() == " " && _buttons[i].Content.ToString() != " ")
                    {
                        _buttons[i - 4].Content = _buttons[i].Content;
                        _buttons[i].Content = " ";
                        if (i != 4 && i != 5 && i != 6 && i != 7)
                        {
                            i -= 4;
                        }
                    }
                }

            }
        }
        void MoveDown()
        {
            for (int i = 0; i < _buttons.Length; i++)
            {
                if (i != 12 && i != 13 && i != 14 && i != 15)
                {
                    while (_buttons[i + 4].Content.ToString() == " " && _buttons[i].Content.ToString() != " ")
                    {
                        _buttons[i + 4].Content = _buttons[i].Content;
                        _buttons[i].Content = " ";
                        if (i != 8 && i != 9 && i != 10 && i != 11)
                        {
                            i += 4;
                        }
                    }
                }

            }
        }
        void Clear()
        {
            for (int i = 0; i < _buttons.Length; i++)
            {
                _buttons[i].IsEnabled = false;
            }
        }
        void EndGame()
        {
            button_up.IsEnabled = false;
            button_down.IsEnabled = false;
            button_left.IsEnabled = false;
            button_right.IsEnabled = false;
            MessageBox.Show("Game Over");
        }
    }
    // много раз сдвинуть раз сложить и еще раз сдвинуть
}
