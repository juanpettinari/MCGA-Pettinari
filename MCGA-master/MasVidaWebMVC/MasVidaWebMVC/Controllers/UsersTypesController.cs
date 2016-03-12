using MasVidaWebMVC.Filters;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace MasVidaWebMVC.Controllers
{
    public class UsersTypesController : Controller
    {
        private MasVidaDataContext db = new MasVidaDataContext();

        //
        // GET: /UsersTypes/
        [ResourceAuthorize]
        public ActionResult Index()
        {
            return View(db.UsersTypes.ToList());
        }

        //
        // GET: /UsersTypes/Details/5
        [ResourceAuthorize]
        public ActionResult Details(int id = 0)
        {
            UserType usertype = db.UsersTypes.Find(id);
            if (usertype == null)
            {
                return HttpNotFound();
            }
            return View(usertype);
        }

        //
        // GET: /UsersTypes/Create
        [ResourceAuthorize]
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /UsersTypes/Create
        [ResourceAuthorize]
        [HttpPost]
        public ActionResult Create(UserType usertype)
        {
            if (ModelState.IsValid)
            {
                string[] arrR = { };
                arrR = (from r in db.Resources select r.ControllerName).ToArray();
                for (int i = 0; i < arrR.Length; i++)
                {
                    UserTypes_Resources UT_R = new UserTypes_Resources();
                    UT_R.Permit = false;
                    UT_R.UserTypeID = usertype.UserTypeID;
                    var rcn = arrR[i];
                    UT_R.ControllerID = (from r in db.Resources
                                       where r.ControllerName == rcn
                                       select r.ControllerID).First();
                    db.UserTypes_Resources.Add(UT_R);
                }
                db.UsersTypes.Add(usertype);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(usertype);
        }

        //
        // GET: /UsersTypes/Edit/5
        [ResourceAuthorize]
        public ActionResult Edit(int id = 0)
        {
            UserType usertype = db.UsersTypes.Find(id);
            if (usertype == null)
            {
                return HttpNotFound();
            }
            return View(usertype);
        }

        //
        // POST: /UsersTypes/Edit/5
        [ResourceAuthorize]
        [HttpPost]
        public ActionResult Edit(UserType usertype)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usertype).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(usertype);
        }

        //
        // GET: /UsersTypes/Delete/5
        [ResourceAuthorize]
        public ActionResult Delete(int id = 0)
        {
            UserType usertype = db.UsersTypes.Find(id);
            if (usertype == null)
            {
                return HttpNotFound();
            }
            return View(usertype);
        }

        //
        // POST: /UsersTypes/Delete/5
        [ResourceAuthorize]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            UserType usertype = db.UsersTypes.Find(id);
            int[] arrR = { };
            arrR = (from r in db.Resources select r.ControllerID).ToArray();
            for (int i = 0; i < arrR.Length; i++)
            {
                UserTypes_Resources UT_R = db.UserTypes_Resources.Find(usertype.UserTypeID, arrR[i]);
                db.UserTypes_Resources.Remove(UT_R);
            }
            db.UsersTypes.Remove(usertype);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}