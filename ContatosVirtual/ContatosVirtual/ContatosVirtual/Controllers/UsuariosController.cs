using ContatosVirtual.Enum;
using ContatosVirtual.Filtros;
using ContatosVirtual.Interfaces;
using ContatosVirtual.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ContatosVirtual.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly IUsuarios _usuarios;
        private readonly IEmail _email1;
        private readonly ICodificadorSenhas _codificadorSenhas;
        private readonly IGerarSenhaCriptografada _gerarSenhaCriptografada;

        public UsuariosController(IUsuarios usuario, IEmail email, ICodificadorSenhas codificadorSenhas, 
                                    IGerarSenhaCriptografada gerarSenhaCriptografada)
        {
            _usuarios = usuario;
            _email1 = email;
            _codificadorSenhas = codificadorSenhas;
            _gerarSenhaCriptografada = gerarSenhaCriptografada;
        }

        [AutorizacaoFiltro]
        public ActionResult Index()
        {
            return View();
        }

        [AutorizacaoFiltro]
        public ActionResult Form()
        {
            return View();
        }

        [AutorizacaoFilter]
        public ActionResult EditarSenha(int id)
        {
            ViewBag.Usuario = _usuarios.BuscaPorId(id);
            return View();
        }

        [HttpPost]
        [AutorizacaoFiltro]
        public ActionResult Adiciona(Usuario usuario)
        {
            try
            {
                if (NomeUsuarioJahExiste(usuario.Id, usuario.NomeUsuario.ToString()))
                    return Json("nomeUsuario");

                if (EmailJahExiste(usuario.Id, usuario.Email.ToString()))
                    return Json("emailUsuario");

                var senhaCriptografada = _gerarSenhaCriptografada.GerarSenhaCriptografada();
                _email1.ProcessoDeEnvioDeEmailNovoUsuario(usuario, senhaCriptografada);
                usuario.Senha = _codificadorSenhas.HashValue(senhaCriptografada);
                usuario.Status = EnumStatusUsuario.StatusAtivado.ToString();
                usuario.DataHoraCadastro = DateTime.Now;
                _usuarios.Adicionar(usuario);
            }
            catch (Exception)
            {
                return Json("erro");
            }

            return Json("/Usuarios");
        }

        [AutorizacaoFiltro]
        public ActionResult Ativar(int id)
        {
            return View();
        }

        [AutorizacaoFiltro]
        public ActionResult Desativar(int id)
        {
            return View();
        }

        [HttpPost]
        [AutorizacaoFiltro]
        public ActionResult AtivarConcluido(int id, int usuarioLogado)
        {
            try
            {
                var usuario = _usuarios.BuscaPorId(id);
                usuario.Status = EnumStatusUsuario.StatusAtivado.ToString();
                usuario.UsuarioEdicaoId = usuarioLogado;
                usuario.DataHoraEdicao = DateTime.Now;
                _usuarios.AlterarStatus(usuario);
            }
            catch (Exception)
            {
                return Json("erro");
            }

            return Json("/Usuarios");
        }

        [HttpPost]
        [AutorizacaoFiltro]
        public ActionResult DesativarConcluido(int id, int usuarioLogado)
        {
            try
            {
                var usuario = _usuarios.BuscaPorId(id);
                usuario.Status = EnumStatusUsuario.StatusDesativado.ToString();
                usuario.UsuarioEdicaoId = usuarioLogado;
                usuario.DataHoraEdicao = DateTime.Now;
                _usuarios.AlterarStatus(usuario);
            }
            catch (Exception)
            {
                return Json("erro");
            }
            return Json("/Usuarios");
        }

        [AutorizacaoFiltro]
        public ActionResult Editar(int id)
        {
            ViewBag.Usuario = _usuarios.BuscaPorId(id); ;
            return View();
        }

        [HttpPost]
        [AutorizacaoFilter]
        public ActionResult EditarSalvar(Usuario usuario)
        {
            try
            {
                usuario.DataHoraEdicao = DateTime.Now;
                _usuarios.Editar(usuario);
            }
            catch (Exception)
            {
                return Json("erro");
            }
            return Json(new { usuario = usuario });
        }

        [HttpPost]
        [AutorizacaoFilter]
        public ActionResult EditarPerfilSalvar(Usuario usuario)
        {
            try
            {
                if (NomeUsuarioJahExiste(usuario.Id, usuario.NomeUsuario.ToString()))
                    return Json("nomeUsuario");

                if (EmailJahExiste(usuario.Id, usuario.Email.ToString()))
                    return Json("emailUsuario");

                if (usuario.Senha.Length < 4 || usuario.Senha.Length > 10)
                    return Json("senhaCara");

                usuario.Senha = _codificadorSenhas.HashValue(usuario.Senha.ToString());
                usuario.DataHoraEdicao = DateTime.Now;
                _usuarios.Editar(usuario);
            }
            catch (Exception)
            {
                return Json("erro");
            }
            return Json("/Login/Sair");
        }

        [HttpPost]
        [AutorizacaoFilter]
        public ActionResult EditarSalvarNovaSenha(int id, string novaSenha)
        {
            try
            {

                if (novaSenha.Length < 4 || novaSenha.Length > 10)
                    return Json("senhaCara");

                var usuario = _usuarios.BuscaPorId(id);
                usuario.Senha = _codificadorSenhas.HashValue(novaSenha);
                usuario.Status = EnumStatusUsuario.StatusAtivado.ToString();
                _usuarios.Editar(usuario);
            }
            catch (Exception)
            {
                return Json("erro");
            }
            return Json("/Home");
        }

        public bool NomeUsuarioJahExiste(int id, string nomeUsuario)
        {
            bool ehIgualRegistroAtual = id == 0 ? false : _usuarios.BuscaPorId(id).NomeUsuario.ToString().Equals(nomeUsuario);

            if (ehIgualRegistroAtual)
                return false;

            if (_usuarios.BuscarNomeUsuario(nomeUsuario) != null)
                return true;

            return false;
        }

        public bool EmailJahExiste(int id, string email)
        {
            bool ehIgualRegistroAtual = id == 0 ? false : _usuarios.BuscaPorId(id).Email.ToString().Equals(email);

            if (ehIgualRegistroAtual)
                return false;

            if (_usuarios.BuscaPorEmail(email) != null)
                return true;

            return false;
        }

        public ActionResult Filtro(string status, string pesquisa, int start = 0, int length = 0, int draw = 0)
        {
            var userLogado = (int)Session["id"];
            IList<Usuario> usuarios = _usuarios.ListaComFiltro(status, pesquisa, userLogado, start, length);
            int quantidadeFiltrada = _usuarios.RetornarQuantidadeRegistroComFiltro(userLogado, status, pesquisa);
            int quantidadeTotal = _usuarios.QuantidadeTotalRegistros();
            return Json(new
            {
                draw = draw,
                recordsTotal = quantidadeTotal,
                recordsFiltered = quantidadeFiltrada,
                data = usuarios
            });
        }

        [AutorizacaoFilter]
        public ActionResult Perfil()
        {
            var id = (int)Session["id"];
            ViewBag.Usuario = _usuarios.BuscaPorId(id);
            return View();
        }
    }
}