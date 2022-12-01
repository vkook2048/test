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
                this.Closing -= Window_Closing;
                DialogResult = true;
            }
            else
            {
                MessageBox.Show("Не все поля заполнены верно");
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string message = "Do you want to save changes before closing this window?";
            var result = MessageBox.Show(message, "", MessageBoxButton.YesNoCancel);
            if (result == MessageBoxResult.Cancel)
            {
                e.Cancel = true;
            }
            else if (result == MessageBoxResult.No)
            {
                DialogResult = false;
            }
            else
            {
                DialogResult = true;
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Closing -= Window_Closing;
            DialogResult = false;
        }
    }
}
