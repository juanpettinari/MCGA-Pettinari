using MasVidaWebMVC.Filters;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace MasVidaWebMVC.Controllers
{
    public class RecursosController : Controller
    {
        private MasVidaDataContext db = new MasVidaDataContext();

        //
        // GET: /Recursos/
        [ResourceAuthorize]
        public ActionResult Index()
        {
            return View(db.Resources.ToList());
        }

        //
        // GET: /Recursos/Details/5
        [ResourceAuthorize]
        public ActionResult Details(int id = 0)
        {
            Resource resource = db.Resources.Find(id);
            if (resource == null)
            {
                return HttpNotFound();
            }
            return View(resource);
        }

        //
        // GET: /Recursos/Create
        [ResourceAuthorize]
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Recursos/Create
        [ResourceAuthorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Resource resource)
        {
            if (ModelState.IsValid)
            {
                
                string[] arrUT = {};

                arrUT = (from ut in db.UsersTypes select ut.UserTypeName).ToArray();
                for (int i = 0; i < arrUT.Length; i++)
                {
                    UserTypes_Resources UT_R = new UserTypes_Resources();
                    UT_R.Permit = false;
                    UT_R.ControllerID = resource.ControllerID;
                    var utn = arrUT[i];
                    UT_R.UserTypeID = (from ut in db.UsersTypes
                                       where ut.UserTypeName == utn
                                       select ut.UserTypeID).First();
                    db.UserTypes_Resources.Add(UT_R);
                }
                db.Resources.Add(resource);
                db.SaveChanges();
                AuditoriaController.AuditoriaAltaRecurso(User.Identity.Name, resource);
                return RedirectToAction("Index");
            }

            return View(resource);
        }

        //
        // GET: /Recursos/Edit/5
        [ResourceAuthorize]
        public ActionResult Edit(int id = 0)
        {
            Resource resource = db.Resources.Find(id);
            if (resource == null)
            {
                return HttpNotFound();
            }
            return View(resource);
        }

        //
        // POST: /Recursos/Edit/5
        [ResourceAuthorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Resource resource)
        {
            if (ModelState.IsValid)
            {
                db.Entry(resource).State = EntityState.Modified;
                db.SaveChanges();
                AuditoriaController.AuditoriaModificacionRecurso(User.Identity.Name, resource);
                return RedirectToAction("Index");
            }
            return View(resource);
        }

        //
        // GET: /Recursos/Delete/5
        [ResourceAuthorize]
        public ActionResult Delete(int id = 0)
        {
            Resource resource = db.Resources.Find(id);
            if (resource == null)
            {
                return HttpNotFound();
            }
            return View(resource);
        }

        //
        // POST: /Recursos/Delete/5
        [ResourceAuthorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Resource resource = db.Resources.Find(id);
            int[] arrUT = { };
            arrUT = (from ut in db.UsersTypes select ut.UserTypeID).ToArray();
            for (int i = 0; i < arrUT.Length; i++)
            {
                UserTypes_Resources UT_R = db.UserTypes_Resources.Find(arrUT[i], resource.ControllerID);
                db.UserTypes_Resources.Remove(UT_R);
            }
            db.Resources.Remove(resource);
            db.SaveChanges();
            AuditoriaController.AuditoriaBajaRecurso(User.Identity.Name, resource);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}