using MasVidaWebMVC.Common;
using MasVidaWebMVC.Filters;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace MasVidaWebMVC.Controllers
{
    public class UsersController : Controller
    {
        private MasVidaDataContext db = new MasVidaDataContext();





        //
        // GET: /Users/
        [ResourceAuthorize]
        public ActionResult Index()
        {
            var users = db.Users.Include(u => u.FamiliesGroup).Include(u => u.Product).Include(u => u.UserType);
            return View(users.ToList());
        }
        [ResourceAuthorize]
        public ActionResult ImportClients()
        {
            var users = db.Users.Include(u => u.FamiliesGroup).Include(u => u.Product).Include(u => u.UserType);
            return View(users.ToList());
        }


        //
        // GET: /Users/Details/5
        [ResourceAuthorize]
        public ActionResult Details(int id = 0)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View("Details",user);
        }

        //
        // GET: /Users/Create
        [ResourceAuthorize]
        public ActionResult Create()
        {
            ViewBag.FamilyGroupID = new SelectList(db.FamiliesGroups, "FamilyGroupID", "FamilyName");
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductName");
            ViewBag.UserTypeID = new SelectList(db.UsersTypes, "UserTypeID", "UserTypeName");
            return View();
        }

        //
        // POST: /Users/Create
        [ResourceAuthorize]
        [HttpPost]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                if (user.UserPassword != null)
                { user.UserPassword = PasswordHash.CreateHash(user.UserPassword); }
                
                db.Users.Add(user);
                db.SaveChanges();
                AuditoriaController.AuditoriaAltaUsuario(User.Identity.Name, user);
                return RedirectToAction("Index");
            }

            ViewBag.FamilyGroupID = new SelectList(db.FamiliesGroups, "FamilyGroupID", "FamilyName", user.FamilyGroupID);
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductName", user.ProductID);
            ViewBag.UserTypeID = new SelectList(db.UsersTypes, "UserTypeID", "UserTypeName", user.UserTypeID);
            return View(user);
        }

        //
        // GET: /Users/Edit/5
        [ResourceAuthorize]
        public ActionResult Edit(int id = 0)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.FamilyGroupID = new SelectList(db.FamiliesGroups, "FamilyGroupID", "FamilyName", user.FamilyGroupID);
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductName", user.ProductID);
            ViewBag.UserTypeID = new SelectList(db.UsersTypes, "UserTypeID", "UserTypeName", user.UserTypeID);
            return View(user);
        }

        //
        // POST: /Users/Edit/5
        [ResourceAuthorize]
        [HttpPost]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                AuditoriaController.AuditoriaModificacionUsuario(User.Identity.Name, user);
                return RedirectToAction("Index");
            }
            ViewBag.FamilyGroupID = new SelectList(db.FamiliesGroups, "FamilyGroupID", "FamilyName", user.FamilyGroupID);
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductName", user.ProductID);
            ViewBag.UserTypeID = new SelectList(db.UsersTypes, "UserTypeID", "UserTypeName", user.UserTypeID);
            return View(user);
        }

        [ResourceAuthorize]
        public ActionResult CambiarPassword(int id = 0)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }
        [ResourceAuthorize]
        [HttpPost]
        public ActionResult CambiarPassword(User user)
        {
            if (ModelState.IsValid)
            {
                user.UserPassword = PasswordHash.CreateHash(user.UserPassword);
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                AuditoriaController.AuditoriaCambiarContraseñaUsuario(User.Identity.Name, user);
                return RedirectToAction("Index");
            }
            return View(user);
        }

        //
        // GET: /Users/Delete/5
        [ResourceAuthorize]
        public ActionResult Delete(int id = 0)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        //
        // POST: /Users/Delete/5
        [ResourceAuthorize]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            AuditoriaController.AuditoriaBajaUsuario(User.Identity.Name, user);
            return RedirectToAction("Index");
        }



      

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}