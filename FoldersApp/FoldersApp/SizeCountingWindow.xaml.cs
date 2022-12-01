using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace FoldersApp
{
    /// <summary>
    /// Interaction logic for SizeCountingWindow.xaml
    /// </summary>
    public partial class SizeCountingWindow : Window
    {
        public long Size = 0;
        public MyFolder Folder = null;
        List<FileInfo> allFiles = new List<FileInfo>();

        public SizeCountingWindow(MyFolder folder)
        {
            Folder = folder;
            InitializeComponent();
            Loaded += SizeCountingWindow_Loaded;
        }
        private void SizeCountingWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 500);
            dispatcherTimer.Start();
            Thread myThread = new Thread(Print);
            myThread.Start();
        }

        private void Print()
        {
            CountFolderSize(Folder.Path);
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            txt.Text = Size.ToString();
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void EnumerateFiles(DirectoryInfo main, IFileVisitor visitor)
        {
            try
            {
                var files = main.GetFiles();
                foreach (var file in files)
                {
                    visitor.Visit(file);
                }
                var otherDir = main.GetDirectories();
                if (otherDir.Length > 0)
                {
                    foreach (var dir in otherDir)
                    {
                        EnumerateFiles(dir, visitor);
                    }
                }
            }
            catch
            {

            }
        }



        private void GetList(DirectoryInfo main)
        {
            AddFiles(main);

            try
            {
                var Dir = main.GetDirectories();

                foreach (var item in Dir)
                {
                    var files = item.GetFiles();
                    foreach (var file in files)
                    {
                        allFiles.Add(file);
                        Size += file.Length;
                    }
                    var otherDir = item.GetDirectories();
                    if (otherDir.Length > 0)
                    {
                        foreach (var dir in otherDir)
                        {
                            GetList(dir);
                        }
                    }
                }
            }
            catch
            {

            }
        }

        private void AddFiles(DirectoryInfo Dir)
        {
            try
            {
                FileInfo[] files = Dir.GetFiles();
                foreach (var file in files)
                {
                    allFiles.Add(file);
                    Size += file.Length;
                }
            }
            catch
            {

            }
        }

        private void CountFolderSize(string path)
        {
            DirectoryInfo directory = new DirectoryInfo(path);
            //GetList(directory);
            var fileSizeCounter = new FileSizeCounter();
            fileSizeCounter.window = this;
            EnumerateFiles(directory, fileSizeCounter);
            //Size = fileSizeCounter.Size;
        }

        private List<string> FindFiles(string path, string extension)
        {
            DirectoryInfo directory = new DirectoryInfo(path);
            //GetList(directory);
            var fileFinder = new FileFinder(extension);
            //fileFinder.window = this;
            EnumerateFiles(directory, fileFinder);
            //Size = fileSizeCounter.Size;
            return fileFinder.filesnames;
        }

        private void FindButton_Click(object sender, RoutedEventArgs e)
        {
            var filesnames = FindFiles(Folder.Path, ".txt");
            string str = "";
            foreach (var name in filesnames)
            {
                str += name + Environment.NewLine;
            }
            MessageBox.Show(str);
        }
    }

    public interface IFileVisitor
    {
        void Visit(FileInfo File);
    }

    public class FileSizeCounter : IFileVisitor
    {
        public SizeCountingWindow window;
        //public long Size;
        public void Visit(FileInfo File)
        {
            window.Size += File.Length;
            //Size += File.Length;
        }
    }

    public class FileFinder : IFileVisitor
    {
        public List<string> filesnames = new List<string>();
       // public SizeCountingWindow window;
        public string extension;
        public FileFinder(string Extension)
        {
            extension = Extension;
        }
        //public long Size;
        public void Visit(FileInfo File)
        {
            if (File.Name.Contains(extension))
            {
                filesnames.Add(File.Name);
            }
            
        }
    }
}
