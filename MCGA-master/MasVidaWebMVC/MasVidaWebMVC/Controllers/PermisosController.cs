using MasVidaWebMVC.Filters;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace MasVidaWebMVC.Controllers
{
    public class PermisosController : Controller
    {
        private MasVidaDataContext db = new MasVidaDataContext();

        //
        // GET: /Permisos/
        [ResourceAuthorize]
        public ActionResult Index()
        {
            var usertypes_resources = db.UserTypes_Resources.Include(u => u.Resource).Include(u => u.UsersType);

            return View(usertypes_resources.ToList());
        }

        //
        // GET: /Permisos/Details/5

        //public ActionResult Details(int id = 0)
        //{
        //    UserTypes_Resources usertypes_resources = db.UserTypes_Resources.Find(id);
        //    if (usertypes_resources == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(usertypes_resources);
        //}

        //
        // GET: /Permisos/Create

        //public ActionResult Create()
        //{
        //    ViewBag.ControllerID = new SelectList(db.Resources, "ControllerID", "ControllerName");
        //    ViewBag.UserTypeID = new SelectList(db.UsersTypes, "UserTypeID", "UserTypeName");
        //    return View();
        //}

        ////
        //// POST: /Permisos/Create

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(UserTypes_Resources usertypes_resources)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.UserTypes_Resources.Add(usertypes_resources);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.ControllerID = new SelectList(db.Resources, "ControllerID", "ControllerName", usertypes_resources.ControllerID);
        //    ViewBag.UserTypeID = new SelectList(db.UsersTypes, "UserTypeID", "UserTypeName", usertypes_resources.UserTypeID);
        //    return View(usertypes_resources);
        //}
        [ResourceAuthorize]
        public ActionResult CambiarPermiso(int UTID, int RID)
        {
            UserTypes_Resources usertypes_resources = db.UserTypes_Resources.Find(UTID, RID);
            if (usertypes_resources.Permit == false)
            {
                usertypes_resources.Permit = true;
            }
            else
            {
                usertypes_resources.Permit = false;
            }
            db.Entry(usertypes_resources).State = EntityState.Modified;
            db.SaveChanges();
            AuditoriaController.AuditoriaModificarPermiso(User.Identity.Name, usertypes_resources);
            return RedirectToAction("Index");
        }

        //

        //
        // GET: /Permisos/Delete/5

        //public ActionResult Delete(int id = 0)
        //{
        //    UserTypes_Resources usertypes_resources = db.UserTypes_Resources.Find(id);
        //    if (usertypes_resources == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(usertypes_resources);
        //}

        ////
        //// POST: /Permisos/Delete/5

        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    UserTypes_Resources usertypes_resources = db.UserTypes_Resources.Find(id);
        //    db.UserTypes_Resources.Remove(usertypes_resources);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}