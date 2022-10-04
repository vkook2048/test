using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace ListOfInstalledPrograms
{
    class InstalledProgram
    {
        public string DisplayName { get; set; }
        public string UninstallString { get; set; }
        public Icon Image { get; set; }
        public int SystemComponent { get; set; }
        public string Key { get; set; }

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

        public InstalledProgram(string name, string uninstallString, string key)
        {
            DisplayName = name;
            UninstallString = uninstallString;
            Key = key;
        }

        static void Void()
        {

        }
    }
}
