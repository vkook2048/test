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
using System.IO;
using System.Diagnostics;
using System.Collections.ObjectModel;

namespace FoldersApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<MyFolder> collection = new ObservableCollection<MyFolder>();
        ObservableCollection<MyFile> myFile = new ObservableCollection<MyFile>();
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
            treeView.ItemsSource = collection;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            DirectoryInfo Main = new DirectoryInfo(@"c:\Games");
            MyFolder root = new MyFolder(Main);
            collection.Add(root);
            GetAllDirectories("", Main, root);
        }

        public void GetAllDirectories(string header, DirectoryInfo main, MyFolder parent)
        {
            DirectoryInfo di = new DirectoryInfo(main.FullName);
            //Trace.WriteLine(header + $"{di.Name}");
            MyFolder folder = new MyFolder(di, parent);
            try
            {
                DirectoryInfo[] AllDirectories = di.GetDirectories();
                foreach (var item in AllDirectories)
                {
                     GetAllDirectories(header + "  ", item, folder);
                    //Trace.WriteLine($"{item.Name}: " + subDirectories.Length);
                   
                }
            }
            catch
            {
                
            }
        }

        private void GetFiles(MyFolder folder)
        {
            myFile.Clear();
            var files = folder.Dir.GetFiles();
            if (files != null)
            {
               foreach (var item in files)
               {
                    MyFile file = new MyFile(item);
                    file.Image = System.Drawing.Icon.ExtractAssociatedIcon(file.Path);
                    myFile.Add(file);
                    //Trace.WriteLine($"{file.Name}");
               }
            }                   
        }

        private void Folder_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
           GetFiles(treeView.SelectedItem as MyFolder);
        }

        private void ListColumnHeader_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("sth");
        }

        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("_sth");
        }
    }
}
