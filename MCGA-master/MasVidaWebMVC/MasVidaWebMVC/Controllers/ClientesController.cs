﻿using MasVidaWebMVC.Common;
using MasVidaWebMVC.Filters;
using PagedList;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;


namespace MasVidaWebMVC.Controllers
{
    public class ClientesController : Controller
    {
        private MasVidaDataContext db = new MasVidaDataContext();



        //
        // GET: /Clintes/
        [ResourceAuthorize]
        public ActionResult Index(string searchString, string currentFilter, int? page)
        {
            //autenticar... si es false que salga, sino que continue
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            int uti = (from ut in db.UsersTypes
                               where ut.UserTypeName == "Cliente"
                               select ut.UserTypeID).SingleOrDefault();

            var users = from u in db.Users.Include(u => u.FamiliesGroup).Include(u => u.Product).Include(u => u.UserType)
                                    .Where(u => u.UserTypeID == uti)
                        select u;

            if (!String.IsNullOrEmpty(searchString))
            {
                users = users.Where(u => u.LastName.ToUpper().Contains(searchString.ToUpper())
                               || u.Name.ToUpper().Contains(searchString.ToUpper()));
            }

            users = users.OrderBy(u => u.LastName).OrderBy(u => u.Name);


            int pageSize = 30;
            int pageNumber = (page ?? 1);
            return View(users.ToPagedList(pageNumber, pageSize));

            // return View(users.ToList());
        }

        //
        // GET: /Clientes/Details/5
        [ResourceAuthorize]
        public ActionResult Details(int id = 0)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        //
        // GET: /Clientes/Create
        [ResourceAuthorize]
        public ActionResult Create()
        {
            ViewBag.FamilyGroupID = new SelectList(db.FamiliesGroups, "FamilyGroupID", "FamilyName");
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductName");
            ViewBag.UserTypeID = new SelectList(db.UsersTypes, "UserTypeID", "UserTypeName");
            return View();
        }

        //
        // POST: /Clientes/Create
        [ResourceAuthorize]
        [HttpPost]
        public ActionResult Create(User user)
        {
            user.UserTypeID = (from ut in db.UsersTypes
                               where ut.UserTypeName == "Cliente"
                               select ut.UserTypeID).SingleOrDefault();
            //user.UserTypeID = (int)AppConstants.UserType.CLIENT;
            user.CreationDateTime = DateTime.Now;
            user.AccountTotal = 0;

            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }


            //TODO logica de generar la factura dependiendo de la fecha


            ViewBag.FamilyGroupID = new SelectList(db.FamiliesGroups, "FamilyGroupID", "FamilyName", user.FamilyGroupID);
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductName", user.ProductID);
            ViewBag.UserTypeID = new SelectList(db.UsersTypes, "UserTypeID", "UserTypeName", user.UserTypeID);
            return View(user);
        }


        //GET: /Clientes/Edit/5

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


        //POST: /Clientes/Edit/5
        [ResourceAuthorize]
        [HttpPost]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FamilyGroupID = new SelectList(db.FamiliesGroups, "FamilyGroupID", "FamilyName", user.FamilyGroupID);
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductName", user.ProductID);
            ViewBag.UserTypeID = new SelectList(db.UsersTypes, "UserTypeID", "UserTypeName", user.UserTypeID);
            return View(user);
        }

        //
        // GET: /Clientes/Delete/5
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
        // POST: /Clientes/Delete/5
        [ResourceAuthorize]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);

            //Delete Transacctions
            foreach (Transaction trans in db.Transactions.Where(t => t.UserID == user.UserID))
            {
                db.Transactions.Remove(trans);
            }

            //Delete FamilyGroup ???

            //delete User
            db.Users.Remove(user);

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [ResourceAuthorize]
        public ActionResult Pagar(int id = 0, int userId = 0)
        {
            Transaction transaction = db.Transactions.Find(id);

            transaction.IsPaid = true;

            Transaction transForPay = new Transaction();
            transForPay.ProductName = transaction.ProductName;
            transForPay.ProductPrice = transaction.ProductPrice;
            transForPay.IsPaid = true;
            transForPay.TransactionTypeID = (int)AppConstants.TransactionType.RECIBO;
            transForPay.ProductID = Convert.ToInt32(transaction.ProductID);
            transForPay.UserID = transaction.Users.UserID;
            transForPay.TransactionCreationDate = DateTime.Now;

            db.Entry(transaction).State = EntityState.Modified;
            db.Transactions.Add(transForPay);

            db.SaveChanges();
            return RedirectToAction("Details", new { id = userId} );
        }
        [ResourceAuthorize]
        public ActionResult Imprimir(int id = 0)
        {
            Transaction transaction = db.Transactions.Find(id);

            //ver como usar el texto de las constantes dependiendo del nro
            ViewBag.Month = "ENERO";

            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }


        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}