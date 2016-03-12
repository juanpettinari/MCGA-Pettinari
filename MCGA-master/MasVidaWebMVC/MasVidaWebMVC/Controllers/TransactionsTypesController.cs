﻿using MasVidaWebMVC.Filters;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace MasVidaWebMVC.Controllers
{
    public class TransactionsTypesController : Controller
    {
        private MasVidaDataContext db = new MasVidaDataContext();

        //
        // GET: /TransactionsTypes/
        [ResourceAuthorize]
        public ActionResult Index()
        {
            return View(db.TransactionsTypes.ToList());
        }

        //
        // GET: /TransactionsTypes/Details/5
        [ResourceAuthorize]
        public ActionResult Details(int id = 0)
        {
            TransactionType transactiontype = db.TransactionsTypes.Find(id);
            if (transactiontype == null)
            {
                return HttpNotFound();
            }
            return View(transactiontype);
        }

        //
        // GET: /TransactionsTypes/Create
        [ResourceAuthorize]
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /TransactionsTypes/Create
        [ResourceAuthorize]
        [HttpPost]
        public ActionResult Create(TransactionType transactiontype)
        {
            if (ModelState.IsValid)
            {
                db.TransactionsTypes.Add(transactiontype);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(transactiontype);
        }

        //
        // GET: /TransactionsTypes/Edit/5
        [ResourceAuthorize]
        public ActionResult Edit(int id = 0)
        {
            TransactionType transactiontype = db.TransactionsTypes.Find(id);
            if (transactiontype == null)
            {
                return HttpNotFound();
            }
            return View(transactiontype);
        }

        //
        // POST: /TransactionsTypes/Edit/5
        [ResourceAuthorize]
        [HttpPost]
        public ActionResult Edit(TransactionType transactiontype)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transactiontype).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(transactiontype);
        }

        //
        // GET: /TransactionsTypes/Delete/5
        [ResourceAuthorize]
        public ActionResult Delete(int id = 0)
        {
            TransactionType transactiontype = db.TransactionsTypes.Find(id);
            if (transactiontype == null)
            {
                return HttpNotFound();
            }
            return View(transactiontype);
        }

        //
        // POST: /TransactionsTypes/Delete/5
        [ResourceAuthorize]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            TransactionType transactiontype = db.TransactionsTypes.Find(id);
            db.TransactionsTypes.Remove(transactiontype);
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