using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace FoldersApp
{
    public class MyFolder
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public Icon Image { get; set; }

        public List<MyFolder> Folders { get; set; } = new List<MyFolder>();
        public DirectoryInfo Dir { get; set; }

        public BitmapSource ImageSource
        {
            get
            {
                try
                {
                    if (Image == null)
                        return null;
                    return Imaging.CreateBitmapSourceFromHIcon(Image.Handle, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                }
                catch
                {
                    return null;
                }

            }
            set
            {

            }
        }

        public MyFolder(DirectoryInfo dir)
        {
            Name = dir.Name;
            Path = dir.FullName;
            Dir = dir;
            Image = Icon.ExtractAssociatedIcon(@"C:\sql\FolderIcon.ico");
        }

        public MyFolder(DirectoryInfo dir, MyFolder parent)
        {
            Name = dir.Name;
            Path = dir.FullName;
            parent.Folders.Add(this);
            Dir = dir;
            Image = Icon.ExtractAssociatedIcon(@"C:\sql\FolderIcon.ico");
        }

        public override string ToString()
        {
            return Path;
        }

        public List<MyFile> Files
        {
            get
            {
                List<MyFile> mfiles = new List<MyFile>();

                var files = Dir.GetFiles();
                if (files != null)
                {
                    foreach (var item in files)
                    {
                        MyFile file = new MyFile(item);
                        file.Image = System.Drawing.Icon.ExtractAssociatedIcon(file.Path);
                        mfiles.Add(file);
                        //Trace.WriteLine($"{file.Name}");
                    }
                }
                return mfiles;
            }
            set { }
        }
    }
}
