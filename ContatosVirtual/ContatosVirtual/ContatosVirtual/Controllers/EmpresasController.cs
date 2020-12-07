using ContatosVirtual.Content.Enum;
using ContatosVirtual.Filtros;
using ContatosVirtual.Interfaces;
using ContatosVirtual.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ContatosVirtual.Controllers
{
    [AutorizacaoFilter]
    public class EmpresasController : Controller
    {
        private readonly IEmpresa _empresas;
        private readonly IEstado _estados;

        public EmpresasController(IEmpresa empresas, IEstado estados)
        {
            _empresas = empresas;
            _estados = estados;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Form()
        {
            ViewBag.Estados = _estados.Lista();
            ViewBag.Empresa = new Empresa();
            return View();
        }

        public ActionResult Editar(int id)
        {
            ViewBag.Estados = _estados.Lista();
            ViewBag.Empresa = _empresas.BuscaPorId(id);
            return View();
        }

        public ActionResult Visualizar(int id)
        {
            ViewBag.Estados = _estados.Lista();
            ViewBag.Empresa = _empresas.BuscaPorId(id);
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
        public ActionResult Adiciona(Empresa empresa)
        {
            try
            {
                empresa.EstadoId = _estados.BuscarPorSigla(empresa.EstadoSigla.ToString());
                empresa.Status = EnumStatusEmpresa.StatusAtivada.ToString();
                empresa.DataHoraCadastro = DateTime.Now;
                _empresas.Adicionar(empresa);
            }
            catch (Exception)
            {
                return Json("erro");
            }
            return Json("/Empresas");
        }

        [HttpPost]
        public ActionResult EditarSalvar(Empresa empresa)
        {
            try
            {
                empresa.EstadoId = _estados.BuscarPorSigla(empresa.EstadoSigla.ToString());
                empresa.DataHoraEdicao = DateTime.Now;
                _empresas.Editar(empresa);
            }
            catch (Exception)
            {
                return Json("erro");
            }
            return Json(new { empresa = empresa });
        }

        [HttpPost]
        public ActionResult AtivarConcluido(Empresa empresa)
        {
            try
            {
                var empresaBuscada = _empresas.BuscaPorId(empresa.Id);
                empresaBuscada.Status = EnumStatusEmpresa.StatusAtivada.ToString();
                empresaBuscada.DataHoraEdicao = DateTime.Now;
                empresaBuscada.UsuarioEdicaoId = empresa.UsuarioEdicaoId;
                _empresas.AlterarStatus(empresaBuscada);
            }
            catch (Exception)
            {
                return Json("erro");
            }
            return Json("/Empresas");
        }

        [HttpPost]
        public ActionResult DesativarConcluido(Empresa empresa)
        {
            try
            {
                var empresaBuscada = _empresas.BuscaPorId(empresa.Id);
                empresaBuscada.Status = EnumStatusEmpresa.StatusDesativada.ToString();
                empresaBuscada.DataHoraEdicao = DateTime.Now;
                empresaBuscada.UsuarioEdicaoId = empresa.UsuarioEdicaoId;
                _empresas.AlterarStatus(empresaBuscada);
            }
            catch (Exception)
            {
                return Json("erro");
            }
            return Json("/Empresas");
        }

        [HttpPost]
        public ActionResult BuscaPorCnpj(int id, string cnpj)
        {
            bool ehIgualRegistroAtual = id == 0 ? false : _empresas.BuscaPorId(id).Cnpj.ToString().Equals(cnpj);

            if (!ehIgualRegistroAtual)
            {
                if (_empresas.BuscaPorCnpj(cnpj) != null)
                {
                    return Json("error");
                }
            }
            return Json("");
        }

        [HttpPost]
        public ActionResult Filtro(string status, string pesquisa, int start = 0, int length = 0, int draw = 0)
        {
            IList<Empresa> empresas = _empresas.ListaComFiltro(status, pesquisa, start, length);
            int quantidadeFiltrada = _empresas.RetornarQuantidadeRegistroComFiltro(status, pesquisa);
            int quantidadeTotal = _empresas.QuantidadeTotalRegistros();
            return Json(new
            {
                draw = draw,
                recordsTotal = quantidadeTotal,
                recordsFiltered = quantidadeFiltrada,
                data = empresas
            });
        }
    }
}