using ContatosVirtual.Models;
using System.Web.Mvc;
using System.Web.Routing;

namespace ContatosVirtual.Filtros
{
    public class AutorizacaoFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Usuario usuarioLogado = (Usuario)filterContext.HttpContext.Session["usuarioLogado"];
            if (usuarioLogado == null)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(
                    new { controller = "Login", Action = "Index" }
                    )
                );
            }
        }
    }
}