using System;
using System.Web.Mvc;

namespace ContatosVirtual.Controllers
{
    public class ErrosController : Controller
    {
        public ActionResult Http404(Exception exception)
        {
            Response.StatusCode = 404;
            Response.ContentType = "text/html";
            return View(exception);
        }

        public ActionResult Http500(Exception exception)
        {
            Response.StatusCode = 500;
            Response.ContentType = "text/html";
            return View(exception);
        }
    }
}