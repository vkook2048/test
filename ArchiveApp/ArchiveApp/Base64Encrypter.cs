using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchiveApp
{
    class Base64Encrypter
    {
        private byte[] Bytes;

        public Base64Encrypter(byte[] bytes)
        {
            Bytes = bytes;
        }

        public string ToBase64String()
        {
            string output = Convert.ToBase64String(Bytes);
            return output;
        }
    }
}
