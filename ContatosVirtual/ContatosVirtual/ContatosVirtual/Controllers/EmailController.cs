using ContatosVirtual.Interfaces;
using ContatosVirtual.Servicos;
using System.Web.Mvc;

namespace ContatosVirtual.Controllers
{
    public class EmailController : Controller
    {
        private readonly IUsuario _usuarios;
        private readonly EmailServicos _emailServicos;

        public EmailController(IUsuario user, EmailServicos emailServicos)
        {
            _usuarios = user;
            _emailServicos = emailServicos;
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
                    var mensagem = _emailServicos.GerarMensagemRecuperarSenha(usuBuscado.Id.ToString());

                    var retornoDeEnvio = _emailServicos.EnviaMensagemEmail(email.ToString(), mensagem, assunto, enviadoDe.ToString());

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