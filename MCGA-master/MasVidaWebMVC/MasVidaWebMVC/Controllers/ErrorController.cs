using MasVidaWebMVC.Filters;
using System.Web.Mvc;

namespace MasVidaWebMVC.Controllers
{
    public class ErrorController : Controller
    {
        //
        // GET: /Error/
        [ResourceAuthorize]
        public ActionResult Unauthorized()
        {
            return View();
        }

    }
}
