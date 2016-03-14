using System.Web.Mvc;

namespace MasVidaWebMVC.Controllers
{
    public class ErrorController : Controller
    {
        //
        // GET: /Error/
        public ActionResult Unauthorized()
        {
            AuditoriaController.AuditoriaNoAutorizadoError(User.Identity.Name);
            return View();
        }

    }
}
