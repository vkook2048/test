using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Linq;

namespace ProcessesApp
{
    class MyProcess
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }

        public MyProcess(Process process, string path)
        {
            this.Id = process.Id;
            this.Name = process.ProcessName;
            this.Path = path;
        }
        
    }
}
