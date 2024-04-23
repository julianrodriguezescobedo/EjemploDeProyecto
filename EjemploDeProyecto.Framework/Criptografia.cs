using System;
using System.Security.Cryptography;
using System.Text;

namespace EjemploDeProyecto.Framework
{
    public static class Criptografia
    {
        public static string Encrypt(string input)
        {
            byte[] IV = ASCIIEncoding.ASCII.GetBytes("qualityi");
            byte[] EncryptionKey = Convert.FromBase64String("rpaSPvIvVLlrcmtzPU9/c67Gkj7yL1S5");
            byte[] buffer = Encoding.UTF8.GetBytes(input);
            TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
            des.Key = EncryptionKey;
            des.IV = IV;
            return Convert.ToBase64String(des.CreateEncryptor().TransformFinalBlock(buffer, 0, buffer.Length));
        }

        public static string Decrypt(string input)
        {
            byte[] IV = ASCIIEncoding.ASCII.GetBytes("qualityi");
            byte[] EncryptionKey = Convert.FromBase64String("rpaSPvIvVLlrcmtzPU9/c67Gkj7yL1S5");
            byte[] buffer = Convert.FromBase64String(input);
            TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
            des.Key = EncryptionKey;
            des.IV = IV;
            return Encoding.UTF8.GetString(des.CreateDecryptor().TransformFinalBlock(buffer, 0, buffer.Length));
        }

        public static string Hash(string input)
        {
            MD5 mMD5Hash = MD5.Create();
            byte[] mBytes = mMD5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder mStringBuilder = new StringBuilder();
            for (int i = 0; i <= mBytes.Length - 1; i++)
            {
                mStringBuilder.Append(mBytes[i].ToString("X2"));
            }
            return mStringBuilder.ToString();
        }
    }
}
