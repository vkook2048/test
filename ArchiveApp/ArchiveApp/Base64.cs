using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchiveApp
{
    class Base64
    {
        public static string ToBase64String(byte[] Bytes)
        {
            string output = Convert.ToBase64String(Bytes);
            return output;
        }

        public static byte[] FromBase64String(string InputString)
        {
            byte[] output = Convert.FromBase64String(InputString);
            return output;
        }
    }
}
