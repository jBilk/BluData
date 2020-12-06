using ContatosVirtual.Interfaces;
using System;
using System.Text;
using System.Web.Mvc;

namespace ContatosVirtual.Controllers
{
    public class SenhasCriptogafadasController : Controller, IGerarSenhaCriptografada
    {
        public string GerarSenhaCriptografada()
        {
            string codigoSenha = DateTime.Now.Ticks.ToString();
            try
            {
                string senha = BitConverter.ToString(new System.Security.Cryptography.SHA512CryptoServiceProvider().ComputeHash(Encoding.Default.GetBytes(codigoSenha))).Replace("-", String.Empty);
                var senhaGerada = senha.Substring(0, 8);
                return senhaGerada.ToString();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}