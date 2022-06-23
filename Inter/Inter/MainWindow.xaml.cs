﻿using System;
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
    public partial class MainWindow : Window, IView
    {
        Button[] Buttons;
        public MainWindow()
        {
            InitializeComponent();
            game = new Game(this);
            Buttons = new Button[] { button1, button2, button3, button4, button5, button6, button7, button8, button9 };
        }

        Game game;

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            game.UserClick(0);
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            game.UserClick(1);
        }

        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            game.UserClick(2);
        }

        private void Button4_Click(object sender, RoutedEventArgs e)
        {
            game.UserClick(3);

        }

        private void Button5_Click(object sender, RoutedEventArgs e)
        {
            game.UserClick(4);
        }

        private void Button6_Click(object sender, RoutedEventArgs e)
        {
            game.UserClick(5);
        }

        private void Button7_Click(object sender, RoutedEventArgs e)
        {
            game.UserClick(6);
        }

        private void Button8_Click(object sender, RoutedEventArgs e)
        {
            game.UserClick(7);
        }

        private void Button9_Click(object sender, RoutedEventArgs e)
        {
            game.UserClick(8);
        }
        
        
        public void EndGame()
        {
            if (game.IsWin())
            {
                MessageBox.Show($"Game over. Winner: {game.CurrentPlayer()}");
                Block();
            }
            else if (game.IsDraw())
            {
                MessageBox.Show("Game ended in a draw");
                Block();
            }
        }
        void StartNewGame()
        {
            game = new Game(this);
            foreach (var item in Buttons)
            {
                item.IsEnabled = true;
            }
            TranslateField();
        }
        public void Block()
        {
            foreach (var item in Buttons)
            {
                item.IsEnabled = false;
            }
        }

        private void newgamebutton_Click(object sender, RoutedEventArgs e)
        {
            StartNewGame();
        }

        public void UpdateView()
        {
            // 2 строчки  + нижння сбда |done
            // 9 строчек кнопка = клетка поля |done
            

            TranslateField();
            foreach (var item in Buttons)
            {
                if (item.Content.ToString() == "X" || item.Content.ToString() == "O")
                {
                    item.IsEnabled = false;
                }
            }
            EndGame();
        }
        public void TranslateField()
        {
            button1.Content = game.Field[0];
            button2.Content = game.Field[1];
            button3.Content = game.Field[2];
            button4.Content = game.Field[3];
            button5.Content = game.Field[4];
            button6.Content = game.Field[5];
            button7.Content = game.Field[6];
            button8.Content = game.Field[7];
            button9.Content = game.Field[8];
        }
        
    }
}
