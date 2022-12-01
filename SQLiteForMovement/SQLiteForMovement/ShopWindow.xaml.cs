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
    /// Interaction logic for ShopWindow.xaml
    /// </summary>
    public partial class ShopWindow : Window
    {

        ApplicationContext ApplicationContext { get; set; }
        public ShopWindow(ApplicationContext db)
        {
            ApplicationContext = db;
            InitializeComponent();
            Loaded += ShopWindow_Loaded;
        }

        public ObservableCollection<Shop> ShopsCopy { get; set; }


        // при загрузке окна
        private void ShopWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // гарантируем, что база данных создана

            // загружаем данные из БД
            ApplicationContext.Shops.Load();
            // и устанавливаем данные в качестве контекста
            ShopsCopy = new ObservableCollection<Shop>();
            foreach(var shop in ApplicationContext.Shops)
            {
                ShopsCopy.Add(shop.CloneShop());
            }
            DataContext = ShopsCopy;
        }
        // добавление
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            Shop shop = new Shop();
            ShopsCopy.Add(shop);
            shopsList.SelectedItem = shop;
        }
        // удаление
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            // получаем выделенный объект
            Shop shop = shopsList.SelectedItem as Shop;
            // если ни одного объекта не выделено, выходим
            if (shop is null) return;
            ShopsCopy.Remove(shop);
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            int count = 0;
            foreach (var item in ShopsCopy)
            {
                if (!string.IsNullOrEmpty(item.Id) && item.AreaId > 0 && !string.IsNullOrEmpty(item.Address))
                {
                    count++;
                }   
            }
            if (count == ShopsCopy.Count)
            {
                try
                {
                    SaveAdded(ApplicationContext, ShopsCopy);
                    SaveDeteled(ApplicationContext, ShopsCopy);
                    SaveEdited(ApplicationContext, ShopsCopy);
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

        private void SaveAdded(ApplicationContext db, ObservableCollection<Shop> ShopsCopy)
        {
            foreach (var item in ShopsCopy)
            {
                if (db.Shops.Find(item.Id) == null)
                {
                    db.Shops.Add(item);
                }
            }
        }
        private void SaveDeteled(ApplicationContext db, ObservableCollection<Shop> ShopsCopy)
        {
            var listCopy = new List<Shop>(ShopsCopy);
            var listOriginal = new List<Shop>(db.Shops);
            foreach (var item in listOriginal)
            {
                /*Shop find = null;
                foreach(var copy in listCopy)
                {
                    if (copy.Id == item.Id)
                    {
                        find = copy;
                        break;
                    }
                }*/

                if (listCopy.Find((copy) => copy.Id == item.Id) == null)
                {
                    db.Shops.Remove(item);
                }
            }
        }
        private void SaveEdited(ApplicationContext db, ObservableCollection<Shop> ShopsCopy)
        {
            var listCopy = new List<Shop>(ShopsCopy);
            var listOriginal = new List<Shop>(db.Shops);
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
