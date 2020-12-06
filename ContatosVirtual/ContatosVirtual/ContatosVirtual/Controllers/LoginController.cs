using ContatosVirtual.DAO;
using ContatosVirtual.Interfaces;
using System.Web.Mvc;

namespace ContatosVirtual.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuarios _usuarios;
        private readonly ICodificadorSenhas _codificadorSenhas;

        public LoginController(IUsuarios usuario, ICodificadorSenhas codificadorSenhas)
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

        public ActionResult Sair()
        {
            Session["usuarioLogado"] = null;

            return RedirectToAction("Index", "Login");
        }
    }    
}