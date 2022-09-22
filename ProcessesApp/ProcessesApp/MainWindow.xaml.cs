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
using System.Diagnostics;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace ProcessesApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private GridViewColumnHeader listViewSortCol = null;
        private SortAdorner listViewSortAdorner = null;

        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }
        
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Process[] localAll = Process.GetProcesses();
            var allMyProcesses = new ObservableCollection<MyProcess>();
            foreach (var item in localAll)
            {
                string path = "";
                string title = item.MainWindowTitle;
                if (!string.IsNullOrEmpty(title))
                {
                    path = item.MainModule.FileName;
                }
                MyProcess Process = new MyProcess(item, path);
                allMyProcesses.Add(Process);
            }
            DataContext = allMyProcesses;
            processesList.ItemsSource = allMyProcesses;

            /*CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(processesList.ItemsSource);
            view.SortDescriptions.Add(new SortDescription("Id", ListSortDirection.Ascending));*/
        }

        private void processesListColumnHeader_Click(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader column = sender as GridViewColumnHeader;
            string sortBy = column.Tag.ToString();
            if (listViewSortCol != null)
            {
                AdornerLayer.GetAdornerLayer(listViewSortCol).Remove(listViewSortAdorner);
                processesList.Items.SortDescriptions.Clear();
            }

            ListSortDirection newDir = ListSortDirection.Ascending;
            if (listViewSortCol == column && listViewSortAdorner.Direction == newDir)
                newDir = ListSortDirection.Descending;

            listViewSortCol = column;
            listViewSortAdorner = new SortAdorner(listViewSortCol, newDir);
            AdornerLayer.GetAdornerLayer(listViewSortCol).Add(listViewSortAdorner);
            processesList.Items.SortDescriptions.Add(new SortDescription(sortBy, newDir));
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
