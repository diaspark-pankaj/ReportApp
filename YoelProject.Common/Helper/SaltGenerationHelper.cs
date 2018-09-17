using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace YoelProject.Common.Helpers
{
    public class SaltGenerationHelper
    {
        #region Workable Code
        // 24 = 192 bits
        private const int SaltByteSize = 24;//128 / 8;
        private const int HashByteSize = 24;//256 / 8;
        private const int HasingIterationsCount = 1000;

        // http://stackoverflow.com/questions/19957176/asp-net-identity-password-hashing
        public static string[] HashPassword(string password)
        {

            byte[] salt;
            byte[] buffer2;
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, SaltByteSize, HasingIterationsCount))
            {
                salt = bytes.Salt;
                buffer2 = bytes.GetBytes(HashByteSize);
            }
            byte[] dst = new byte[(SaltByteSize + HashByteSize) + 1];
            Buffer.BlockCopy(salt, 0, dst, 1, SaltByteSize);
            Buffer.BlockCopy(buffer2, 0, dst, SaltByteSize + 1, HashByteSize);
            return new String[] { Convert.ToBase64String(dst), Convert.ToBase64String(salt) };
        }

        public static bool VerifyHashedPassword(string hashedPassword, string password)
        {
            byte[] _passwordHashBytes;

            int _arrayLen = (SaltByteSize + HashByteSize) + 1;

            if (hashedPassword == null)
            {
                return false;
            }

            if (password == null)
            {
                throw new ArgumentNullException("password");
            }

            byte[] src = Convert.FromBase64String(hashedPassword);

            if ((src.Length != _arrayLen) || (src[0] != 0))
            {
                return false;
            }

            byte[] _currentSaltBytes = new byte[SaltByteSize];
            Buffer.BlockCopy(src, 1, _currentSaltBytes, 0, SaltByteSize);

            byte[] _currentHashBytes = new byte[HashByteSize];
            Buffer.BlockCopy(src, SaltByteSize + 1, _currentHashBytes, 0, HashByteSize);

            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, _currentSaltBytes, HasingIterationsCount))
            {
                _passwordHashBytes = bytes.GetBytes(SaltByteSize);
            }

            return AreHashesEqual(_currentHashBytes, _passwordHashBytes);

        }

        private static bool AreHashesEqual(byte[] firstHash, byte[] secondHash)
        {
            int _minHashLength = firstHash.Length <= secondHash.Length ? firstHash.Length : secondHash.Length;
            var xor = firstHash.Length ^ secondHash.Length;
            for (int i = 0; i < _minHashLength; i++)
                xor |= firstHash[i] ^ secondHash[i];
            return 0 == xor;
        }
        #endregion

        #region Unused Code

        public static string GenerateSaltForPassword(int length) //length of salt    
        {
            const string allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789";
            var randNum = new Random();
            var chars = new char[length];
            var allowedCharCount = allowedChars.Length;
            for (var i = 0; i <= length - 1; i++)
            {
                chars[i] = allowedChars[Convert.ToInt32((allowedChars.Length) * randNum.NextDouble())];
            }
            return new string(chars);
        }

        public static string GenerateSecurityStampSalt()
        {
            return System.Guid.NewGuid().ToString();
        }
        public static string EncodePassword(string pass, string salt) //encrypt password    
        {
            byte[] bytes = Encoding.Unicode.GetBytes(pass);
            byte[] src = Encoding.Unicode.GetBytes(salt);
            byte[] dst = new byte[src.Length + bytes.Length];
            System.Buffer.BlockCopy(src, 0, dst, 0, src.Length);
            System.Buffer.BlockCopy(bytes, 0, dst, src.Length, bytes.Length);
            HashAlgorithm algorithm = HashAlgorithm.Create("SHA1");
            //HashAlgorithm algorithm = HashAlgorithm.Create("SHA256");
            
            byte[] inArray = algorithm.ComputeHash(dst);
            
            return Convert.ToBase64String(inArray);    
           
            // return EncodePasswordMd5(Convert.ToBase64String(inArray));
           // return EncodePasswordSHA256(Convert.ToBase64String(inArray));
            
        }
        public static string EncodePasswordMd5(string pass) //Encrypt using MD5    
        {
            Byte[] originalBytes;
            Byte[] encodedBytes;
            MD5 md5;
            //Instantiate MD5CryptoServiceProvider, get bytes for original password and compute hash (encoded password)    
            md5 = new MD5CryptoServiceProvider();
            originalBytes = ASCIIEncoding.Default.GetBytes(pass);
            encodedBytes = md5.ComputeHash(originalBytes);
            //Convert encoded bytes back to a 'readable' string    
            return BitConverter.ToString(encodedBytes);
        }

        public static string EncodePasswordSHA256(string pass) //Encrypt using SHA256    
        {
            Byte[] originalBytes;
            Byte[] encodedBytes;
            SHA256 sha256;
            //Instantiate MD5CryptoServiceProvider, get bytes for original password and compute hash (encoded password)    
            sha256 = new SHA256CryptoServiceProvider();
            originalBytes = ASCIIEncoding.Default.GetBytes(pass);
            encodedBytes = sha256.ComputeHash(originalBytes);
            //Convert encoded bytes back to a 'readable' string    
            return BitConverter.ToString(encodedBytes);
        }

        public static string EncodePasswordSHA512(string pass) //Encrypt using SHA512   
        {
            Byte[] originalBytes;
            Byte[] encodedBytes;
            SHA512 sha512;
            //Instantiate MD5CryptoServiceProvider, get bytes for original password and compute hash (encoded password)    
            sha512 = new SHA512CryptoServiceProvider();
            originalBytes = ASCIIEncoding.Default.GetBytes(pass);
            encodedBytes = sha512.ComputeHash(originalBytes);
            //Convert encoded bytes back to a 'readable' string    
            return BitConverter.ToString(encodedBytes);
        }

        public static string base64Encode(string sData) // Encode    
        {
            try
            {
                byte[] encData_byte = new byte[sData.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(sData);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }
        public static string base64Decode(string sData) //Decode    
        {
            try
            {
                var encoder = new System.Text.UTF8Encoding();
                System.Text.Decoder utf8Decode = encoder.GetDecoder();
                byte[] todecodeByte = Convert.FromBase64String(sData);
                int charCount = utf8Decode.GetCharCount(todecodeByte, 0, todecodeByte.Length);
                char[] decodedChar = new char[charCount];
                utf8Decode.GetChars(todecodeByte, 0, todecodeByte.Length, decodedChar, 0);
                string result = new String(decodedChar);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Decode" + ex.Message);
            }
        }
        #endregion


    }
}