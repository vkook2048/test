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

namespace FirstWpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        List<Button> _xoButtons = new List<Button>();
        Game _game = new Game();

        public MainWindow()
        {
            InitializeComponent();

            _xoButtons.Add(button1);
            _xoButtons.Add(button2);
            _xoButtons.Add(button3);
            _xoButtons.Add(button4);
            _xoButtons.Add(button5);
            _xoButtons.Add(button6);
            _xoButtons.Add(button7);
            _xoButtons.Add(button8);
            _xoButtons.Add(button9);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            textName.Text = "0000";
            textName.Foreground = Brushes.Aquamarine;

            TextBlock text = combobox.SelectedItem as TextBlock;
            if (text != null)
                MessageBox.Show(text.Text);

            Window1 wnd = new Window1();
            //wnd.ShowDialog();
            wnd.Show();
        }

        bool turnX = true;

        private void Button_Click_0(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;


            btn.Content = turnX ? "X" : "O";
            turnX = !turnX;
            btn.IsEnabled = false;

            int index = _xoButtons.IndexOf(btn);
            //MessageBox.Show($"button{index} pressed");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            foreach (var button in _xoButtons)
            {
                button.Content = "";
                button.IsEnabled = true;
            }

            turnX = true;
        }
    }
}
