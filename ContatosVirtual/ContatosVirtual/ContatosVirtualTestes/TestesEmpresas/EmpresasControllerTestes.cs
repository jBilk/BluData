using ContatosVirtual.Content.Enum;
using ContatosVirtual.Controllers;
using ContatosVirtual.Interfaces;
using ContatosVirtual.Models;
using ContatosVirtualTestes.TestesEstados;
using Newtonsoft.Json;
using System.Linq;
using System.Web.Mvc;
using Xunit;

namespace ContatosVirtualTestes.TestesEmpresas
{
    public class EmpresasControllerTestes
    {
        private readonly EmpresasController _empresasController;
        private readonly EstadosController _estadosController;
        private readonly IEmpresa _empresas;
        private readonly IEstado _estados;

        public EmpresasControllerTestes()
        {
            _empresas = new EmpresasFake();
            _estados = new EstadosFake();
            _estadosController = new EstadosController(_estados);
            _empresasController = new EmpresasController(_empresas, _estados);
        }

        [Fact]
        public void AdicionarEmpresa_PassandoDados_ResultadoOk()
        {
            var empresaTeste = new Empresa();
            empresaTeste.NomeFantasia = "Empresa Teste Unitário";
            empresaTeste.Cnpj = "87.651.337/0001-47";
            empresaTeste.EstadoSigla = "PR";
            empresaTeste.UsuarioCadastroId = 1;

            var okResult = _empresasController.Adiciona(empresaTeste);
            string output = JsonConvert.SerializeObject(okResult);
            var deserializedokResult = JsonConvert.DeserializeObject<JsonResult>(output);

            Assert.True(deserializedokResult.Data.Equals("/Empresas"));
        }

        [Fact]
        public void AlterarStatusEmpresaParaAtivado_PassandoDados_ResultadoOk()
        {
            Empresa empresaTeste = null;
            using (var contexto = new ContatosVirtualContext())
            {
                empresaTeste = contexto.Empresas.FirstOrDefault(u => u.Status == EnumStatusEmpresa.StatusDesativada.ToString());
            }
            empresaTeste.UsuarioEdicaoId = 1;

            var okResult = _empresasController.AtivarConcluido(empresaTeste);
            string output = JsonConvert.SerializeObject(okResult);
            var deserializedokResult = JsonConvert.DeserializeObject<JsonResult>(output);

            Assert.True(deserializedokResult.Data.Equals("/Empresas"));
        }

        [Fact]
        public void AlterarStatusEmpresaParaDesativada_PassandoDados_ResultadoOk()
        {
            Empresa empresaTeste = null;
            using (var contexto = new ContatosVirtualContext())
            {
                empresaTeste = contexto.Empresas.FirstOrDefault(u => u.Status == EnumStatusEmpresa.StatusAtivada.ToString());
            }
            empresaTeste.UsuarioEdicaoId = 1;

            var okResult = _empresasController.DesativarConcluido(empresaTeste);
            string output = JsonConvert.SerializeObject(okResult);
            var deserializedokResult = JsonConvert.DeserializeObject<JsonResult>(output);

            Assert.True(deserializedokResult.Data.Equals("/Empresas"));
        }

        [Fact]
        public void EditarEmpresa_PassandoDados_ResultadoOk()
        {
            Empresa empresaTeste = null;
            using (var contexto = new ContatosVirtualContext())
            {
                empresaTeste = contexto.Empresas.FirstOrDefault(u => u.Cnpj != "");
            }
            empresaTeste.Cep = "99999-999";

            var okResult = _empresasController.EditarSalvar(empresaTeste);
            string output = JsonConvert.SerializeObject(okResult);
            var deserializedokResult = JsonConvert.DeserializeObject<JsonResult>(output);

            Assert.True(!deserializedokResult.Data.Equals("erro"));
        }
    }
}
