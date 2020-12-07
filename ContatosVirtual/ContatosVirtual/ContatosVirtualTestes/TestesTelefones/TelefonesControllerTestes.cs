using ContatosVirtual.Controllers;
using ContatosVirtual.Enum;
using ContatosVirtual.Interfaces;
using ContatosVirtual.Models;
using Newtonsoft.Json;
using System.Linq;
using System.Web.Mvc;
using Xunit;

namespace ContatosVirtualTestes.TestesTelefones
{
    public class TelefonesControllerTestes
    {
        private readonly TelefonesController _telefonesController;
        private readonly ITelefone _telefones;

        public TelefonesControllerTestes()
        {
            _telefones = new TelefonesFake();
            _telefonesController = new TelefonesController(_telefones);
        }

        [Fact]
        public void AdicionarTelefone_PassandoDados_ResultadoOk()
        {
            var fornecedorTeste = 0;
            using (var contexto = new ContatosVirtualContext())
            {
                fornecedorTeste = contexto.Fornecedores.FirstOrDefault(u => u.Status == EnumStatusFornecedor.StatusAtivado.ToString()).Id;
            }
            var telefoneTeste = new Telefone();
            telefoneTeste.Numero = "(99)99999-9999";
            telefoneTeste.FornecedorId = fornecedorTeste;


            var okResult = _telefonesController.Adiciona(telefoneTeste);
            string output = JsonConvert.SerializeObject(okResult);
            var deserializedokResult = JsonConvert.DeserializeObject<JsonResult>(output);

            Assert.True(deserializedokResult.Data.Equals("/Telefones"));
        }

        [Fact]
        public void AdicionarTelefone_PassandoDadosComNumeroVazio_ResultadoNuemroVazio()
        {
            var fornecedorTeste = 0;
            using (var contexto = new ContatosVirtualContext())
            {
                fornecedorTeste = contexto.Fornecedores.FirstOrDefault(u => u.Status == EnumStatusFornecedor.StatusAtivado.ToString()).Id;
            }
            var telefoneTeste = new Telefone();
            telefoneTeste.Numero = "";
            telefoneTeste.FornecedorId = fornecedorTeste;


            var okResult = _telefonesController.Adiciona(telefoneTeste);
            string output = JsonConvert.SerializeObject(okResult);
            var deserializedokResult = JsonConvert.DeserializeObject<JsonResult>(output);

            Assert.True(deserializedokResult.Data.Equals("numeroVazio"));
        }

        [Fact]
        public void AdicionarTelefone_PassandoDadosComNumeroJahExistente_ResultadoNuemroJahExiste()
        {
            var fornecedorTeste = 0;
            var telefoneJahExisteTeste = "";
            using (var contexto = new ContatosVirtualContext())
            {
                fornecedorTeste = contexto.Fornecedores.FirstOrDefault(u => u.Status == EnumStatusFornecedor.StatusAtivado.ToString()).Id;
                telefoneJahExisteTeste = contexto.Telefones.FirstOrDefault().Numero.ToString();
            }
            var telefoneTeste = new Telefone();
            telefoneTeste.Numero = telefoneJahExisteTeste;
            telefoneTeste.FornecedorId = fornecedorTeste;


            var okResult = _telefonesController.Adiciona(telefoneTeste);
            string output = JsonConvert.SerializeObject(okResult);
            var deserializedokResult = JsonConvert.DeserializeObject<JsonResult>(output);

            Assert.True(deserializedokResult.Data.Equals("numeroJahExiste"));
        }

        [Fact]
        public void ExcuindoTelefone_PassandoDados_ResultadoOk()
        {
            var telefoneParaExcluir = 0;
            using (var contexto = new ContatosVirtualContext())
            {
                telefoneParaExcluir = contexto.Telefones.FirstOrDefault().Id;
            }

            var okResult = _telefonesController.ExcluirConcluido(telefoneParaExcluir);
            string output = JsonConvert.SerializeObject(okResult);
            var deserializedokResult = JsonConvert.DeserializeObject<JsonResult>(output);

            Assert.True(deserializedokResult.Data.Equals(""));
        }
    }
}
