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

namespace Calculator
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

        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            textblock1.Text = "";
            error1.Text = "";
            try
            {
                var expq = Expression.Parse(textbox1.Text);
                double val = expq.Calculate();
                textblock1.Text = val.ToString();
            }
            catch (Exception exc)
            {
                textblock1.Text = "ERR";
                error1.Text = exc.Message;
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            textbox1.Text = "";
            textblock1.Text = "";
            error1.Text = "";
        }
    }
}
