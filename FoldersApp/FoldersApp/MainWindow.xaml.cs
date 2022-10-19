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
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
            treeView.ItemsSource = collection;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            DirectoryInfo Main = new DirectoryInfo(@"C:\");
            MyFolder root = new MyFolder(Main);
            GetTwoLevels(root);
            collection.Add(root);
            //GetAllDirectories("", Main, root);
        }

        public void GetTwoLevels(MyFolder root)
        {
            root.Folders.Clear();
            root.GetDirectories(root.Dir, root);
            
            foreach (var item in root.Folders)
            {
                try
                {
                    item.Folders.Clear();
                    item.GetDirectories(item.Dir, item);
                }
                catch
                {

                }
            }
            
        }

        private void Folder_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //GetFiles(treeView.SelectedItem as MyFolder);
        }

        private void ListColumnHeader_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("sth");
        }

        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MyFolder currentFolder = treeView.SelectedItem as MyFolder;
            var nextFolder = fileList.SelectedItem as MyFile;
            foreach (var item in new List<MyFolder>(currentFolder.Folders))
            {
                if (item.Name == nextFolder.Name)
                {
                    SelectFolder(item);
                    //Trace.WriteLine($"{item.Name}: {CountFolderSize(item)}");
                }
            }
        }

        private void SelectFolder(MyFolder folder)
        {
            List<MyFolder> parents = new List<MyFolder>();
            var tmpFolder = folder;
            while (tmpFolder != null)
            {
                parents.Insert(0, tmpFolder);
                tmpFolder = tmpFolder.Parent;
            }

            TreeViewItem tvi = null;
            for (int i = 0; i < parents.Count; i++)
            {
                if (tvi == null)
                {
                    tvi = treeView.ItemContainerGenerator.ContainerFromItem(parents[i]) as TreeViewItem;
                }
                else
                {
                    tvi = tvi.ItemContainerGenerator.ContainerFromItem(parents[i]) as TreeViewItem;
                }

                if (tvi == null)
                    break;

                tvi.IsExpanded = true;

                if (i == parents.Count - 1)
                    tvi.IsSelected = true;
            }
        }

        private void Folder_Expanded(object sender, RoutedEventArgs e)
        {
            TreeViewItem item = sender as TreeViewItem;
            if (item != null)
            {
                var folder = item.DataContext as MyFolder;
                if (folder != null)
                {
                    GetTwoLevels(folder);
                }
            }
        }

        private void OpenSizeCounter_Click(object sender, RoutedEventArgs e)
        {
            var file = fileList.SelectedItem as MyFile;
            if (file.IsFolder)
            {
                SizeCountingWindow SizeWindow = new SizeCountingWindow(FileToFolder(file, file.Parent));
                if (SizeWindow.ShowDialog() == true)
                {

                }
            }
        }
        public MyFolder FileToFolder(MyFile file, MyFolder parent)
        {
            MyFolder folder = null;
            foreach (var item in parent.Folders)
            {
                if (item.Name == file.Name)
                {
                    folder = item;
                }
            }
            return folder;
        }
    }
}
