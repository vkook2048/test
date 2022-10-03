using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SQLiteForMovement
{
    /// <summary>
    /// Interaction logic for OperationWindow.xaml
    /// </summary>
    public partial class OperationWindow : Window
    {
        
        public Movement Movement { get; private set; }        
        public OperationWindow(Movement movement)
        {
            InitializeComponent();
            Movement = movement;
            DataContext = Movement;
        }

        void Accept_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(Movement.Date) && Movement.Shop != null && Movement.Product != null && !string.IsNullOrEmpty(Movement.Operation) && Movement.Count > 0 && Movement.Price > 0)
            {
                DialogResult = true;
            }
            else
            {
                MessageBox.Show("Не все поля заполнены верно");
            }
        }
    }
}
