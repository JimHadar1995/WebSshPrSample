using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Library.Common.Security.Helpers
{
     /// <summary>
    /// helper для шифрования / расшифровки данных
    /// </summary>
    public static class CryptHelper
    {
        /// <summary>
        /// Шифрование данных по алгоритму AES
        /// </summary>
        /// <param name="aeskey">The key.</param>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        public static string AesEncrypt(string aeskey, string text)
        {
            //Объявляем объект класса AES
            Aes aes = Aes.Create();
            //Генерируем соль
            aes.GenerateIV();
            //Присваиваем ключ. aeskey - переменная (массив байт), сгенерированная методом GenerateKey() класса AES
            aes.Key = Encoding.UTF8.GetBytes(aeskey);
            byte[] encrypted;
            ICryptoTransform crypt = aes.CreateEncryptor(aes.Key, aes.IV);
            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, crypt, CryptoStreamMode.Write))
                {
                    using var sw = new StreamWriter(cs);
                    sw.Write(text);
                }
                //Записываем в переменную encrypted зашиврованный поток байтов
                encrypted = ms.ToArray();
            }
            //Возвращаем поток байт + крепим соль
            return Convert.ToBase64String(encrypted.Concat(aes.IV).ToArray());
		}

        /// <summary>
        /// Расшифровка данных по алгоритму AES
        /// </summary>
        /// <param name="aeskey">The aeskey.</param>
        /// <param name="cipherText">The cipher text.</param>
        /// <returns></returns>
        public static string AesDecrypt(string aeskey, string cipherText)
        {
            var shifr = Convert.FromBase64String(cipherText);
            byte[] bytesIv = new byte[16];
            byte[] mess = new byte[shifr.Length - 16];
            //Списываем соль
            for (int i = shifr.Length - 16, j = 0; i < shifr.Length; i++, j++)
                bytesIv[j] = shifr[i];
            //Списываем оставшуюся часть сообщения
            for (int i = 0; i < shifr.Length - 16; i++)
                mess[i] = shifr[i];
            //Объект класса Aes
            Aes aes = Aes.Create();
            //Задаем тот же ключ, что и для шифрования
            aes.Key = Encoding.UTF8.GetBytes(aeskey);
            //Задаем соль
            aes.IV = bytesIv;
            //Строковая переменная для результата
            string text = "";
            byte[] data = mess;
            ICryptoTransform crypt = aes.CreateDecryptor(aes.Key, aes.IV);
            using (var ms = new MemoryStream(data))
            {
                using var cs = new CryptoStream(ms, crypt, CryptoStreamMode.Read);
                using var sr = new StreamReader(cs);
                //Результат записываем в переменную text в вие исходной строки
                text = sr.ReadToEnd();
            }
            return text;
        }
    }
}
