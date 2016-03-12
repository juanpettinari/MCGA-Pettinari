using MasVidaWebMVC.Filters;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace MasVidaWebMVC.Controllers
{
    public class ProductosController : Controller
    {
        private MasVidaDataContext db = new MasVidaDataContext();

        //
        // GET: /Productos/
        [ResourceAuthorize]
        public ActionResult Index()
        {
            return View(db.Products.ToList());
        }

        //
        // GET: /Productos/Details/5
        [ResourceAuthorize]
        public ActionResult Details(int id = 0)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        //
        // GET: /Productos/Create
        [ResourceAuthorize]
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Productos/Create
        [ResourceAuthorize]
        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        //
        // GET: /Productos/Edit/5
        [ResourceAuthorize]
        public ActionResult Edit(int id = 0)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        //
        // POST: /Productos/Edit/5
        [ResourceAuthorize]
        [HttpPost]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        //
        // GET: /Productos/Delete/5
        [ResourceAuthorize]
        public ActionResult Delete(int id = 0)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        //
        // POST: /Productos/Delete/5
        [ResourceAuthorize]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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