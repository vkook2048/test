using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
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
using System.Xml.Serialization;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


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

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            Movement movement = movementsList.SelectedItem as Movement;
            if (movement is null) return;
            var editCopy = new Movement() { Context = db };
            movement.CopyTo(editCopy);
            OperationWindow operationWindow = new OperationWindow(editCopy);
            operationWindow.Owner = this;
            if (operationWindow.ShowDialog() == true )
            {
                movement = db.Movements.Find(operationWindow.Movement.Id);
                if (movement != null)
                {
                    editCopy.CopyTo(movement);
                    db.SaveChanges();
                    movementsList.Items.Refresh();
                }
            }
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Movement movement = movementsList.SelectedItem as Movement;
            if (movement is null) return;
            db.Movements.Remove(movement);
            db.SaveChanges();
        }

        private void Shops_Click(object sender, RoutedEventArgs e)
        {
            ShopWindow shopWindow = new ShopWindow(db);
            shopWindow.Owner = this;
            if (shopWindow.ShowDialog() == true)
            {

            }
        }

        private void Products_Click(object sender, RoutedEventArgs e)
        {
            ProductWindow productWindow = new ProductWindow(db);
            productWindow.Owner = this;
            if (productWindow.ShowDialog() == true)
            {

            }
        }

        public class ExportItem
        {
            public int Id { get; set; }
            public string Date { get; set; }
            public string ShopId { get; set; }
            public string ProductId { get; set; }
            public int Count { get; set; }
            public string Operation { get; set; }
            public double Price { get; set; }

            public string Headers()
            {
                return "Id,Date,ShopId,ProductId,Count,Operation,Price";
            }
        }

        private void Export_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "CSV files(*.csv)|*.csv|JSON files(*.json)|*.json|XML files(*.xml)|*.xml";
            if (saveFileDialog1.ShowDialog() == false)
            {
                return;
            }
            string filename = saveFileDialog1.FileName;
            string[] movements = new string[1];
            if (filename.Contains(".csv"))
            {
                movements = new string[db.Movements.Count() + 1];
                string sqlExpression = "select * from movements";
                using (var connection = new SqliteConnection("Data Source=c:\\Users\\Lexa\\Desktop\\SmartGit\\test\\12.db"))
                {
                    connection.Open();
                    SqliteCommand command = new SqliteCommand(sqlExpression, connection);
                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        int columnscount = reader.FieldCount;
                        for (int i = 0; i < columnscount; i++)
                        {
                            if (i == columnscount - 1)
                                movements[0] += reader.GetName(i);
                            else
                                movements[0] += reader.GetName(i) + ",";
                        }

                        if (reader.HasRows)
                        {
                            int rowcount = 1;
                            while (reader.Read())
                            {
                                for (int i = 0; i < columnscount; i++)
                                {
                                    if (reader.GetFieldType(i) == typeof(int))
                                    {
                                        int val = reader.GetInt32(i);
                                        movements[rowcount] += val;
                                    }
                                    else
                                    {
                                        byte[] bytes = new byte[1024];
                                        long size = reader.GetBytes(i, 0, bytes, 0, 1024);
                                        byte[] bt = new byte[size];
                                        for (int j = 0; j < size; j++)
                                            bt[j] = bytes[j];
                                        string val = Encoding.Default.GetString(bt);
                                        movements[rowcount] += val;
                                    }

                                    if (i != columnscount - 1)
                                    {
                                        movements[rowcount] += ",";
                                    }
                                }
                                rowcount++;
                            }
                        }
                    }
                }
                File.WriteAllLines(filename, movements);
            }
            else if (filename.Contains(".json"))
            {
                movements = new string[db.Movements.Count()];
                int i = 0;

                string sqlExpression = "select * from movements";
                using (var connection = new SqliteConnection("Data Source=c:\\Users\\Lexa\\Desktop\\SmartGit\\test\\12.db"))
                {
                    connection.Open();
                    SqliteCommand command = new SqliteCommand(sqlExpression, connection);
                    using (SqliteDataReader reader = command.ExecuteReader())
                    {                        
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                JObject obj = new JObject();
                                obj.Add("Id", reader.GetInt32(0));
                                obj.Add("Date", reader.GetString(1));
                                obj.Add("ShopId", reader.GetString(2));
                                obj.Add("ProductId", reader.GetString(3));
                                obj.Add("Count", reader.GetInt32(4));
                                obj.Add("Operation", reader.GetString(5));
                                obj.Add("Price", reader.GetDouble(6));
                                movements[i] = obj.ToString(Formatting.None);
                                i++;
                            }
                        }
                    }
                }
                File.WriteAllLines(filename, movements);
            }
            else if (filename.Contains(".xml"))
            {
                List <ExportItem> items = new List<ExportItem>();
                string sqlExpression = "select * from movements";
                using (var connection = new SqliteConnection("Data Source=c:\\Users\\Lexa\\Desktop\\SmartGit\\test\\12.db"))
                {
                    connection.Open();
                    SqliteCommand command = new SqliteCommand(sqlExpression, connection);
                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {                                
                                var item = new ExportItem();
                                item.Id = reader.GetInt32(0);
                                item.Date = reader.GetString(1);
                                item.ShopId = reader.GetString(2);
                                item.ProductId = reader.GetString(3);
                                item.Count = reader.GetInt32(4);
                                item.Operation = reader.GetString(5);
                                item.Price = reader.GetDouble(6);
                                items.Add(item);
                            }
                        }
                    }
                }
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<ExportItem>));
                using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate))
                {
                    xmlSerializer.Serialize(fs, items);
                }
            }
        }

        private void Import_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "CSV files(*.csv)|*.csv|JSON files(*.json*)|*.json*|XML files(*.xml*)|*.xml*";
            if (openFileDialog1.ShowDialog() == false)
            {
                return;
            }
            string filename = openFileDialog1.FileName;
            string[] movements = File.ReadAllLines(filename);           
            int number = 0;
            int mistakes = 0;
            using (var connection = new SqliteConnection("Data Source=c:\\Users\\Lexa\\Desktop\\SmartGit\\test\\12.db"))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand();
                command.Connection = connection;
                if (filename.Contains(".csv"))
                {
                    for (int i = 0; i < movements.Length; i++)
                    {
                        if (i == 0)
                        {
                            i++;
                        }
                        string[] headers = movements[0].Split(',');
                        string[] move = movements[i].Split(',');

                        command.CommandText =
                            $"INSERT INTO Movements ({headers[0]}, {headers[1]}, {headers[2]}, {headers[3]}, {headers[4]}, {headers[5]}, {headers[6]})"
                            + $" VALUES ('{move[0]}', '{move[1]}', '{move[2]}', '{move[3]}', '{move[4]}', '{move[5]}', '{move[6]}')";
                        try
                        {
                            number += command.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            Trace.WriteLine(ex.ToString());
                            mistakes++;
                        }
                    }
                }
                else if (filename.Contains(".json"))
                {
                    for (int i = 0; i < movements.Length; i++)
                    {
                        int count = 0;
                        JObject jObj = JObject.Parse(movements[i]);
                        string[] headers = new string[jObj.Count];
                        string[] move = new string[jObj.Count];
                        foreach (var child in jObj)
                        {

                            headers[count] = child.Key;
                            move[count] = child.Value.ToString();
                            count++;
                        }
                        command.CommandText =
                            $"INSERT INTO Movements ({headers[0]}, {headers[1]}, {headers[2]}, {headers[3]}, {headers[4]}, {headers[5]}, {headers[6]})"
                            + $" VALUES ('{move[0]}', '{move[1]}', '{move[2]}', '{move[3]}', '{move[4]}', '{move[5]}', '{move[6]}')";
                        try
                        {
                            number += command.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            Trace.WriteLine(ex.ToString());
                            mistakes++;
                        }
                    }                   
                }
                else if (filename.Contains(".xml"))
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<ExportItem>));
                    List<ExportItem> items = new List<ExportItem>();
                    using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate))
                    {
                        items = xmlSerializer.Deserialize(fs) as List<ExportItem>;
                    }
                    foreach (var item in items)
                    {
                        string[] headers = item.Headers().Split(',');
                        command.CommandText =
                            $"INSERT INTO Movements ({headers[0]}, {headers[1]}, {headers[2]}, {headers[3]}, {headers[4]}, {headers[5]}, {headers[6]})"
                            + $" VALUES ('{item.Id}', '{item.Date}', '{item.ShopId}', '{item.ProductId}', '{item.Count}', '{item.Operation}', '{item.Price}')";
                        try
                        {
                            number += command.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            Trace.WriteLine(ex.ToString());
                            mistakes++;
                        }
                    }
                }
                    
            }                
            
            MessageBox.Show($"В таблицу Movements добавлено объектов: {number}. Ошибок: {mistakes}");
            db.Movements.Load();
        }
    }
}

