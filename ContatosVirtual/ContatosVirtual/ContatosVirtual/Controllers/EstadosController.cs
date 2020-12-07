using ContatosVirtual.Filtros;
using ContatosVirtual.Interfaces;
using System.Web.Mvc;

namespace ContatosVirtual.Controllers
{
    [AutorizacaoFilter]
    public class EstadosController : Controller
    {
        private readonly IEstado _estados;

        public EstadosController(IEstado estados)
        {
            _estados = estados;
        }

        [HttpPost]
        public ActionResult BuscaPorEstadoPorId(int id)
        {
            if (_estados.BuscaPorId(id) == null)
            {
                return Json("error");
            }

            return Json("");
        }
    }
}