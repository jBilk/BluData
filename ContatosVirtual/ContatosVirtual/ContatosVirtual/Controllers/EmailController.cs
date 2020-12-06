using ContatosVirtual.Interfaces;
using ContatosVirtual.Models;
using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace ContatosVirtual.Controllers
{
    public class EmailController : Controller, IEmail
    {
        private readonly IUsuarios _usuarios;
        public EmailController(IUsuarios user)
        {
            _usuarios = user;
        }

        private string EnviaMensagemEmail(string destinatario, string mensagem, string assunto, string enviadoDe)
        {
            try
            {
                bool bValidaEmail = ValidaEnderecoEmail(destinatario);

                if (bValidaEmail == false)
                    return "emailInvalido";

                var smtpClient = ConfigurarSmtpClient();
                var mailMessage = ConfigurarMensagemEmail(destinatario, mensagem, assunto, enviadoDe);

                smtpClient.Send(mailMessage);

                return "enviado";
            }
            catch (Exception)
            {
                return "erro";
            }
        }

        private MailMessage ConfigurarMensagemEmail(string destinatario, string mensagem, string assunto, string enviadoDe)
        {
            var MailMessage = new MailMessage();
            MailMessage.From = new MailAddress("contatosvirtualteste@gmail.com", enviadoDe.ToString(), Encoding.UTF8);
            MailMessage.To.Add(destinatario.ToString());
            MailMessage.Subject = assunto.ToString();
            MailMessage.SubjectEncoding = Encoding.UTF8;
            MailMessage.Body = mensagem.ToString();
            MailMessage.BodyEncoding = Encoding.UTF8;
            MailMessage.BodyEncoding = Encoding.GetEncoding("ISO-8859-1");

            return MailMessage;
        }

        private SmtpClient ConfigurarSmtpClient()
        {
            var SmtpClient = new SmtpClient();
            SmtpClient.Host = "smtp.gmail.com";
            SmtpClient.Port = 587;
            SmtpClient.EnableSsl = true;
            SmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            SmtpClient.UseDefaultCredentials = false;
            SmtpClient.Credentials = new NetworkCredential("contatosvirtualteste@gmail.com", "contatosVirtualTeste2020");

            return SmtpClient;
        }

        private string GerarMensagemEmailNovoUsuario(string nomeUsuario, string senha)
        {
            return "Para acesar sua conta acesse: https://localhost:44326 Seu dados para acesso são: Login: " + nomeUsuario.ToString() + " Senha: " + senha.ToString();
        }

        private string GerarMensagemRecuperarSenha(string idUsuario)
        {
            return "Para recuperar sua senha acesse o link: https://localhost:44326/Usuarios/EditarSenha/" + idUsuario.ToString() + " Caso não tenha solicitado só ignorar esse email!";
        }

        private bool ValidaEnderecoEmail(string enderecoEmail)
        {
            try
            {
                string texto_Validar = enderecoEmail;
                Regex expressaoRegex = new Regex(@"\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}");

                if (expressaoRegex.IsMatch(texto_Validar))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void ProcessoDeEnvioDeEmailNovoUsuario(Usuario usuario, string senhaCriptografada)
        {
            var mensagemEmailNovoUsuario = GerarMensagemEmailNovoUsuario(usuario.NomeUsuario.ToString(), senhaCriptografada);
            var enviadoDe = "Contatos Virtual ADM";
            EnviaMensagemEmail(usuario.Email.ToString(), mensagemEmailNovoUsuario.ToString(), "Dados para acesso", enviadoDe);
        }

        [HttpPost]
        public ActionResult BuscarPorEmailEnviarMensagem(string email)
        {
            try
            {
                var usuBuscado = _usuarios.BuscaPorEmail(email);
                if (usuBuscado != null)
                {
                    var enviadoDe = Session["usuarioLogado"];
                    var assunto = "Recuperação de senha!";
                    var mensagem = GerarMensagemRecuperarSenha(usuBuscado.Id.ToString());

                    var retornoDeEnvio = EnviaMensagemEmail(email.ToString(), mensagem, assunto, enviadoDe.ToString());

                    return Json(retornoDeEnvio.ToString());
                }

                return Json("emailNLocalizado");
            }
            catch
            {
                return Json("erro");
            }
        }
    }
}