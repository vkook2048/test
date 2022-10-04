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
using System.Drawing;
using System.IO;
using System.Windows.Threading;

namespace ProcessesApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private GridViewColumnHeader listViewSortCol = null;
        private SortAdorner listViewSortAdorner = null;
        ObservableCollection<MyProcess> allMyProcesses = new ObservableCollection<MyProcess>();

        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }
        
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Process[] localAll = Process.GetProcesses();

            foreach (var item in localAll)
            {
                MyProcess Process = MakeMyProcess(item);
                allMyProcesses.Add(Process);
            }
            DataContext = allMyProcesses;
            processesList.ItemsSource = allMyProcesses;
            var dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            UpdateVoid();
        }

        private MyProcess MakeMyProcess(Process process)
        {
            string path = "";
            Icon icon = null;
            try
            {
                if (process.MainModule != null)
                {
                    path = process.MainModule.FileName;
                    icon = System.Drawing.Icon.ExtractAssociatedIcon(path);
                }
            }
            catch
            {

            }
            MyProcess myProcess = new MyProcess(process, path);
            myProcess.Image = icon;
            return myProcess;
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

            if (processesList.SelectedItem != null)
            {
                processesList.ScrollIntoView(processesList.SelectedItem);
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            // taskkill /pid 4600
            // cmd.exe /c taskkill /pid 4600
            // c# how to start process
            // c# how to start cmd.exe


            if (MessageBox.Show("Вы хотите завершить процесс?","Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                var proc = processesList.SelectedItem as MyProcess;
                if (proc != null)
                {
                    //proc.Process.Kill();
                    var cmd = new ProcessStartInfo()
                    {
                        UseShellExecute = true,
                        FileName = @"cmd.exe",
                        Arguments = @"/c" + " taskkill /pid " + proc.Id.ToString(),
                        WindowStyle = ProcessWindowStyle.Hidden
                    };
                    Process.Start(cmd);
                    allMyProcesses.Remove(proc);

                }
                
            }          
        }
        private void Update_Click(object sender, RoutedEventArgs e)
        {
            UpdateVoid();
        }

        private void UpdateVoid()
        {
            Process[] localAll = Process.GetProcesses();
            AddNew(localAll);
            DeleteOld(localAll);
        }

        private void AddNew(Process[] localAll)
        {
            var oldProcessList = new List<int>() { };
            foreach (var item in allMyProcesses)
            {
                oldProcessList.Add(item.Id);
            }
            foreach (var item in localAll)
            {
                if (!oldProcessList.Contains(item.Id))
                {
                    Process proc = Array.Find(localAll, x => x.Id == item.Id);
                    if (proc != null)
                    {
                        MyProcess newProcess = MakeMyProcess(proc);
                        allMyProcesses.Add(newProcess);
                    }
                }
            }
        }

        private void DeleteOld(Process[] localAll)
        {
            var oldProcessIdList = new List<int>() { };
            var newProcessIdList = new List<int>() { };
            foreach (var item in allMyProcesses)
            {
                oldProcessIdList.Add(item.Id);
            }
            foreach (var item in localAll)
            {
                newProcessIdList.Add(item.Id);
            }
            foreach (var item in oldProcessIdList)
            {
                if (!newProcessIdList.Contains(item))
                {
                    MyProcess oldProcess = null;
                    foreach (var process in allMyProcesses)
                    {
                        if (process.Id == item)
                        {
                            oldProcess = process;
                        }
                    }
                    if (oldProcess != null)                    
                        allMyProcesses.Remove(oldProcess);                  
                }
            }
        }

        private void OpenFileLocation_Click(object sender, RoutedEventArgs e)
        {
            var proc = processesList.SelectedItem as MyProcess;
            if (File.Exists(proc.Path))
            {
                Process.Start(new ProcessStartInfo("explorer.exe", " /select, " + proc.Path));
            }
        }
    }
}
