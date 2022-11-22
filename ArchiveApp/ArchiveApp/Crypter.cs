using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ArchiveApp
{

    class Crypter
    {
        byte[] Key;
        byte[] IV;
        byte[] Bytes;

        public Crypter(byte[] bytes, string key)
        {
            Key = Encoding.ASCII.GetBytes(key);
            IV = new byte[16];
            Random rnd = new Random();
            rnd.NextBytes(IV);
            Bytes = bytes;
        }

        public byte[] Encrypt()
        {
            byte[] encrypted = EncryptToBytes(Bytes, Key, IV);
            byte[] full = new byte[IV.Length + encrypted.Length];
            Array.Copy(IV, 0, full, 0, IV.Length);
            Array.Copy(encrypted, 0, full, IV.Length, encrypted.Length);
            return full;
        }

        public byte[] Decrypt()
        {
            byte[] enc = new byte[Bytes.Length - 16];
            byte[] Iv = new byte[16];
            Array.Copy(Bytes, 0, Iv, 0, Iv.Length);
            Array.Copy(Bytes, Iv.Length, enc, 0, enc.Length);
            byte[] decrypted = DecryptFromBytes(enc, Key, Iv);
            return decrypted;
        }

        private byte[] EncryptToBytes(byte[] data, byte[] Key, byte[] IV)
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
        private byte[] DecryptFromBytes(byte[] cipherText, byte[] Key, byte[] IV)
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
    }
}
