using Microsoft.Win32;
using ProcessesApp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
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
            GetList(localMachineRoot, "no", collection);
            GetList(currentUserRoot, "no", collection);
            GetList(localMachineRoot, "yes", collection);
            DataContext = collection;
            progList.ItemsSource = collection;
        }
        private void GetList(RegistryKey root, string confirmation, ObservableCollection<InstalledProgram> Collection)
        {
            // "Компьютер\\HKEY_CURRENT_USER\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall"
            // Компьютер\\HKEY_LOCAL_MACHINE\\SOFTWARE\\WOW6432Node\\Microsoft\\Windows\\CurrentVersion\\Uninstall
            string path_beginning = "SOFTWARE\\";
            string path_ending = "Microsoft\\Windows\\CurrentVersion\\Uninstall";
            string path_addition = "WOW6432Node\\";
            string path_full = "";
            if (confirmation == "yes")
            {
                path_full = path_beginning + path_addition + path_ending;
            }
            else
            {
                path_full = path_beginning + path_ending;
            }
            RegistryKey rkTest = root.OpenSubKey(path_full);
            string[] ArrayofValuesNames = rkTest.GetSubKeyNames();
            foreach (var subkeyName in ArrayofValuesNames)
            {
                RegistryKey subkey = rkTest.OpenSubKey(subkeyName);
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
                var obj = subkey.GetValue("DisplayIcon");
                try
                {
                    
                    if (obj != null)
                    {
                        path = obj.ToString();

                        if (path[0] == '\"')
                        {
                            path = path.Substring(1);
                        }

                        if (path[path.Length - 1] == '\"')
                        {
                            path = path.Substring(0, path.Length - 1);
                        }

                        int found = path.LastIndexOf(",");
                        if (found > 0)
                        {
                            string after = path.Substring(found + 1);
                            bool isDigit = true;
                            for (int i = 0; i < after.Length; i++)
                            {
                                if (" -0123456789".IndexOf(after[i]) < 0)
                                    isDigit = false;
                            }

                            if (isDigit)
                            {
                                path = path.Substring(0, found);
                            }
                        }

                        if (path[path.Length - 1] == '\"')
                        {
                            path = path.Substring(0, path.Length - 1);
                        }

                        if (!File.Exists(path))
                            path = @"c:\sql\fileinterfacesymboloftextpapersheet_79740.ico";

                        icon = System.Drawing.Icon.ExtractAssociatedIcon(path);
                    }
                }
                catch
                {
                    if (File.Exists(path))
                    {
                        var p = path;
                    }
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
                    Collection.Add(prog);
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

        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var program = progList.SelectedItem as InstalledProgram;
                //MessageBox.Show(program.UninstallString);
                //Process.Start(program.UninstallString);


                Process proc = new Process();
                proc.StartInfo.FileName = program.UninstallString;
                proc.StartInfo.UseShellExecute = true;
                proc.StartInfo.Verb = "runas";
                proc.Start();

                collection.Remove(program);
            }
            catch (System.ComponentModel.Win32Exception)
            {

            }
            catch (Exception exc)
            {
                MessageBox.Show("Error " + Environment.NewLine + exc.ToString());
            }
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            RegistryKey currentUserRoot = Registry.CurrentUser;
            RegistryKey localMachineRoot = Registry.LocalMachine;
            ObservableCollection<InstalledProgram> NewCollection = new ObservableCollection<InstalledProgram>();
            GetList(localMachineRoot, "no", NewCollection);
            GetList(currentUserRoot, "no", NewCollection);
            GetList(localMachineRoot, "yes", NewCollection);
            AddNew(NewCollection);
            //DeleteOld(NewCollection);
        }

        private void AddNew(ObservableCollection<InstalledProgram> NewCollection)
        {
            var oldProcessNameList = new List<string>() { };
            var newProcessNameList = new List<string>() { };
            foreach (var item in collection)
            {
                oldProcessNameList.Add(item.DisplayName);
            }
             foreach (var item in NewCollection)
            {
                newProcessNameList.Add(item.DisplayName);
            }
            foreach (var item in NewCollection)
            {
                if (!oldProcessNameList.Contains(item.DisplayName))
                {
                    /*InstalledProgram prog = null;
                    foreach (var name in newProcessNameList)
                    {
                        if (name == item.DisplayName)
                        {
                            prog = item;
                        }
                    }
                    if (prog != null)
                    {
                        collection.Add(prog);
                    }*/
                    collection.Add(item);
                }
            }
        }

        /*private void DeleteOld(ObservableCollection<InstalledProgram> NewCollection)
        {
            var newProcessNameList = new List<string>() { };
            foreach (var item in NewCollection)
            {
                newProcessNameList.Add(item.DisplayName);
            }
            foreach (var item in collection)
            {
                if (!newProcessNameList.Contains(item.DisplayName))
                {
                    collection.Remove(item);
                }
            }
        }*/
    }
}
