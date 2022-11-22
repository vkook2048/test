using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchiveApp
{
    class Base64Decrypter
    {
        private string InputString;

        public Base64Decrypter(string inputString)
        {
            InputString = inputString;
        }

        public byte[] FromBase64String()
        {
            byte[] output = Convert.FromBase64String(InputString);
            return output;
        }
    }
}
