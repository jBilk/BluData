using ContatosVirtual.Interfaces;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace ContatosVirtual.Servicos
{
    public class CodificadorSenhaServicos : ICodificadorSenha
    {
        public string HashValue(string senhaGerada)
        {
            var encoding = new UnicodeEncoding();
            byte[] hashBytes;
            using (var hash = SHA1.Create())
                hashBytes = hash.ComputeHash(encoding.GetBytes(senhaGerada));

            var hashValue = new StringBuilder(hashBytes.Length * 2);
            foreach (byte b in hashBytes)
            {
                hashValue.AppendFormat(CultureInfo.InvariantCulture, "{0:X2}", b);
            }

            return hashValue.ToString();
        }
    }
}