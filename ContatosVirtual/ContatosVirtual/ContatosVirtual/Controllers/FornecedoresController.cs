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
    [AutorizacaoFilter]
    public class FornecedoresController : Controller
    {
        private readonly IFornecedor _fornecedores;
        private readonly FornecedorServicos _fornecedorServicos;
        private readonly IEmpresa _empresas;
        private readonly IEstado _estados;

        public FornecedoresController(IFornecedor fornecedores, FornecedorServicos fornecedorServicos, 
                                      IEmpresa empresas, IEstado estados)
        {
            _fornecedores = fornecedores;
            _fornecedorServicos = fornecedorServicos;
            _empresas = empresas;
            _estados = estados;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Form()
        {
            ViewBag.Empresas = _empresas.ListaEmpresasAtivada();
            ViewBag.Fornecedor = new Fornecedor();
            ViewBag.Telefone = new Telefone();
            return View();
        }

        public ActionResult Editar(int id)
        {
            var fornecedor = _fornecedores.BuscaPorId(id);
            ViewBag.Empresas = _empresas.ListaEmpresasAtivadaMaisEmoresaFornecedor(fornecedor.EmpresaId);
            ViewBag.Fornecedor = fornecedor;
            return View();
        }

        public ActionResult Ativar(int id)
        {
            return View();
        }

        public ActionResult Desativar(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Adiciona(Fornecedor fornecedor)
        {
            try
            {
                if (_fornecedorServicos.VerificarFornecedorPFMenorIdadeEmpresaParane(fornecedor))
                    return Json("EhFornecedorPFMenorIdadeEmpresaParana");

                fornecedor.DataHoraCadastro = DateTime.Now;
                fornecedor.Status = EnumStatusFornecedor.StatusAtivado.ToString();
                _fornecedores.Adicionar(fornecedor);
            }
            catch (Exception)
            {
                return Json("erro");
            }

            return Json("/Fornecedores");
        }

        [HttpPost]
        public ActionResult EditarSalvar(Fornecedor fornecedor)
        {
            try
            {
                if (_fornecedorServicos.VerificarFornecedorPFMenorIdadeEmpresaParane(fornecedor))
                    return Json("EhFornecedorPFMenorIdadeEmpresaParana");

                fornecedor.DataHoraEdicao = DateTime.Now;
                _fornecedores.Editar(fornecedor);
            }
            catch (Exception)
            {
                return Json("erro");
            }

            return Json(new { fornecedor = fornecedor });
        }

        [HttpPost]
        public ActionResult AtivarConcluido(Fornecedor fornecedor)
        {
            try
            {
                var fornecedorBuscado = _fornecedores.BuscaPorId(fornecedor.Id);
                fornecedorBuscado.Status = EnumStatusFornecedor.StatusAtivado.ToString();
                fornecedorBuscado.UsuarioEdicaoId = fornecedor.UsuarioEdicaoId;
                fornecedorBuscado.DataHoraEdicao = DateTime.Now;
                _fornecedores.AlterarStatus(fornecedorBuscado);
            }
            catch (Exception)
            {
                return Json("erro");
            }

            return Json("/Fornecedores");
        }

        [HttpPost]
        public ActionResult DesativarConcluido(Fornecedor fornecedor)
        {
            try
            {
                var fornecedorBuscado = _fornecedores.BuscaPorId(fornecedor.Id);
                fornecedorBuscado.Status = EnumStatusFornecedor.StatusDesativado.ToString();
                fornecedorBuscado.UsuarioEdicaoId = fornecedor.UsuarioEdicaoId;
                fornecedorBuscado.DataHoraEdicao = DateTime.Now;
                _fornecedores.AlterarStatus(fornecedorBuscado);
            }
            catch (Exception)
            {
                return Json("erro");
            }

            return Json("/Fornecedores");
        }
        
        [HttpPost]
        public ActionResult VerificarPorCpfCnpjJahCadastrado(int id, string cpfCnpj)
        {
            bool ehIgualRegistroAtual = id == 0 ? false : _fornecedores.BuscaPorId(id).CpfCnpj.ToString().Equals(cpfCnpj);

            if (!ehIgualRegistroAtual)
            {
                if (_fornecedores.BuscaPorCpfCnpj(cpfCnpj) != null)
                {
                    return Json("error");
                }
            }

            return Json("");
        }

        [HttpPost]
        public ActionResult BuscarFornecedorPorCpfCnpj(string cpfCnpj)
        {
            try
            {
                return Json(_fornecedores.BuscaPorCpfCnpj(cpfCnpj));
            }
            catch
            {
                return Json("error");
            }
        }

        [HttpPost]
        public ActionResult Filtro(string status, string pesquisa, int start = 0, int length = 0, int draw = 0)
        {
            IList<Fornecedor> fornecedores = _fornecedores.ListaComFiltro(status, pesquisa, start, length);
            int quantidadeFiltrada = _fornecedores.RetornarQuantidadeRegistroComFiltro(status, pesquisa);
            int quantidadeTotal = _fornecedores.QuantidadeTotalRegistros();
            return Json(new
            {
                draw = draw,
                recordsTotal = quantidadeTotal,
                recordsFiltered = quantidadeFiltrada,
                data = fornecedores
            });
        }
    }
}