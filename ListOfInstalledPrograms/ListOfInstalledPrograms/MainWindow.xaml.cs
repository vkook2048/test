using Microsoft.Win32;
using ProcessesApp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
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

namespace ListOfInstalledPrograms
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private GridViewColumnHeader listViewSortCol = null;
        private SortAdorner listViewSortAdorner = null;
        ObservableCollection<InstalledProgram> collection = new ObservableCollection<InstalledProgram>();
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            RegistryKey currentUserRoot = Registry.CurrentUser;
            RegistryKey localMachineRoot = Registry.LocalMachine;
            GetList(localMachineRoot);
            GetList(currentUserRoot);
            //GetList(localMachineRoot);
            DataContext = collection;
            progList.ItemsSource = collection;
        }
        public void GetList(RegistryKey root)
        {            
            // "Компьютер\\HKEY_CURRENT_USER\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall"
            RegistryKey rkTest = root.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall");
            string[] ArrayofValuesNames = rkTest.GetSubKeyNames();
            foreach (var subkeyName in ArrayofValuesNames)
            {
                RegistryKey subkey = rkTest.OpenSubKey(subkeyName);
                //DisplayName
                var objname = subkey.GetValue("DisplayName");
                string name = "???";
                string uninstallString = "...";
                var objUnistStr = subkey.GetValue("UninstallString");
                if (objname != null)
                {
                    name = objname.ToString();                     
                }
                if (objUnistStr != null)
                {
                    uninstallString = objUnistStr.ToString();
                }
                string path = "";
                Icon icon = null;
                try
                {
                    var obj = subkey.GetValue("DisplayIcon");
                    if (obj != null)
                    {
                        path = obj.ToString();
                        icon = System.Drawing.Icon.ExtractAssociatedIcon(path);
                    }
                }
                catch
                {

                };
                int comp = -8;
                var intobj = subkey.GetValue("SystemComponent");
                if (intobj != null)
                {
                    comp = (int)intobj;
                }
                if (comp != 1 && uninstallString != "...")
                {
                    var prog = new InstalledProgram(name, uninstallString, subkeyName);
                    prog.Image = icon;
                    prog.SystemComponent = comp;
                    collection.Add(prog);
                }               
            }
        }
        private void ListColumnHeader_Click(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader column = sender as GridViewColumnHeader;
            string sortBy = column.Tag.ToString();
            if (listViewSortCol != null)
            {
                AdornerLayer.GetAdornerLayer(listViewSortCol).Remove(listViewSortAdorner);
                progList.Items.SortDescriptions.Clear();
            }

            ListSortDirection newDir = ListSortDirection.Ascending;
            if (listViewSortCol == column && listViewSortAdorner.Direction == newDir)
                newDir = ListSortDirection.Descending;

            listViewSortCol = column;
            listViewSortAdorner = new SortAdorner(listViewSortCol, newDir);
            AdornerLayer.GetAdornerLayer(listViewSortCol).Add(listViewSortAdorner);
            progList.Items.SortDescriptions.Add(new SortDescription(sortBy, newDir));

            if (progList.SelectedItem != null)
            {
                progList.ScrollIntoView(progList.SelectedItem);
            }
        }
    }
}
