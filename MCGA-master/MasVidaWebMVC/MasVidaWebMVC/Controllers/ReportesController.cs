using MasVidaWebMVC.Common;
using MasVidaWebMVC.Filters;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace MasVidaWebMVC.Controllers
{
    public class ReportesController : Controller
    {

        private MasVidaDataContext db = new MasVidaDataContext();

        //
        // GET: /Reportes/
        [ResourceAuthorize]
        public ActionResult Index()
        {
            return View();
        }

        [ResourceAuthorize]
        public ActionResult ReporteDeClientes()
        {
            var users = db.Users.Include(u => u.FamiliesGroup).Include(u => u.Product).Include(u => u.UserType).Where(u => u.UserTypeID == (int)AppConstants.UserType.CLIENT).Where(u => u.IsActive == true).OrderBy(u => u.LastName).OrderBy(u => u.Name);

            return View(users.ToList());
        }
        [ResourceAuthorize]
        public ActionResult ImprimirReporteDeClientes()
        {
            var users = db.Users.Include(u => u.FamiliesGroup).Include(u => u.Product).Include(u => u.UserType).Where(u => u.UserTypeID == (int)AppConstants.UserType.CLIENT).Where(u => u.IsActive == true).OrderBy(u => u.LastName).OrderBy(u => u.Name);

            return View(users.ToList());
        }


    }
}
