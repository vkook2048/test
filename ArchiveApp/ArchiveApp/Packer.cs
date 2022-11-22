using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchiveApp
{
    class Packer
    {
        string InputFile;
        string OutputFile;

        public Packer(string inputFile, string outputFile)
        {
            InputFile = inputFile;
            OutputFile = outputFile;
        }

        public void PackZip()
        {
            string path = Path.GetTempPath();
            path = Path.Combine(path, Path.GetRandomFileName());
            DirectoryInfo Dir = Directory.CreateDirectory(path);
            FileInfo file = new FileInfo(InputFile);
            path = Path.Combine(path, file.Name);
            File.Copy(InputFile, path);
            string sourceFolder = Dir.FullName;
            string zipFile = OutputFile;
            ZipFile.CreateFromDirectory(sourceFolder, zipFile);
            Directory.Delete(Dir.FullName, true);
        }

        public void UnpackZip()
        {
            string zipFile = InputFile;
            string path = Path.GetTempPath();
            path = Path.Combine(path, Path.GetRandomFileName());
            DirectoryInfo Dir = Directory.CreateDirectory(path);
            ZipFile.ExtractToDirectory(zipFile, path);
            var files = Dir.GetFiles();
            File.Copy(files[0].FullName, OutputFile);
            Directory.Delete(path, true);
        }
    }
}
