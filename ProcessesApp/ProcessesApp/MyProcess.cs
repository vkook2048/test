using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Linq;
using System.Drawing;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace ProcessesApp
{
    public class MyProcess
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public Icon Image { get; set; }

        public BitmapSource ImageSource
        {
            get
            {
                if (Image == null)
                    return null;
                return Imaging.CreateBitmapSourceFromHIcon(Image.Handle, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            }
            set
            {

            }
        }

        public Process Process { get; private set; }

        public MyProcess(Process process, string path)
        {
            Process = process;
            Id = process.Id;
            Name = process.ProcessName;
            Path = path;
            /*if (!string.IsNullOrEmpty(path))
            {
                Image = Icon.ExtractAssociatedIcon(path);
            }*/

        }

       
        
    }
}
