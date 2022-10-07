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
    public class MyFile
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public Icon Image { get; set; }
        public long Size { get; set; }

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

        public MyFile(FileInfo file)
        {
            Name = file.Name;
            Path = file.FullName;
            Size = file.Length;
        }
    }
}
