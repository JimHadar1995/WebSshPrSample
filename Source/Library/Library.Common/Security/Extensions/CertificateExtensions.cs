using System;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;

namespace Library.Common.Security.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class CertificateExtensions
    {
        /// <summary>
        /// Считывание сертификата из строки.
        /// </summary>
        /// <param name="certString">The cert string.</param>
        /// <returns>Се</returns>
        public static X509Certificate2 GetX509Cert(this string certString)
        {
            //сначала пытаемся распарсить стандартными средствами dotnet.
            //если не получается, то пытаемся сделать строковое представление "более читаемым" после блока catch.
            try
            {
                return new X509Certificate2(Encoding.UTF8.GetBytes(certString));
            }
            catch
            {
                //
            }

            var certLines = certString.Trim().Split("\n");
            certString = string.Empty;
            if (certLines.Length == 1)
            {
                var regex = 
                    new Regex("([-]{5}BEGIN CERTIFICATE[-]{5})(.*)([-]{5}END CERTIFICATE[-]{5})", RegexOptions.Compiled);
                
                certString = regex.Match(certLines[0]).Groups[2].Value;
            }
            else
            {
                certString = string.Join("\n", certLines[1..^1]);
            }

            var bytes = Convert.FromBase64String(certString);

            var cert = new X509Certificate2(bytes);
            return cert;
        }
    }
}
