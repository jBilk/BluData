using ContatosVirtual.Filtros;
using ContatosVirtual.Interfaces;
using ContatosVirtual.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ContatosVirtual.Controllers
{
    [AutorizacaoFilter]
    public class TelefonesController : Controller
    {
        private readonly ITelefones _telefones;

        public TelefonesController(ITelefones telefones)
        {
            _telefones = telefones;
        }

        [HttpPost]
        public ActionResult Adiciona(Telefone telefone)
        {
            try
            {
                if (telefone.Numero == null || telefone.Numero.Length < 13)
                    return Json("numeroVazio");

                if (_telefones.VerificarNumeroSeJahExiste(telefone.Numero.ToString()) != null)
                    return Json("numeroJahExiste");

                _telefones.Adiciona(telefone);
            }
            catch (Exception)
            {
                return Json("erro");
            }

            return Json("/Telefones");
        }

        public ActionResult ListaTelefonesPorFornecedorComFiltro(string fornecedorId, string pesquisa, int start = 0, int length = 0, int draw = 0)
        {
            IList<Telefone> telefones = _telefones.ListaComFiltro(fornecedorId, pesquisa, start, length);
            int quantidadeFiltrada = _telefones.RetornarQuantidadeRegistroComFiltro(fornecedorId, pesquisa);
            int quantidadeTotal = _telefones.QuantidadeTotalRegistros(fornecedorId);
            return Json(new
            {
                draw = draw,
                recordsTotal = quantidadeTotal,
                recordsFiltered = quantidadeFiltrada,
                data = telefones
            });
        }

        [HttpPost]
        public ActionResult BuscarTelefonePorIdFornecedor(int idFornecedor)
        {
            try
            {
                return Json(_telefones.BuscarPorIdFornecedor(idFornecedor).FirstOrDefault());
            }
            catch
            {
                return Json("error");
            }
        }

        public ActionResult Excluir(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult ExcluirConcluido(int id)
        {
            try
            {
                var telefone = _telefones.BuscaPorId(id);
                _telefones.Excluir(telefone);
            }
            catch (Exception)
            {
                return Json("erro");
            }
            return Json("");
        }
    }
}