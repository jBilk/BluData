﻿using ContatosVirtual.Content.Enum;
using ContatosVirtual.Controllers;
using ContatosVirtual.Enum;
using ContatosVirtual.Interfaces;
using ContatosVirtual.Models;
using Newtonsoft.Json;
using System.Linq;
using System.Web.Mvc;
using Xunit;

namespace ContatosVirtualTestes.TestesUsuarios
{
    public class UsuariosControllerTestes
    {
        private readonly UsuariosController _usuariosController;
        private readonly IUsuarios _usuarios;
        private readonly IEmail _email;
        private readonly ICodificadorSenhas _codificadorSenhas;
        private readonly IGerarSenhaCriptografada _gerarSenhaCriptografada;

        public UsuariosControllerTestes()
        {
            _usuarios = new UsuariosFake();
            _email = new EmailController(_usuarios);
            _codificadorSenhas = new CodificadorSenhasController();
            _gerarSenhaCriptografada = new SenhasCriptogafadasController();
            _usuariosController = new UsuariosController(_usuarios, _email, _codificadorSenhas, _gerarSenhaCriptografada);
        }

        [Fact]
        public void AdicionarUsuario_PassandoDados_ResultadoOk()
        {
            var usuarioTeste = new Usuario();

            usuarioTeste.Nome = "Usuário Teste Unitário";
            usuarioTeste.NomeUsuario = "usuarioTesteUnitario";
            usuarioTeste.Email = "usuarioTesteUnitario@usuarioTesteUnitario.com";
            usuarioTeste.Senha = "usuarioTesteUnitario";
            usuarioTeste.Permissao = EnumPermissaoUsuario.PermissaoAdministrador.ToString();
            usuarioTeste.UsuarioCadastroId = 1;

            var okResult = _usuariosController.Adiciona(usuarioTeste);
            string output = JsonConvert.SerializeObject(okResult);
            var deserializedokResult = JsonConvert.DeserializeObject<JsonResult>(output);

            Assert.True(deserializedokResult.Data.Equals("/Usuarios"));
        }

        [Fact]
        public void AdionarUsuario_PassandoDados_ResultadoNomeUsuarioJahExiste()
        {
            Usuario usuarioTeste = null;
            using (var contexto = new ContatosVirtualContext())
            {
                usuarioTeste = contexto.Usuarios.FirstOrDefault(u => u.Permissao == EnumPermissaoUsuario.PermissaoAdministrador.ToString());
            }
            usuarioTeste.Id = 0;

            var okResult = _usuariosController.Adiciona(usuarioTeste);
            string output = JsonConvert.SerializeObject(okResult);
            var deserializedokResult = JsonConvert.DeserializeObject<JsonResult>(output);

            Assert.True(deserializedokResult.Data.Equals("nomeUsuario"));
        }

        [Fact]
        public void AdionarUsuario_PassandoDados_ResultadoEmailJahExiste()
        {
            Usuario usuarioTeste = null;
            using (var contexto = new ContatosVirtualContext())
            {
                usuarioTeste = contexto.Usuarios.FirstOrDefault(u => u.Permissao == EnumPermissaoUsuario.PermissaoAdministrador.ToString());
            }
            usuarioTeste.Id = 0;
            usuarioTeste.NomeUsuario = "usuarioTesteUnitario";

            var okResult = _usuariosController.Adiciona(usuarioTeste);
            string output = JsonConvert.SerializeObject(okResult);
            var deserializedokResult = JsonConvert.DeserializeObject<JsonResult>(output);

            Assert.True(deserializedokResult.Data.Equals("emailUsuario"));
        }

        [Fact]
        public void AlterarStatusUsuarioParaDesativado_PassandoDados_ResultadoOk()
        {
            var usuarioTeste = 0;
            using (var contexto = new ContatosVirtualContext())
            {
                usuarioTeste = contexto.Usuarios.FirstOrDefault(u => u.Status == EnumStatusUsuario.StatusAtivado.ToString()).Id;
            }

            var okResult = _usuariosController.DesativarConcluido(usuarioTeste, usuarioTeste);
            string output = JsonConvert.SerializeObject(okResult);
            var deserializedokResult = JsonConvert.DeserializeObject<JsonResult>(output);

            Assert.True(deserializedokResult.Data.Equals("/Usuarios"));

        }

        [Fact]
        public void AlterarStatusUsuarioParaAtivado_PassandoDados_ResultadoOk()
        {
            var usuarioTeste = 0;
            using (var contexto = new ContatosVirtualContext())
            {
                usuarioTeste = contexto.Usuarios.FirstOrDefault(u => u.Status == EnumStatusUsuario.StatusDesativado.ToString()).Id;
            }

            var okResult = _usuariosController.AtivarConcluido(usuarioTeste, usuarioTeste);
            string output = JsonConvert.SerializeObject(okResult);
            var deserializedokResult = JsonConvert.DeserializeObject<JsonResult>(output);

            Assert.True(deserializedokResult.Data.Equals("/Usuarios"));
        }

