using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Core.Extensions
{
    public static class CryptExtensions
    {
        const string encryptionKey = "Sn8+Gv7#-lQ0m_2!OwX0";

        public static string Crypt(this string input)
        {
            try
            {
                string parameter = CryptPassword(input);
                parameter = Base64Encode(parameter);
                parameter = HttpUtility.UrlEncode(parameter);
                return parameter;
            }
            catch { }

            return "";
        }
        public static string Decrypt(this string input)
        {
            try
            {
                string parameter = HttpUtility.UrlDecode(input);
                parameter = Base64Decode(parameter);
                parameter = DecryptPassword(parameter);
                return parameter;
            }
            catch { }

            return "";
        }

        private static string CryptPassword(string input)
        {
            byte[] clearBytes = Encoding.Unicode.GetBytes(input);

            using Aes encryptor = Aes.Create();

            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x76 });

            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);

            using MemoryStream ms = new MemoryStream();
            using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
            {
                cs.Write(clearBytes, 0, clearBytes.Length);
                cs.Close();
            }

            input = Convert.ToBase64String((ms.ToArray()));

            return input;
        }

        private static string DecryptPassword(string input)
        {
            if ((input ?? "") == "")
                return "";

            byte[] cipherBytes = Convert.FromBase64String(input);

            using Aes encryptor = Aes.Create();

            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x76 });

            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);

            using MemoryStream ms = new MemoryStream();
            using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
            {
                cs.Write(cipherBytes, 0, cipherBytes.Length);
                cs.Close();
            }
            input = Encoding.Unicode.GetString((ms.ToArray()));

            return input;
        }

        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        private static string QueryStringMatch(string Content, string Key, string Default)
        {
            Regex regex = new Regex($"<{Key}(.*?){Key}>");
            Match match = regex.Match(Content);
            if (match.Success)
            {
                string value = match.Value;
                value = value.Substring(Key.Length + 1);
                value = value.Substring(0, value.Length - (Key.Length + 1));

                return value;
            }

            return Default;
        }

        private static string AddQueryString(string Input, string Key, string Value)
        {
            return Input + $"<{Key}{Value}{Key}>";
        }

        private static string GeneratePassword(int Length, int NonAlphaNumericChars)
        {
            string allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789";
            string allowedNonAlphaNum = "!@#$%^&*()_-+=[{]};:<>|./?";
            Random rd = new Random();

            if (NonAlphaNumericChars > Length || Length <= 0 || NonAlphaNumericChars < 0)
                throw new ArgumentOutOfRangeException();

            char[] pass = new char[Length];
            int[] pos = new int[Length];
            int i = 0, j = 0, temp = 0;
            bool flag = false;

            //Random the position values of the pos array for the string Pass
            while (i < Length - 1)
            {
                j = 0;
                flag = false;
                temp = rd.Next(0, Length);
                for (j = 0; j < Length; j++)
                    if (temp == pos[j])
                    {
                        flag = true;
                        j = Length;
                    }

                if (!flag)
                {
                    pos[i] = temp;
                    i++;
                }
            }

            //Random the AlphaNumericChars
            for (i = 0; i < Length - NonAlphaNumericChars; i++)
                pass[i] = allowedChars[rd.Next(0, allowedChars.Length)];

            //Random the NonAlphaNumericChars
            for (i = Length - NonAlphaNumericChars; i < Length; i++)
                pass[i] = allowedNonAlphaNum[rd.Next(0, allowedNonAlphaNum.Length)];

            //Set the sorted array values by the pos array for the rigth posistion
            char[] sorted = new char[Length];
            for (i = 0; i < Length; i++)
                sorted[i] = pass[pos[i]];

            string Pass = new String(sorted);

            return Pass;
        }
    }
}