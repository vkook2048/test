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
    public partial class MainWindow : Window, IView
    {
        Button[] Buttons;
        public MainWindow()
        {
            InitializeComponent();
            Buttons = new Button[] { button1, button2, button3, button4, button5, button6, button7, button8, button9 };
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
            if (game.IsWin())
            {
                MessageBox.Show($"Game over. Winner: {game.CurrentPlayer()}");
            }
            else if (game.IsDraw())
            {
                MessageBox.Show("Game ended in a draw");
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
            bool isEnd = game.IsDraw() || game.IsWin();

            TranslateField(game);
            foreach (var item in Buttons)
            {
                if (item.Content.ToString() == "X" || item.Content.ToString() == "O" || isEnd)
                {
                    item.IsEnabled = false;
                }
                else
                {
                    item.IsEnabled = true;
                }
            }
            EndGame(game);
        }
        public void TranslateField(Game game)
        {
            for (int i = 0; i < Buttons.Length; i++)
            {
                Buttons[i].Content = game.Field[i];
            }
        }
        
    }
}
