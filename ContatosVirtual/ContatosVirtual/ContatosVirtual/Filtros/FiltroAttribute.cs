using ContatosVirtual.Content.Enum;
using ContatosVirtual.Models;
using System.Web.Mvc;
using System.Web.Routing;

namespace ContatosVirtual.Filtros
{
    public class AutorizacaoFiltroAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Usuario usuario = (Usuario)filterContext.HttpContext.Session["usuarioLogado"];
            if (usuario == null || !usuario.Permissao.Equals(EnumPermissaoUsuario.PermissaoAdministrador.ToString()))
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(
                    new { controller = "AcessoNegado", Action = "Index" }
                    )
                );
            }
        }
    }
}