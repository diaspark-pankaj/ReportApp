using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace YoelProject.Common.Helpers
{
    public static class Crypto
    {
        static string StringFromDictionary(Dictionary<string, string> dictionary)
        {
            //return string.Join("-", dictionary.Select(d => d.Key + "+" + d.Value));
            return string.Join("~", dictionary.Select(d => d.Key + "+" + d.Value));
        }

        static byte[] EncryptBytes(byte[] bytes)
        {
            // Change keyBytes or generate based on some logic
            // We are using IV (Initialization Vector) same as keyBytes 
            // but strongly recommend to change or generate separate one
            byte[] keyBytes = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
            RijndaelManaged rijndaelManaged = new RijndaelManaged
            {
                Mode = CipherMode.CBC,
                Padding = PaddingMode.PKCS7,
                KeySize = 128,
                BlockSize = 128,
                Key = keyBytes,
                IV = keyBytes
            };
            return rijndaelManaged.CreateEncryptor().TransformFinalBlock(bytes, 0, bytes.Length);
        }

        static string ToBase64(byte[] bytes)
        {
            string strBase64 = Convert.ToBase64String(bytes);
            return strBase64.Replace('+', '-').Replace('/', '_').Replace('=', ',');

           
        }

        static Dictionary<string, string> DictionaryFromBytes(byte[] bytes)
        {
            string decryptedString = Encoding.UTF8.GetString(bytes);
            //string[] keyValuePair = decryptedString.Split('-');
            string[] keyValuePair = decryptedString.Split('~');
            return keyValuePair.Select(key => key.Split('+')).ToDictionary(keyValue => keyValue[0], keyValue => keyValue[1]);
        }

        static byte[] DecryptBytes(byte[] bytes)
        {
            // Change keyBytes or generate based on some logic
            // We are using IV (Initialization Vector) same as keyBytes 
            // but strongly recommend to change or generate separate one
            byte[] keyBytes = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
            RijndaelManaged rijndaelManaged = new RijndaelManaged
            {
                Mode = CipherMode.CBC,
                Padding = PaddingMode.PKCS7,
                KeySize = 128,
                BlockSize = 128,
                Key = keyBytes,
                IV = keyBytes
            };
            return rijndaelManaged.CreateDecryptor().TransformFinalBlock(bytes, 0, bytes.Length);
        }

        static byte[] FromBase64(string encryptedText)
        {
           encryptedText = encryptedText.Replace('-', '+').Replace('_', '/').Replace(',', '=');
           
            return Convert.FromBase64String(encryptedText);
        }

        public static string Encrypt(Dictionary<string, string> dictionary)
        {
            byte[] queryString = Encoding.UTF8.GetBytes(StringFromDictionary(dictionary));
            byte[] encryptedBytes = EncryptBytes(queryString);
            string res = ToBase64(encryptedBytes);
            return res;
        }

        public static Dictionary<string, string> Decrypt(string encryptedText, out bool status)
        {
            try
            {
                byte[] encryptedBytes = FromBase64(encryptedText);
                byte[] decryptedBytes = DecryptBytes(encryptedBytes);
                Dictionary<string, string> dictionary = DictionaryFromBytes(decryptedBytes);
                status = true;
                return dictionary;
            }
            catch (Exception)
            {
                status = false;
                return null;
            }
        }
    }
}
