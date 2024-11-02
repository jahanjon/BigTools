using System.Security.Cryptography;
using System.Text;

namespace Common.Utilities;

public static class SecurityHelper
{
    public static string GetSha256Hash(string input)
    {
        //using (var sha256 = new SHA256CryptoServiceProvider())
        using (var sha256 = SHA256.Create())
        {
            var byteValue = Encoding.UTF8.GetBytes(input);
            var byteHash = sha256.ComputeHash(byteValue);
            return Convert.ToBase64String(byteHash);
            //return BitConverter.ToString(byteHash).Replace("-", "").ToLower();
        }
    }

    public static string EncryptString(Guid key, string plainText)
    {
        var iv = new byte[16];
        byte[] array;

        using (var aes = Aes.Create())
        {
            aes.Key = Encoding.UTF8.GetBytes(key.ToString().Replace("-", "").ToCharArray());
            aes.IV = iv;

            var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            using (var memoryStream = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    using (var streamWriter = new StreamWriter(cryptoStream))
                    {
                        streamWriter.Write(plainText);
                    }

                    array = memoryStream.ToArray();
                }
            }
        }

        return Convert.ToBase64String(array);
    }

    public static string DecryptString(Guid key, string cipherText)
    {
        var iv = new byte[16];
        var buffer = Convert.FromBase64String(cipherText);

        using (var aes = Aes.Create())
        {
            aes.Key = Encoding.UTF8.GetBytes(key.ToString().Replace("-", "").ToCharArray());
            aes.IV = iv;
            var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            using (var memoryStream = new MemoryStream(buffer))
            {
                using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                {
                    using (var streamReader = new StreamReader(cryptoStream))
                    {
                        return streamReader.ReadToEnd();
                    }
                }
            }
        }
    }
}