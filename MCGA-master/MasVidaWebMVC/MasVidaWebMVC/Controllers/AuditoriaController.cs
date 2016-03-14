using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MasVidaWebMVC.Controllers
{
    public class AuditoriaController : Controller
    {
        private MasVidaDataContext db = new MasVidaDataContext();

        //
        // GET: /Auditoria/

        public ActionResult Index(string ddUserName, string ddResourceName, string ddActionName, string fechaDesde, string fechaHasta)
        {
            var ListaUsuario = new List<string>();

            var ConsultaUsuario = from u in db.Audits
                                  orderby u.UserName
                                  select u.UserName;
            ListaUsuario.AddRange(ConsultaUsuario.Distinct());
            ViewBag.ddUserName = new SelectList(ListaUsuario);


            var ListaRecurso = new List<string>();

            var ConsultaRecurso = from r in db.Audits
                                  orderby r.ResourceName
                                  select r.ResourceName;
            ListaRecurso.AddRange(ConsultaRecurso.Distinct());
            ViewBag.ddResourceName = new SelectList(ListaRecurso);


            var ListaAccion = new List<string>();

            var ConsultaAccion = from a in db.Audits
                                  orderby a.ActionName
                                  select a.ActionName;
            ListaAccion.AddRange(ConsultaAccion.Distinct());
            ViewBag.ddActionName = new SelectList(ListaAccion);

            var auditorialogs = from l in db.Audits
                                select l;

            if (!String.IsNullOrEmpty(fechaDesde))
            {
                DateTime fecha1 = Convert.ToDateTime(fechaDesde);
                auditorialogs = auditorialogs.Where(fd => fd.DateTimeLog >= fecha1);
            }

            if (!String.IsNullOrEmpty(fechaHasta))
            {
                DateTime fecha2 = Convert.ToDateTime(fechaHasta);
                auditorialogs = auditorialogs.Where(fd => fd.DateTimeLog <= fecha2);
            }

            if (!String.IsNullOrEmpty(ddUserName))
                auditorialogs = auditorialogs.Where(a => a.UserName == ddUserName);

            if (!String.IsNullOrEmpty(ddResourceName))
                auditorialogs = auditorialogs.Where(a => a.ResourceName == ddResourceName);

            if (!String.IsNullOrEmpty(ddActionName))
                return View(auditorialogs.Where(a => a.ActionName == ddActionName));
            else
                return View(auditorialogs);
        }

        public static void AuditoriaLogIn(User user)
        {
            using (var db = new MasVidaDataContext())
            {
                var AuditLog = db.Audits.Create();
                AuditLog.UserName = user.UserName;
                AuditLog.ResourceName = "Account";
                AuditLog.ActionName = "Log In";
                AuditLog.DateTimeLog = DateTime.Now;

                db.Audits.Add(AuditLog);
                db.SaveChanges();
            }
        }

        public static void AuditoriaLogOut(string UserName)
        {
            using (var db = new MasVidaDataContext())
            {
                var AuditLog = db.Audits.Create();
                AuditLog.UserName = UserName;
                AuditLog.ResourceName = "Account";
                AuditLog.ActionName = "Log Out";
                AuditLog.DateTimeLog = DateTime.Now;

                db.Audits.Add(AuditLog);
                db.SaveChanges();

            }
        }


        public static void AuditoriaAltaCliente(string UserName, User user)
        {
            using (var db = new MasVidaDataContext())
            {
                var AuditLog = db.Audits.Create();
                AuditLog.UserName = UserName;
                AuditLog.ResourceName = "Cliente";
                AuditLog.ActionName = "Alta";
                AuditLog.DateTimeLog = DateTime.Now;
                AuditLog.ModifiedElement1 = user.Name;
                AuditLog.ModifiedElement2 = user.LastName;

                db.Audits.Add(AuditLog);
                db.SaveChanges();
            }
        }

        public static void AuditoriaModificacionCliente(string UserName, User user)
        {
            using (var db = new MasVidaDataContext())
            {
                var AuditLog = db.Audits.Create();
                AuditLog.UserName = UserName;
                AuditLog.ResourceName = "Cliente";
                AuditLog.ActionName = "Modificación";
                AuditLog.DateTimeLog = DateTime.Now;
                AuditLog.ModifiedElement1 = user.Name;
                AuditLog.ModifiedElement2 = user.LastName;

                db.Audits.Add(AuditLog);
                db.SaveChanges();
            }
        }

        public static void AuditoriaBajaCliente(string UserName, User user)
        {
            using (var db = new MasVidaDataContext())
            {
                var AuditLog = db.Audits.Create();
                AuditLog.UserName = UserName;
                AuditLog.ResourceName = "Cliente";
                AuditLog.ActionName = "Baja";
                AuditLog.DateTimeLog = DateTime.Now;
                AuditLog.ModifiedElement1 = user.Name;
                AuditLog.ModifiedElement2 = user.LastName;

                db.Audits.Add(AuditLog);
                db.SaveChanges();
            }
        }

        public static void AuditoriaNoAutorizadoError(string UserName)
        {
            using (var db = new MasVidaDataContext())
            {
                var AuditLog = db.Audits.Create();
                AuditLog.UserName = UserName;
                AuditLog.ResourceName = "Error";
                AuditLog.ActionName = "No Autorizado";
                AuditLog.DateTimeLog = DateTime.Now;

                db.Audits.Add(AuditLog);
                db.SaveChanges();
            }
        }

        public static void AuditoriaAltaGrupoFamiliar(string UserName, FamilyGroup familyGroup)
        {
            using (var db = new MasVidaDataContext())
            {
                var AuditLog = db.Audits.Create();
                AuditLog.UserName = UserName;
                AuditLog.ResourceName = "Grupo Familiar";
                AuditLog.ActionName = "Alta";
                AuditLog.DateTimeLog = DateTime.Now;
                AuditLog.ModifiedElement1 = familyGroup.FamilyName;

                db.Audits.Add(AuditLog);
                db.SaveChanges();
            }
        }

        public static void AuditoriaModificacionGrupoFamiliar(string UserName, FamilyGroup familyGroup)
        {
            using (var db = new MasVidaDataContext())
            {
                var AuditLog = db.Audits.Create();
                AuditLog.UserName = UserName;
                AuditLog.ResourceName = "Grupo Familiar";
                AuditLog.ActionName = "Modificación";
                AuditLog.DateTimeLog = DateTime.Now;
                AuditLog.ModifiedElement1 = familyGroup.FamilyName;

                db.Audits.Add(AuditLog);
                db.SaveChanges();
            }
        }

        public static void AuditoriaBajaGrupoFamiliar(string UserName, FamilyGroup familyGroup)
        {
            using (var db = new MasVidaDataContext())
            {
                var AuditLog = db.Audits.Create();
                AuditLog.UserName = UserName;
                AuditLog.ResourceName = "Grupo Familiar";
                AuditLog.ActionName = "Baja";
                AuditLog.DateTimeLog = DateTime.Now;
                AuditLog.ModifiedElement1 = familyGroup.FamilyName;

                db.Audits.Add(AuditLog);
                db.SaveChanges();
            }
        }

        public static void AuditoriaModificarPermiso(string UserName, UserTypes_Resources userTypes_Resources)
        {
            using (var db = new MasVidaDataContext())
            {
                var AuditLog = db.Audits.Create();
                AuditLog.UserName = UserName;
                AuditLog.ResourceName = "Permiso";
                AuditLog.ActionName = "Modificar";
                AuditLog.DateTimeLog = DateTime.Now;
                AuditLog.ModifiedElement1 = userTypes_Resources.Resource.ControllerName;
                AuditLog.ModifiedElement2 = userTypes_Resources.UsersType.UserTypeName;

                db.Audits.Add(AuditLog);
                db.SaveChanges();
            }
        }

        public static void AuditoriaAltaProducto(string UserName, Product product)
        {
            using (var db = new MasVidaDataContext())
            {
                var AuditLog = db.Audits.Create();
                AuditLog.UserName = UserName;
                AuditLog.ResourceName = "Producto";
                AuditLog.ActionName = "Alta";
                AuditLog.DateTimeLog = DateTime.Now;
                AuditLog.ModifiedElement1 = product.ProductName;

                db.Audits.Add(AuditLog);
                db.SaveChanges();
            }
        }

        public static void AuditoriaModificacionProducto(string UserName, Product product)
        {
            using (var db = new MasVidaDataContext())
            {
                var AuditLog = db.Audits.Create();
                AuditLog.UserName = UserName;
                AuditLog.ResourceName = "Producto";
                AuditLog.ActionName = "Modificación";
                AuditLog.DateTimeLog = DateTime.Now;
                AuditLog.ModifiedElement1 = product.ProductName;

                db.Audits.Add(AuditLog);
                db.SaveChanges();
            }
        }

        public static void AuditoriaBajaProducto(string UserName, Product product)
        {
            using (var db = new MasVidaDataContext())
            {
                var AuditLog = db.Audits.Create();
                AuditLog.UserName = UserName;
                AuditLog.ResourceName = "Producto";
                AuditLog.ActionName = "Baja";
                AuditLog.DateTimeLog = DateTime.Now;
                AuditLog.ModifiedElement1 = product.ProductName;

                db.Audits.Add(AuditLog);
                db.SaveChanges();
            }
        }

        public static void AuditoriaAltaRecurso(string UserName, Resource resource)
        {
            using (var db = new MasVidaDataContext())
            {
                var AuditLog = db.Audits.Create();
                AuditLog.UserName = UserName;
                AuditLog.ResourceName = "Recurso";
                AuditLog.ActionName = "Alta";
                AuditLog.DateTimeLog = DateTime.Now;
                AuditLog.ModifiedElement1 = resource.ControllerName;

                db.Audits.Add(AuditLog);
                db.SaveChanges();
            }
        }

        public static void AuditoriaModificacionRecurso(string UserName, Resource resource)
        {
            using (var db = new MasVidaDataContext())
            {
                var AuditLog = db.Audits.Create();
                AuditLog.UserName = UserName;
                AuditLog.ResourceName = "Recurso";
                AuditLog.ActionName = "Modificación";
                AuditLog.DateTimeLog = DateTime.Now;
                AuditLog.ModifiedElement1 = resource.ControllerName;

                db.Audits.Add(AuditLog);
                db.SaveChanges();
            }
        }

        public static void AuditoriaBajaRecurso(string UserName, Resource resource)
        {
            using (var db = new MasVidaDataContext())
            {
                var AuditLog = db.Audits.Create();
                AuditLog.UserName = UserName;
                AuditLog.ResourceName = "Recurso";
                AuditLog.ActionName = "Baja";
                AuditLog.DateTimeLog = DateTime.Now;
                AuditLog.ModifiedElement1 = resource.ControllerName;

                db.Audits.Add(AuditLog);
                db.SaveChanges();
            }
        }

        public static void AuditoriaAltaUsuario(string UserName, User user)
        {
            using (var db = new MasVidaDataContext())
            {
                var AuditLog = db.Audits.Create();
                AuditLog.UserName = UserName;
                AuditLog.ResourceName = "Usuario";
                AuditLog.ActionName = "Alta";
                AuditLog.DateTimeLog = DateTime.Now;
                AuditLog.ModifiedElement1 = user.Name;
                AuditLog.ModifiedElement2 = user.LastName;

                db.Audits.Add(AuditLog);
                db.SaveChanges();
            }
        }

        public static void AuditoriaModificacionUsuario(string UserName, User user)
        {
            using (var db = new MasVidaDataContext())
            {
                var AuditLog = db.Audits.Create();
                AuditLog.UserName = UserName;
                AuditLog.ResourceName = "Usuario";
                AuditLog.ActionName = "Modificación";
                AuditLog.DateTimeLog = DateTime.Now;
                AuditLog.ModifiedElement1 = user.Name;
                AuditLog.ModifiedElement2 = user.LastName;

                db.Audits.Add(AuditLog);
                db.SaveChanges();
            }
        }

        public static void AuditoriaBajaUsuario(string UserName, User user)
        {
            using (var db = new MasVidaDataContext())
            {
                var AuditLog = db.Audits.Create();
                AuditLog.UserName = UserName;
                AuditLog.ResourceName = "Usuario";
                AuditLog.ActionName = "Baja";
                AuditLog.DateTimeLog = DateTime.Now;
                AuditLog.ModifiedElement1 = user.Name;
                AuditLog.ModifiedElement2 = user.LastName;

                db.Audits.Add(AuditLog);
                db.SaveChanges();
            }
        }

        public static void AuditoriaCambiarContraseñaUsuario(string UserName, User user)
        {
            using (var db = new MasVidaDataContext())
            {
                var AuditLog = db.Audits.Create();
                AuditLog.UserName = UserName;
                AuditLog.ResourceName = "Usuario";
                AuditLog.ActionName = "Cambiar Contraseña";
                AuditLog.DateTimeLog = DateTime.Now;
                AuditLog.ModifiedElement1 = user.Name;
                AuditLog.ModifiedElement2 = user.LastName;

                db.Audits.Add(AuditLog);
                db.SaveChanges();
            }
        }

        public static void AuditoriaAltaUsertype(string UserName, UserType userType)
        {
            using (var db = new MasVidaDataContext())
            {
                var AuditLog = db.Audits.Create();
                AuditLog.UserName = UserName;
                AuditLog.ResourceName = "Tipo de Usuario";
                AuditLog.ActionName = "Alta";
                AuditLog.DateTimeLog = DateTime.Now;
                AuditLog.ModifiedElement1 = userType.UserTypeName;

                db.Audits.Add(AuditLog);
                db.SaveChanges();
            }
        }

        public static void AuditoriaModificacionUsertype(string UserName, UserType userType)
        {
            using (var db = new MasVidaDataContext())
            {
                var AuditLog = db.Audits.Create();
                AuditLog.UserName = UserName;
                AuditLog.ResourceName = "Tipo de Usuario";
                AuditLog.ActionName = "Modificación";
                AuditLog.DateTimeLog = DateTime.Now;
                AuditLog.ModifiedElement1 = userType.UserTypeName;

                db.Audits.Add(AuditLog);
                db.SaveChanges();
            }
        }

        public static void AuditoriaBajaUsertype(string UserName, UserType userType)
        {
            using (var db = new MasVidaDataContext())
            {
                var AuditLog = db.Audits.Create();
                AuditLog.UserName = UserName;
                AuditLog.ResourceName = "Tipo de Usuario";
                AuditLog.ActionName = "Baja";
                AuditLog.DateTimeLog = DateTime.Now;
                AuditLog.ModifiedElement1 = userType.UserTypeName;

                db.Audits.Add(AuditLog);
                db.SaveChanges();
            }
        }

        //
        // GET: /Auditoria/Details/5

        //public ActionResult Details(int id = 0)
        //{
        //    Audit audit = db.Audits.Find(id);
        //    if (audit == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(audit);
        //}

        //
        // GET: /Auditoria/Create

        //public ActionResult Create()
        //{
        //    return View();
        //}

        //
        // POST: /Auditoria/Create

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(Audit audit)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Audits.Add(audit);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(audit);
        //}

        ////
        //// GET: /Auditoria/Edit/5

        //public ActionResult Edit(int id = 0)
        //{
        //    Audit audit = db.Audits.Find(id);
        //    if (audit == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(audit);
        //}

        ////
        //// POST: /Auditoria/Edit/5

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(Audit audit)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(audit).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(audit);
        //}

        //
        //GET: /Auditoria/Delete/5

        //public ActionResult Delete(int id = 0)
        //{
        //    Audit audit = db.Audits.Find(id);
        //    if (audit == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(audit);
        //}

        ////
        //// POST: /Auditoria/Delete/5

        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Audit audit = db.Audits.Find(id);
        //    db.Audits.Remove(audit);
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