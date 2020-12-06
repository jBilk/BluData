using ContatosVirtual.Content.Enum;
using ContatosVirtual.Controllers;
using ContatosVirtual.Enum;
using ContatosVirtual.Interfaces;
using ContatosVirtual.Models;
using ContatosVirtualTestes.TestesEmpresas;
using ContatosVirtualTestes.TestesEstados;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Web.Mvc;
using Xunit;

namespace ContatosVirtualTestes.TestesLogins
{
    public class FornecedoresControllersTestes
    {
        private readonly FornecedoresController _fornecedoresController;
        private readonly EmpresasController _empresasController;
        private readonly EstadosController _estadosController;
        private readonly IFornecedores _fornecedores;
        private readonly IEmpresas _empresas;
        private readonly IEstados _estados;

        public FornecedoresControllersTestes()
        {
            _fornecedores = new FornecedoresFake();
            _estados = new EstadosFake();
            _empresas = new EmpresasFake();
            _estadosController = new EstadosController(_estados);
            _empresasController = new EmpresasController(_empresas, _estados);
            _fornecedoresController = new FornecedoresController(_fornecedores, _empresas, _estados);
        }

        [Fact]
        public void AdicionarFornecedor_PassandoDados_ResultadoOk()
        {
            var empresaTeste = 0;
            using (var contexto = new ContatosVirtualContext())
            {
                empresaTeste = contexto.Empresas.FirstOrDefault().Id;
            }
            var fornecedorTeste = new Fornecedor();
            fornecedorTeste.EmpresaId = empresaTeste;
            fornecedorTeste.Nome = "Fornecedor Teste Unitário";
            fornecedorTeste.CpfCnpj = "87.651.337/0001-47";
            fornecedorTeste.PessoaJuridicaFisica = EnumTipoPessoa.PessoaJuridica.ToString();
            fornecedorTeste.UsuarioCadastroId = 1;

            var okResult = _fornecedoresController.Adiciona(fornecedorTeste);
            string output = JsonConvert.SerializeObject(okResult);
            var deserializedokResult = JsonConvert.DeserializeObject<JsonResult>(output);

            Assert.True(deserializedokResult.Data.Equals("/Fornecedores"));
        }

        [Fact]
        public void AdicionarFornecedor_PassandoDadosEmpresaDoPRPFMenorDeIdade_ResultadoEhMenorDeIdade()
        {
            var idEmpresaTeste = 0;
            using (var context = new ContatosVirtualContext())
            {
                var idEstado = context.Estados.FirstOrDefault(u => u.NomeEstado == EnumEstados.EstadoParana.ToString()).Id;
                idEmpresaTeste = context.Empresas.FirstOrDefault(u => u.EstadoId == idEstado).Id;
            }

            var fornecedorTeste = new Fornecedor();
            fornecedorTeste.EmpresaId = idEmpresaTeste;
            fornecedorTeste.Nome = "Fornecedor Teste Unitário";
            fornecedorTeste.CpfCnpj = "999.999.999-99";
            fornecedorTeste.PessoaJuridicaFisica = EnumTipoPessoa.PessoaFisica.ToString();
            fornecedorTeste.UsuarioCadastroId = 1;
            var dataNascimento = new DateTime(DateTime.Now.Year - 17, DateTime.Now.Month, DateTime.Now.Day);
            fornecedorTeste.DataNascimento = dataNascimento;

            var okResult = _fornecedoresController.Adiciona(fornecedorTeste);
            string output = JsonConvert.SerializeObject(okResult);
            var deserializedokResult = JsonConvert.DeserializeObject<JsonResult>(output);

            Assert.True(deserializedokResult.Data.Equals("EhFornecedorPFMenorIdadeEmpresaParana"));
        }

        [Fact]
        public void AlterarStatusFornecedorParaAtivado_PassandoDados_ResultadoOk()
        {
            Fornecedor fornecedorTeste = null;
            using (var contexto = new ContatosVirtualContext())
            {
                fornecedorTeste = contexto.Fornecedores.FirstOrDefault(u => u.Status == EnumStatusFornecedor.StatusDesativado.ToString());
            }
            fornecedorTeste.UsuarioEdicaoId = 1;

            var okResult = _fornecedoresController.AtivarConcluido(fornecedorTeste);
            string output = JsonConvert.SerializeObject(okResult);
            var deserializedokResult = JsonConvert.DeserializeObject<JsonResult>(output);

            Assert.True(deserializedokResult.Data.Equals("/Fornecedores"));
        }

        [Fact]
        public void AlterarStatusFornecedorParaDesativado_PassandoDados_ResultadoOk()
        {
            Fornecedor fornecedorTeste = null;
            using (var contexto = new ContatosVirtualContext())
            {
                fornecedorTeste = contexto.Fornecedores.FirstOrDefault(u => u.Status == EnumStatusFornecedor.StatusAtivado.ToString());
            }
            fornecedorTeste.UsuarioEdicaoId = 1;

            var okResult = _fornecedoresController.DesativarConcluido(fornecedorTeste);
            string output = JsonConvert.SerializeObject(okResult);
            var deserializedokResult = JsonConvert.DeserializeObject<JsonResult>(output);

            Assert.True(deserializedokResult.Data.Equals("/Fornecedores"));
        }

        [Fact]
        public void EditarFornecedor_PassandoDados_ResultadoOk()
        {
            Fornecedor fornecedorTeste = null;
            using (var contexto = new ContatosVirtualContext())
            {
                fornecedorTeste = contexto.Fornecedores.FirstOrDefault(u => u.CpfCnpj != "");
            }
            fornecedorTeste.Rg = "9999999";

            var okResult = _fornecedoresController.EditarSalvar(fornecedorTeste);
            string output = JsonConvert.SerializeObject(okResult);
            var deserializedokResult = JsonConvert.DeserializeObject<JsonResult>(output);

            Assert.True(!deserializedokResult.Data.Equals("erro"));
        }
    }
}
