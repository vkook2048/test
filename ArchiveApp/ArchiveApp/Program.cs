using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;
using System.IO;
using System.Security.Cryptography;

namespace ArchiveApp
{

    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 3)
            {
                Console.WriteLine("bad args");
                Console.ReadKey();
                return;
            }

            string command = args[0];
            string inputFile = args[1];
            string outputFile = args[2];
            /*string command = "/to_hex";
            string inputFile = @"c:\Users\Lexa\Desktop\SmartGit\test\ArchiveApp\ArchiveApp\bin\Debug\TFKGEOM.dll";
            string outputFile = @"c:\Users\Lexa\Desktop\SmartGit\test\ArchiveApp\ArchiveApp\bin\Debug\TFKGEOMh.dat";*/

            if (command == "/pack")
            {
                string path = Path.GetTempPath();
                path = Path.Combine(path, Path.GetRandomFileName());
                DirectoryInfo Dir = Directory.CreateDirectory(path);
                FileInfo file = new FileInfo(inputFile);
                path = Path.Combine(path, file.Name);
                File.Copy(inputFile, path);
                string sourceFolder = Dir.FullName;
                string zipFile = outputFile;
                ZipFile.CreateFromDirectory(sourceFolder, zipFile);
                Directory.Delete(Dir.FullName, true);
            }
            if (command == "/unpack")
            {
                string zipFile = inputFile;
                string path = Path.GetTempPath();
                path = Path.Combine(path, Path.GetRandomFileName());
                DirectoryInfo Dir = Directory.CreateDirectory(path);
                ZipFile.ExtractToDirectory(zipFile, path);
                var files = Dir.GetFiles();
                File.Copy(files[0].FullName, outputFile);
                Directory.Delete(path, true);
            }
            if (command == "/encrypt" || command == "/decrypt")
            {
                {
                    byte[] key = Encoding.ASCII.GetBytes("012345678901234567890123456789as");
                    byte[] iv = new byte[16];
                    Random rnd = new Random();
                    rnd.NextBytes(iv);

                    if (command == "/encrypt")
                    {
                        byte[] encrypted = EncryptToBytes(File.ReadAllBytes(inputFile), key, iv);
                        byte[] full = new byte[iv.Length + encrypted.Length];
                        Array.Copy(iv, 0, full, 0, iv.Length);
                        Array.Copy(encrypted, 0, full, iv.Length, encrypted.Length);
                        File.WriteAllBytes(outputFile, full);

                    }
                    if (command == "/decrypt")
                    {
                        byte[] full = File.ReadAllBytes(inputFile);
                        byte[] enc = new byte[full.Length - 16];
                        byte[] Iv = new byte[16];
                        Array.Copy(full, 0, Iv, 0, Iv.Length);
                        Array.Copy(full, Iv.Length, enc, 0, enc.Length);

                        byte[] decrypted = DecryptFromBytes(enc, key, Iv);
                        File.WriteAllBytes(outputFile, decrypted);

                    }
                }

            }
            if (command == "/to_hex")
            {
                byte[] orig = File.ReadAllBytes(inputFile);
                StringBuilder sb = new StringBuilder();
                foreach (var item in orig)
                {
                    sb.Append(ToHexademical(item));
                }
                File.WriteAllText(outputFile, sb.ToString());
            }
            if (command == "/from_hex")
            {
               string input = File.ReadAllText(inputFile);
               string[] numbers = new string[input.Length / 2];
                int count = 0;
                for (int i = 0; i < numbers.Length; i++)
                {
                    numbers[i] += input[count];
                    count++;
                    numbers[i] += input[count];
                    count++;
                }
                byte[] output = new byte[numbers.Length];
                for (int i = 0; i < numbers.Length; i++)
                {
                    output[i] = FromHexademical(numbers[i]);
                }
                File.WriteAllBytes(outputFile, output);
            }
            if (command == "/to_base64")
            {
                byte[] input = File.ReadAllBytes(inputFile);
                string output = Convert.ToBase64String(input);
                File.WriteAllText(outputFile, output);
            }
            if (command == "/from_base64")
            {
                string input = File.ReadAllText(inputFile);
                byte[] output = Convert.FromBase64String(input);
                File.WriteAllBytes(outputFile, output);
            }
            if (command == "/crc32")
            {
                byte[] source = File.ReadAllBytes(inputFile);

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

                foreach (byte s in source)
                {
                    crc = crc_table[(crc ^ s) & 0xFF] ^ (crc >> 8);
                }

                crc ^= 0xFFFFFFFF;
                int numb = (int)crc;
                byte[] intBytes = BitConverter.GetBytes(numb);
                Array.Reverse(intBytes);
                StringBuilder sb = new StringBuilder();
                foreach (var item in intBytes)
                {
                    sb.Append(ToHexademical(item));
                }
                File.WriteAllText(outputFile, sb.ToString());
            }
            if (command == "/md5")
            {
                string str = "";
                using (var md5 = MD5.Create())
                {
                    using (var stream = File.OpenRead(inputFile))
                    {
                        var hash = md5.ComputeHash(stream);
                        str = BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                    }
                }
                File.WriteAllText(outputFile, str);
            }
            if (command == "/sha256")
            {
                byte[] b = File.ReadAllBytes(inputFile);
                File.WriteAllText(outputFile, ComputeSHA256(b));
            }
        }
        static string ComputeSHA256(byte[] b)
        {
            string hash = String.Empty;

            // Initialize a SHA256 hash object
            using (SHA256 sha256 = SHA256.Create())
            {
                // Compute the hash of the given string
                byte[] hashValue = sha256.ComputeHash(b);

                // Convert the byte array to string format
                foreach (byte bt in hashValue)
                {
                    hash += $"{bt:X2}";
                }
            }

            return hash;
        }


        static byte[] EncryptToBytes(byte[] data, byte[] Key, byte[] IV)
        {
            if (data == null || data.Length <= 0)
                throw new ArgumentNullException("data");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");
            byte[] encrypted;
            using (Rijndael rijAlg = Rijndael.Create())
            {
                rijAlg.Key = Key;
                rijAlg.IV = IV;

                ICryptoTransform encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        csEncrypt.Write(data, 0, data.Length);
                    }
                    encrypted = msEncrypt.ToArray();
                }
            }
            return encrypted;
        }

        static byte[] DecryptFromBytes(byte[] cipherText, byte[] Key, byte[] IV)
        {
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            byte[] decrypted = null;

            using (Rijndael rijAlg = Rijndael.Create())
            {
                rijAlg.Key = Key;
                rijAlg.IV = IV;

                ICryptoTransform decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Write))
                    {
                        csDecrypt.Write(cipherText, 0, cipherText.Length);
                    }

                    decrypted = msDecrypt.ToArray();
                }
            }
            return decrypted;
        }

        static string ToHexademical(byte item)
        {
            int number = Convert.ToInt32(item);
            string result = "";
            int[] array = new int[] { number / 16, number % 16 };
            for (int i = 0; i < array.Length; i++)
            {
                result += NumbToLetter(array[i]);
            }
            
            return result;
        }

        static string NumbToLetter(int numb)
        {
            if (numb == 10)
            {
                return "a";
            }
            else if (numb == 11)
            {
                return "b";
            }
            else if (numb == 12)
            {
                return "c";
            }
            else if (numb == 13)
            {
                return "d";
            }
            else if (numb == 14)
            {
                return "e";
            }
            else if (numb == 15)
            {
                return "f";
            }
            else
            {
                return numb.ToString();
            }
        }

        static byte FromHexademical(string str)
        {
            int numb = 0;
            int help = 16;
            foreach (var charr in str)
            {
                numb += ToInt(charr) * help;
                help = 1;
            }
            return Convert.ToByte(numb);
        }

        static int ToInt(char charr)
        {
            if (charr == 'a')
            {
                return 10;
            }
            else if (charr == 'b')
            {
                return 11;
            }
            else if (charr == 'c')
            {
                return 12;
            }
            else if (charr == 'd')
            {
                return 13;
            }
            else if (charr == 'e')
            {
                return 14;
            }
            else if (charr == 'f')
            {
                return 15;
            }
            else
            {
                return int.Parse(charr.ToString());
            }
        }


    }
}
