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
using Microsoft.EntityFrameworkCore;


namespace SQLiteForMovement
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ApplicationContext db = new ApplicationContext();
        public MainWindow()
        {
            InitializeComponent();

            Loaded += MainWindow_Loaded;
        }

        // при загрузке окна
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // гарантируем, что база данных создана
            db.Database.EnsureCreated();
            // загружаем данные из БД
            db.Movements.Load();
            db.Products.Load();
            db.Shops.Load();
            // и устанавливаем данные в качестве контекста
            DataContext = db.Movements.Local.ToObservableCollection();
        }
        // добавление
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            OperationWindow OperationWindow = new OperationWindow(new Movement() { Context = db });
            OperationWindow.Owner = this;
            if (OperationWindow.ShowDialog() == true)
            {
                Movement Movement = OperationWindow.Movement;
                
                 db.Movements.Add(Movement);
                 db.SaveChanges();
                
            }
        }
        // редактирование
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            // получаем выделенный объект
            Movement movement = movementsList.SelectedItem as Movement;
            // если ни одного объекта не выделено, выходим
            if (movement is null) return;
            var editCopy = new Movement() { Context = db };
            movement.CopyTo(editCopy);
            OperationWindow operationWindow = new OperationWindow(editCopy);
            if (operationWindow.ShowDialog() == true )
            {
                // получаем измененный объект
                movement = db.Movements.Find(operationWindow.Movement.Id);
                if (movement != null)
                {
                        editCopy.CopyTo(movement);
                        db.SaveChanges();
                        movementsList.Items.Refresh();
                }
            }
        }
        // удаление
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            // получаем выделенный объект
            Movement movement = movementsList.SelectedItem as Movement;
            // если ни одного объекта не выделено, выходим
            if (movement is null) return;
            db.Movements.Remove(movement);
            db.SaveChanges();
        }

        private void Shops_Click(object sender, RoutedEventArgs e)
        {
            ShopWindow shopWindow = new ShopWindow(db);
            if (shopWindow.ShowDialog() == true)
            {

            }
        }

        private void Products_Click(object sender, RoutedEventArgs e)
        {
            ProductWindow productWindow = new ProductWindow(db);
            if (productWindow.ShowDialog() == true)
            {

            }
        }
    }
}
/*
        
*/
