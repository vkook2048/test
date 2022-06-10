using System;
using System.Collections.Generic;
using System.IO;
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

namespace Words
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TextBlock[] _letters;
        Button[] _buttons;

        public MainWindow()
        {
            InitializeComponent();

            _letters = new TextBlock[] { letter1, letter2, letter3, letter4, letter5, letter6, letter7, letter8, letter9, letter10, letter11, letter12, letter13, letter14, letter15, letter16, letter17, letter18};
            _buttons = new Button[] { button1, button2, button3, button4, button5, button6, button7, button8, button9, button10, button11, button12, button13, button14, button15, button16, button17, button18, button19, button20, button21, button22, button23, button24, button25, button26, button27, button28, button29, button30, button31, button32, button33 };
        }
        string[] lines = File.ReadAllLines(@"D:\Downloads\zagruzki s c\WordsStockRus.txt");
        Random rnd = new Random();
        string word = "";
        int left = 6;


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            btn.IsEnabled = false;
            int find = word.IndexOf(btn.Content.ToString().ToLower());

            if (find == -1)
            {
                left--;
                MessageBox.Show("There is no such letter in the word.");
            }
            else
            {
                while (find != -1)
                {
                    _letters[find].Text = btn.Content.ToString();
                    find = word.IndexOf(btn.Content.ToString().ToLower(), find + 1);
                }
                
            }
            Attempts.Text = $"Attempts left: {left}";
            IfEnd();
        }

        private void StartNewGame_Click(object sender, RoutedEventArgs e)
        {
            word = lines[rnd.Next(0, lines.Length)];
            for (int i = 0; i < _letters.Length; i++)
            {
                _letters[i].Visibility = Visibility.Visible;
                _letters[i].Text = "-";
            }
            for (int i = word.Length; i < _letters.Length; i++)
                _letters[i].Visibility = Visibility.Collapsed;
            for (int i = 0; i < _buttons.Length; i++)
            {
                _buttons[i].IsEnabled = true;
            }
            left = 6;
            Attempts.Text = $"Attempts left: {left}";
        }
        void IfEnd()
        {
            int notemptyspace = 0;
            for (int i = 0; i < _letters.Length; i++)
            {
                if (_letters[i].Text != "-")
                {
                    notemptyspace++;
                    
                }
               
            }
            if (left < 1 || notemptyspace == word.Length)
            {
                if (left < 1)
                {
                    MessageBox.Show($"Game over. Please try again. Word was: {word}");
                }
                else if (notemptyspace == word.Length)
                {
                    MessageBox.Show("You win!");
                }
                for (int i = 0; i < _buttons.Length; i++)
                {
                    _buttons[i].IsEnabled = false;
                }
            }
            
        }
    }
}
