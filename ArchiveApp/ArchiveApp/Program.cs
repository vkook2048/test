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
            if (args.Length < 3 || ((args[0] == "/encrypt" || args[0] == "/decrypt") && (args.Length < 4)))
            {
                Console.WriteLine("bad args");
                Console.ReadKey();
                return;
            }

            string command = args[0];
            string inputFile = args[1];
            string outputFile = args[2];

            if (command == "/pack")
            {
                Packer packer = new Packer(inputFile, outputFile);
                packer.PackZip();
            }
            if (command == "/unpack")
            {
                Packer packer = new Packer(inputFile, outputFile);
                packer.UnpackZip();
                
            }
            if (command == "/encrypt")
            {
                string key = args[3];
                byte[] bytes = File.ReadAllBytes(inputFile);
                Crypter encrypter = new Crypter(bytes, key);
                File.WriteAllBytes(outputFile, encrypter.Encrypt());
            }
            if (command == "/decrypt")
            {
                string key = args[3];
                byte[] bytes = File.ReadAllBytes(inputFile);
                Crypter decrypter = new Crypter(bytes, key);
                File.WriteAllBytes(outputFile, decrypter.Decrypt());
            }
            if (command == "/to_hex")
            {
                byte[] bytes = File.ReadAllBytes(inputFile);
                File.WriteAllText(outputFile, Hex.ToHexString(bytes));
            }
            if (command == "/from_hex")
            {
               string inputString = File.ReadAllText(inputFile);
               File.WriteAllBytes(outputFile, Hex.FromHexString(inputString));
            }
            if (command == "/to_base64")
            {
                byte[] input = File.ReadAllBytes(inputFile);
                File.WriteAllText(outputFile, Base64.ToBase64String(input));
            }
            if (command == "/from_base64")
            {
                string input = File.ReadAllText(inputFile);
                File.WriteAllBytes(outputFile, Base64.FromBase64String(input));
            }
            if (command == "/crc32")
            {
                Hash hasher = new Hash(inputFile);
                File.WriteAllText(outputFile, hasher.CountCRC32());
            }
            if (command == "/md5")
            {
                Hash hasher = new Hash(inputFile);
                File.WriteAllText(outputFile, hasher.CountMD5());
            }
            if (command == "/sha256")
            {
                Hash hasher = new Hash(inputFile);
                File.WriteAllText(outputFile, hasher.CountSHA256());
            }
        }
    }
}
