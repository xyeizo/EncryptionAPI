using System.Security.Cryptography;
using System.Text;

namespace EncryptionAPI
{
    public static class EncryptionAPI
    {
        private const int SaltSize = 32;
        private const int Iterations = 100000;
        private const int KeySize = 256;

        public static string EncryptString(string plainText, string password)
        {
            byte[] salt = GenerateRandomSalt();
            byte[] encryptedBytes;

            using (Aes aes = CreateAesInstance(password, salt))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    ms.Write(salt, 0, SaltSize);
                    using (CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
                        cs.Write(plainBytes, 0, plainBytes.Length);
                    }
                    encryptedBytes = ms.ToArray();
                }
            }

            return Convert.ToBase64String(encryptedBytes);
        }

        public static string DecryptString(string encryptedText, string password)
        {
            byte[] encryptedBytes = Convert.FromBase64String(encryptedText);
            byte[] salt = new byte[SaltSize];
            Array.Copy(encryptedBytes, 0, salt, 0, SaltSize);

            using (Aes aes = CreateAesInstance(password, salt))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(encryptedBytes, SaltSize, encryptedBytes.Length - SaltSize);
                    }
                    byte[] decryptedBytes = ms.ToArray();
                    return Encoding.UTF8.GetString(decryptedBytes);
                }
            }
        }

        private static Aes CreateAesInstance(string password, byte[] salt)
        {
            Aes aes = Aes.Create();
            aes.KeySize = KeySize;
            aes.BlockSize = 128;
            using (var key = new Rfc2898DeriveBytes(password, salt, Iterations))
            {
                aes.Key = key.GetBytes(KeySize / 8);
                aes.IV = key.GetBytes(aes.BlockSize / 8);
            }
            aes.Mode = CipherMode.CFB;
            aes.Padding = PaddingMode.PKCS7;
            return aes;
        }

        public static string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private static byte[] GenerateRandomSalt()
        {
            byte[] data = new byte[SaltSize];
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(data);
            }
            return data;
        }
    }
}
