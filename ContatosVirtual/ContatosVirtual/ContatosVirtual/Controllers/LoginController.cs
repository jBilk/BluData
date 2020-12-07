using ContatosVirtual.DAO;
using ContatosVirtual.Interfaces;
using System.Web.Mvc;

namespace ContatosVirtual.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuario _usuarios;
        private readonly ICodificadorSenha _codificadorSenhas;

        public LoginController(IUsuario usuario, ICodificadorSenha codificadorSenhas)
        {
            _usuarios = usuario;
            _codificadorSenhas = codificadorSenhas;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Senha()
        {
            return View();
        }

        public ActionResult Sair()
        {
            Session["usuarioLogado"] = null;

            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public ActionResult Autenticar(string nomeUsuario, string senha)
        {
            var senhaCod = _codificadorSenhas.HashValue(senha);
            var usuario = _usuarios.Busca(nomeUsuario, senhaCod);
            if (usuario != null)
            {
                try
                {
                    Session["usuarioLogado"] = usuario;
                    Session["nome"] = usuario.Nome;
                    Session["permissao"] = usuario.Permissao;
                    Session["id"] = usuario.Id;
                    return Json("/Home");
                }
                catch
                {
                    return Json("error");
                }
            }

            return Json("erroUsuario");

        }
    }    
}