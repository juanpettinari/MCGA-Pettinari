using System.Web.Mvc;

namespace MasVidaWebMVC.Controllers
{
    public class ErrorController : Controller
    {
        //
        // GET: /Error/
        public ActionResult Unauthorized()
        {
            return View();
        }

    }
}
