using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for ProductWindow.xaml
    /// </summary>
    public partial class ProductWindow : Window
    {
        ApplicationContext ApplicationContext { get; set; }
        public ProductWindow(ApplicationContext db)
        {
            ApplicationContext = db;
            InitializeComponent();
            Loaded += ProductWindow_Loaded;
        }

        public ObservableCollection<Product> ProductsCopy { get; set; }


        // при загрузке окна
        private void ProductWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // гарантируем, что база данных создана

            // загружаем данные из БД
            ApplicationContext.Products.Load();
            // и устанавливаем данные в качестве контекста
            ProductsCopy = new ObservableCollection<Product>();
            foreach (var product in ApplicationContext.Products)
            {
                ProductsCopy.Add(product.Clone());
            }
            DataContext = ProductsCopy;
        }
        // добавление
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            Product product = new Product();
            ProductsCopy.Add(product);
            productsList.SelectedItem = product;
        }
        // удаление
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            // получаем выделенный объект
            Product product = productsList.SelectedItem as Product;
            // если ни одного объекта не выделено, выходим
            if (product is null) return;
            ProductsCopy.Remove(product);
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            int count = 0;
            foreach (var item in ProductsCopy)
            {
                if (!string.IsNullOrEmpty(item.Department) && item.Count > 0 && item.VendorId > 0 && !string.IsNullOrEmpty(item.Unit) && !string.IsNullOrEmpty(item.Name))
                {
                    count++;
                }
            }
            if (count == ProductsCopy.Count)
            {
                try
                {
                    SaveAdded(ApplicationContext, ProductsCopy);
                    SaveDeteled(ApplicationContext, ProductsCopy);
                    SaveEdited(ApplicationContext, ProductsCopy);
                    ApplicationContext.SaveChanges();
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Ошибка" + Environment.NewLine + exc);

                }
                MessageBox.Show("Изменения сохранены");
                this.Close();
            }
            else
            {
                MessageBox.Show("Невозможно сохранить, не все данные заполнены корректно");
            }

        }

        private void SaveAdded(ApplicationContext db, ObservableCollection<Product> ProductsCopy)
        {
            foreach (var item in ProductsCopy)
            {
                if (db.Products.Find(item.Id) == null)
                {
                    db.Products.Add(item);
                }
            }
        }
        private void SaveDeteled(ApplicationContext db, ObservableCollection<Product> ProductsCopy)
        {
            var listCopy = new List<Product>(ProductsCopy);
            var listOriginal = new List<Product>(db.Products);
            foreach (var item in listOriginal)
            {
                if (listCopy.Find((copy) => copy.Id == item.Id) == null)
                {
                    db.Products.Remove(item);
                }
            }
        }
        private void SaveEdited(ApplicationContext db, ObservableCollection<Product> ProductsCopy)
        {
            var listCopy = new List<Product>(ProductsCopy);
            var listOriginal = new List<Product>(db.Products);
            foreach (var item in listOriginal)
            {
                var finditem = listCopy.Find((copy) => copy.Id == item.Id);

                if (finditem != null)
                {
                    finditem.CopyTo(item);
                }
            }
        }
    }
}
