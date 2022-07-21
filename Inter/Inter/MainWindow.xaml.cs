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
    // xaml

    // template для кнопок поля одинаковый

    // Stack panel (orientation)
    // Grid (column definition, row definition, grid.row)
    // переопределение кнопки

    // TextBlock (font fontweight fontstyle fontsize heigth width, horizontal aligment, vertical aligment)
    // TexBox
    // Button 
    // Border (corner radius)
    // scroll view

    // фиксированный размер окна

    // подсвечивается последний ход
    // крестики нолики рисовать либо картинка либо графика
    // mouse over эффект (подсвет кнопки при наведени)
    // поле (красивое)
    // линия выигрыша
    // надпись о выигыше в самом окне
    // кнопка новой игры картинкой





    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IView
    {
        Button[] Buttons;
        Line[] Lines;
        int index;
        public MainWindow()
        {
            InitializeComponent();
            Buttons = new Button[] { button1, button2, button3, button4, button5, button6, button7, button8, button9 };
            Lines = new Line[] { line1, line2, line3, line4, line5, line6, line7, line8 };
            Loaded += OnWindowLoaded;
        }

        private void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            _controller.StartNewGame(this);
        }

        Controller _controller = new Controller();

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            _controller.UserClick(Array.IndexOf(Buttons, btn));
        }


        public void EndGame(Game game)
        {
            if (game.IsWin(ref index))
            {
                Lines[index].Visibility = Visibility.Visible;
                TopText.Text = $"WINNER: {game.CurrentPlayer()}!";
                //MessageBox.Show($"Game over. Winner: {game.CurrentPlayer()}");
            }
            else if (game.IsDraw())
            {
                TopText.Text = "DRAW";
                //MessageBox.Show("Game ended in a draw");
            }
        }
        void StartNewGame()
        {
            _controller.StartNewGame(this);
        }
        private void newgamebutton_Click(object sender, RoutedEventArgs e)
        {
            StartNewGame();
        }

        public void UpdateView(Game game)
        {
            bool isEnd = game.IsDraw() || game.IsWin(ref index);

            for (int i = 0; i < Buttons.Length; i++)
            {
                Buttons[i].Content = game.Field[i];

                if (!isEnd && game.Field[i].Length == 0)
                    Buttons[i].Content = game.CurrentPlayer();

                if (game.Field[i] == "X" || game.Field[i] == "O" || isEnd)
                {
                    Buttons[i].IsEnabled = false;
                    
                }
                else
                {
                    Buttons[i].IsEnabled = true;
                    Lines[index].Visibility = Visibility.Collapsed;
                    TopText.Text = "HELLO";
                }
            }
            EndGame(game);
        }

        
    }
}