        [Fact]
        public void EditarUsuario_PassandoDados_ResultadoOk()
        {
            Usuario usuarioTeste = null;
            using (var contexto = new ContatosVirtualContext())
            {
                usuarioTeste = contexto.Usuarios.FirstOrDefault(u => u.Permissao == EnumPermissaoUsuario.PermissaoAdministrador.ToString());
            }
            usuarioTeste.Email = "usuarioTesteUnitario@usuarioTesteUnitario.com";
            usuarioTeste.Senha = "usuarioTesteUnitario";

            var okResult = _usuariosController.EditarSalvar(usuarioTeste);
            string output = JsonConvert.SerializeObject(okResult);
            var deserializedokResult = JsonConvert.DeserializeObject<JsonResult>(output);

            Assert.True(!deserializedokResult.Data.Equals("erro"));
        }

        [Fact]
        public void EditarUsuario_PassandoDados_ResultadoNomeUsuarioJahExiste()
        {
            Usuario usuarioTeste = null;
            var usuarioTesteNomeUsuario = "";
            using (var contexto = new ContatosVirtualContext())
            {
                usuarioTeste = contexto.Usuarios.FirstOrDefault(u => u.Permissao == EnumPermissaoUsuario.PermissaoAdministrador.ToString());
                usuarioTesteNomeUsuario = contexto.Usuarios.FirstOrDefault(u => u.Permissao == EnumPermissaoUsuario.PermissaoUsuario.ToString()).NomeUsuario.ToString();
            }
            usuarioTeste.NomeUsuario = usuarioTesteNomeUsuario;

            var okResult = _usuariosController.EditarPerfilSalvar(usuarioTeste);
            string output = JsonConvert.SerializeObject(okResult);
            var deserializedokResult = JsonConvert.DeserializeObject<JsonResult>(output);

            Assert.True(deserializedokResult.Data.Equals("nomeUsuario"));
        }

        [Fact]
        public void EditarUsuario_PassandoDados_ResultadoEmailJahExiste()
        {
            Usuario usuarioTeste = null;
            var usuarioTesteEmail = "";
            using (var contexto = new ContatosVirtualContext())
            {
                usuarioTeste = contexto.Usuarios.FirstOrDefault(u => u.Permissao == EnumPermissaoUsuario.PermissaoAdministrador.ToString());
                usuarioTesteEmail = contexto.Usuarios.FirstOrDefault(u => u.Permissao == EnumPermissaoUsuario.PermissaoUsuario.ToString()).Email.ToString();
            }
            usuarioTeste.Email = usuarioTesteEmail;

            var okResult = _usuariosController.EditarPerfilSalvar(usuarioTeste);
            string output = JsonConvert.SerializeObject(okResult);
            var deserializedokResult = JsonConvert.DeserializeObject<JsonResult>(output);

            Assert.True(deserializedokResult.Data.Equals("emailUsuario"));
        }

        [Fact]
        public void EditarUsuario_PassandoDados_ResultadoSenhaInvalida()
        {
            Usuario usuarioTeste = null;
            using (var contexto = new ContatosVirtualContext())
            {
                usuarioTeste = contexto.Usuarios.FirstOrDefault(u => u.Permissao == EnumPermissaoUsuario.PermissaoAdministrador.ToString());
            }
            usuarioTeste.Senha = "123";

            var okResult = _usuariosController.EditarPerfilSalvar(usuarioTeste);
            string output = JsonConvert.SerializeObject(okResult);
            var deserializedokResult = JsonConvert.DeserializeObject<JsonResult>(output);

            Assert.True(deserializedokResult.Data.Equals("senhaCara"));
        }

        [Fact]
        public void EditarSalvarNovaSenha_PassandoDados_ResultadoSenhaInvalida()
        {
            var usuarioTeste = 0;
            using (var contexto = new ContatosVirtualContext())
            {
                usuarioTeste = contexto.Usuarios.FirstOrDefault(u => u.Permissao == EnumPermissaoUsuario.PermissaoAdministrador.ToString()).Id;
            }
            var senha = "1234";

            var okResult = _usuariosController.EditarSalvarNovaSenha(usuarioTeste, senha);
            string output = JsonConvert.SerializeObject(okResult);
            var deserializedokResult = JsonConvert.DeserializeObject<JsonResult>(output);

            Assert.True(deserializedokResult.Data.Equals("/Home"));
        }
    }
}