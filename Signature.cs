using System;
using System.Security.Cryptography;
using System.Linq;

namespace SignatureMakhovZaur
{
    static class Signature
    {
        public static string HashSign(byte[] bytes)
        {
            using (SHA256 sha = SHA256.Create())
            {
                byte[] hash = sha.ComputeHash(bytes);

                // на stackoverflow до сих пор спорят выводить с помощью linq или bitconverter. Выбрал первый вариант :)
                string hashString = string.Join("", hash.Select(f => f.ToString("X2")).ToArray());

                return hashString;
                

            }
        }
    }
}
