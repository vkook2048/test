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

namespace Inter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();           
        }

        /*private void Bch_Click(object sender, RoutedEventArgs e)
        {
            Bch.Content = "X";

            //MessageBox.Show("привет");
        }*/

        string[] players = new string[] { "X", "O" };
        int current = 0;
        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            button1.Content = players[current];
            button1.IsEnabled = false;
            Field[0] = players[current];
            EndGame();
            ChangePlayer();
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            button2.Content = players[current];
            button2.IsEnabled = false;
            Field[1] = players[current];
            EndGame();
            ChangePlayer();
        }

        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            button3.Content = players[current];
            button3.IsEnabled = false;
            Field[2] = players[current];
            EndGame();
            ChangePlayer();
        }

        private void Button4_Click(object sender, RoutedEventArgs e)
        {
            button4.Content = players[current];
            button4.IsEnabled = false;
            Field[3] = players[current];
            EndGame();
            ChangePlayer();
        }

        private void Button5_Click(object sender, RoutedEventArgs e)
        {
            button5.Content = players[current];
            button5.IsEnabled = false;
            Field[4] = players[current];
            EndGame();
            ChangePlayer();
        }

        private void Button6_Click(object sender, RoutedEventArgs e)
        {
            button6.Content = players[current];
            button6.IsEnabled = false;
            Field[5] = players[current];
            EndGame();
            ChangePlayer();
        }

        private void Button7_Click(object sender, RoutedEventArgs e)
        {
            button7.Content = players[current];
            button7.IsEnabled = false;
            Field[6] = players[current];
            EndGame();
            ChangePlayer();
        }

        private void Button8_Click(object sender, RoutedEventArgs e)
        {
            button8.Content = players[current];
            button8.IsEnabled = false;
            Field[7] = players[current];
            EndGame();
            ChangePlayer();
        }

        private void Button9_Click(object sender, RoutedEventArgs e)
        {
            button9.Content = players[current];
            button9.IsEnabled = false;
            Field[8] = players[current];
            EndGame();
            ChangePlayer();
        }
        void ChangePlayer()
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
        string[] Field = new string[] { "-", "-", "-", "-", "-", "-", "-", "-", "-" };
        public bool IsWin()
        {
            var winningCombinations = new int[8, 3] { { 0, 1, 2 }, { 3, 4, 5 }, { 6, 7, 8 }, { 0, 3, 6 }, { 1, 4, 7 }, { 2, 5, 8 }, { 0, 4, 8 }, { 2, 4, 6 } };

            for (int i = 0; i < 8; i++)
            {
                int index1 = winningCombinations[i, 0];
                int index2 = winningCombinations[i, 1];
                int index3 = winningCombinations[i, 2];
                if (Field[index1] == Field[index2] && Field[index2] == Field[index3] && Field[index2] != "-")
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
                if (Field[i] == "-")
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
        void EndGame()
        {
            if (IsWin())
            {
                MessageBox.Show(this, $"Game over. Winner: {players[current]}");
                Block();
            }
            else if (IsDraw())
            {
                MessageBox.Show(this, "Game ended in a draw");
                Block();
            }
        }
        void StartNewGame()
        {
            button1.IsEnabled = true;
            button2.IsEnabled = true;
            button3.IsEnabled = true;
            button4.IsEnabled = true;
            button5.IsEnabled = true;
            button6.IsEnabled = true;
            button7.IsEnabled = true;
            button8.IsEnabled = true;
            button9.IsEnabled = true;
            button1.Content = "";
            button2.Content = "";
            button3.Content = "";
            button4.Content = "";
            button5.Content = "";
            button6.Content = "";
            button7.Content = "";
            button8.Content = "";
            button9.Content = "";
            current = 0;
            Field = new string[] { "-", "-", "-", "-", "-", "-", "-", "-", "-" };
        }
        void Block()
        {
            button1.IsEnabled = false;
            button2.IsEnabled = false;
            button3.IsEnabled = false;
            button4.IsEnabled = false;
            button5.IsEnabled = false;
            button6.IsEnabled = false;
            button7.IsEnabled = false;
            button8.IsEnabled = false;
            button9.IsEnabled = false;
        }

        private void newgamebutton_Click(object sender, RoutedEventArgs e)
        {
            StartNewGame();
        }
    }
}
