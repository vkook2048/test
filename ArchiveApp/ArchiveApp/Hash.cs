using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ArchiveApp
{
    class Hash
    {
        string InputFile;
        public Hash(string inputFile)
        {
            InputFile = inputFile;
        }

        public string CountCRC32()
        {
            byte[] Bytes = File.ReadAllBytes(InputFile);
            UInt32[] crc_table = new UInt32[256];
            UInt32 crc;

            for (UInt32 i = 0; i < 256; i++)
            {
                crc = i;
                for (UInt32 j = 0; j < 8; j++)
                    crc = (crc & 1) != 0 ? (crc >> 1) ^ 0xEDB88320 : crc >> 1;

                crc_table[i] = crc;
            };

            crc = 0xFFFFFFFF;

            foreach (byte s in Bytes)
            {
                crc = crc_table[(crc ^ s) & 0xFF] ^ (crc >> 8);
            }

            crc ^= 0xFFFFFFFF;
            int numb = (int)crc;
            byte[] intBytes = BitConverter.GetBytes(numb);
            Array.Reverse(intBytes);
            return Hex.ToHexString(intBytes);
        }

        public string CountMD5()
        {
            string str = "";
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(InputFile))
                {
                    var hash = md5.ComputeHash(stream);
                    str = BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                }
            }
            return str;
        }

        public string CountSHA256()
        {
            byte[] Bytes = File.ReadAllBytes(InputFile);
            string hash = String.Empty;
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashValue = sha256.ComputeHash(Bytes);
                hash = Hex.ToHexString(hashValue);
            }
            return hash;
        }
    }
}
