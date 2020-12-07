using ContatosVirtual.Enum;
using ContatosVirtual.Filtros;
using ContatosVirtual.Interfaces;
using ContatosVirtual.Models;
using ContatosVirtual.Servicos;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ContatosVirtual.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly IUsuario _usuarios;
        private readonly EmailServicos _emailServicos;
        private readonly UsuarioServicos _usuarioServicos;
        private readonly CodificadorSenhaServicos _codificadorSenhas;
        private readonly IGerarSenhaCriptografada _gerarSenhaCriptografada;

        public UsuariosController(IUsuario usuario, EmailServicos emailServicos, UsuarioServicos usuarioServicos,
                                  CodificadorSenhaServicos codificadorSenhas, IGerarSenhaCriptografada gerarSenhaCriptografada)
        {
            _usuarios = usuario;
            _emailServicos = emailServicos;
            _usuarioServicos = usuarioServicos;
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

        [AutorizacaoFiltro]
        public ActionResult Editar(int id)
        {
            ViewBag.Usuario = _usuarios.BuscaPorId(id); ;
            return View();
        }

        [AutorizacaoFilter]
        public ActionResult Perfil()
        {
            var id = (int)Session["id"];
            ViewBag.Usuario = _usuarios.BuscaPorId(id);
            return View();
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
        public ActionResult Adiciona(Usuario usuario)
        {
            try
            {
                if (_usuarioServicos.NomeUsuarioJahExiste(usuario.Id, usuario.NomeUsuario.ToString()))
                    return Json("nomeUsuario");

                if (_usuarioServicos.EmailJahExiste(usuario.Id, usuario.Email.ToString()))
                    return Json("emailUsuario");

                var senhaCriptografada = _gerarSenhaCriptografada.GerarSenhaCriptografada();
                _emailServicos.ProcessoDeEnvioDeEmailNovoUsuario(usuario, senhaCriptografada);
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

        [HttpPost]
        [AutorizacaoFilter]
        public ActionResult EditarPerfilSalvar(Usuario usuario)
        {
            try
            {
                if (_usuarioServicos.NomeUsuarioJahExiste(usuario.Id, usuario.NomeUsuario.ToString()))
                    return Json("nomeUsuario");

                if (_usuarioServicos.EmailJahExiste(usuario.Id, usuario.Email.ToString()))
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
        [AutorizacaoFiltro]
        public ActionResult AtivarConcluido(Usuario usuario)
        {
            try
            {
                var user = _usuarios.BuscaPorId(usuario.Id);
                user.Status = EnumStatusUsuario.StatusAtivado.ToString();
                user.UsuarioEdicaoId = usuario.UsuarioEdicaoId;
                user.DataHoraEdicao = DateTime.Now;
                _usuarios.AlterarStatus(user);
            }
            catch (Exception)
            {
                return Json("erro");
            }

            return Json("/Usuarios");
        }

        [HttpPost]
        [AutorizacaoFiltro]
        public ActionResult DesativarConcluido(Usuario usuario)
        {
            try
            {
                var user = _usuarios.BuscaPorId(usuario.Id);
                user.Status = EnumStatusUsuario.StatusDesativado.ToString();
                user.UsuarioEdicaoId = usuario.UsuarioEdicaoId;
                user.DataHoraEdicao = DateTime.Now;
                _usuarios.AlterarStatus(user);
            }
            catch (Exception)
            {
                return Json("erro");
            }
            return Json("/Usuarios");
        }

        [HttpPost]
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
    }
}