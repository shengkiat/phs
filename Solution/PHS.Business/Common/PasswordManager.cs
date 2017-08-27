using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Web.Security;

namespace PHS.Business.Common
{
    public class PasswordManager
    {
        public const int SALT_BYTE_SIZE = 24;
        public const int HASH_BYTE_SIZE = 24;
        public const int PBKDF2_ITERATIONS = 1000;

        public static string GenerateSalt()
        {
            // Generate a random salt
            byte[] salt = new byte[SALT_BYTE_SIZE];
            RNGCryptoServiceProvider csprng = new RNGCryptoServiceProvider();
            csprng.GetBytes(salt);
            return Convert.ToBase64String(salt);
        }

        public static SecureString CreateHash(string passwordStr, string saltStr)
        {
            // Hash the password and encode the parameters
            byte[] salt = Convert.FromBase64String(saltStr);
            byte[] hash = PBKDF2(passwordStr, salt, PBKDF2_ITERATIONS, HASH_BYTE_SIZE);

            var securePassword = new SecureString();

            foreach (char c in Convert.ToBase64String(hash))
            {
                securePassword.AppendChar(c);
            }
                

            securePassword.MakeReadOnly();
            return securePassword;
        }

        public static string SecureStringToString(SecureString value)
        {
            IntPtr valuePtr = IntPtr.Zero;
            try
            {
                valuePtr = Marshal.SecureStringToGlobalAllocUnicode(value);
                return Marshal.PtrToStringUni(valuePtr);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(valuePtr);
            }
        }

        public static bool ValidatePassword(string password, string correctHash, string correctSalt)
        {

            byte[] salt = Convert.FromBase64String(correctSalt);

            byte[] hash = Convert.FromBase64String(correctHash);

            byte[] testHash = PBKDF2(password, salt, PBKDF2_ITERATIONS, hash.Length);
            return SlowEquals(hash, testHash);
        }

        private static bool SlowEquals(byte[] a, byte[] b)
        {
            uint diff = (uint)a.Length ^ (uint)b.Length;
            for (int i = 0; i < a.Length && i < b.Length; i++)
                diff |= (uint)(a[i] ^ b[i]);
            return diff == 0;
        }

        private static byte[] PBKDF2(string password, byte[] salt, int iterations, int outputBytes)
        {
            Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, salt);
            pbkdf2.IterationCount = iterations;
            return pbkdf2.GetBytes(outputBytes);
        }

        public static bool IsPasswordComplex(string plainPassword)
        {
            string regexPattern = @"(?=^.{8,}$)(?=.*\d)(?=.*[!@#$%^&*]+)(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$";

            return Regex.IsMatch(plainPassword, regexPattern);
        }

        public static string GeneratePassword()
        {
            string password = Membership.GeneratePassword(12, 2);
            return password;
        }
    }
}
