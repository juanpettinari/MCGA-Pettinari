﻿using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using MasVidaWebMVC.Models;
using MasVidaWebMVC.Common;

namespace MasVidaWebMVC.Controllers
{
    [AllowAnonymous]
    //[InitializeSimpleMembership]
    public class AccountController : Controller
    {
        //
        // GET: /Account/Login

        public ActionResult Login()
        {
            if (Request.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            return View();
        }

        //
        // POST: /Account/Login

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            MasVidaDataContext db = new MasVidaDataContext();
            if (ModelState.IsValid)
            {
                //The ".FirstOrDefault()" method will return either the first matched
                //result or null
                var myUser = db.Users
                    .FirstOrDefault(u => u.UserName == model.UserName
                                 && u.IsActive == true);


                if (myUser != null)    //User was found
                {
                    if (!PasswordHash.ValidatePassword(model.Password, myUser.UserPassword.ToString()))
                    {
                        ModelState.AddModelError("", "Los datos ingresados no son correctos.");
                    }
                    else { 
                        FormsAuthentication.SetAuthCookie(model.UserName, false);
                        AuditoriaController.AuditoriaLogIn(myUser);
                        //FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
                        //    "UserInfo",
                        //    DateTime.Now,
                        //    DateTime.Now.AddMinutes(30), // value of time out property
                        //    false, // Value of IsPersistent property
                        //    String.Empty,
                        //    FormsAuthentication.FormsCookiePath);

                        //string encryptedTicket = FormsAuthentication.Encrypt(ticket);

                        //HttpCookie authCookie = new HttpCookie(
                        //        FormsAuthentication.FormsCookieName,
                        //        encryptedTicket);

                        //authCookie.Secure = true;

                        //Response.Cookies.Add(authCookie);

                        return RedirectToAction("Index", "Home");
                    }
                }
                else    //User was not found
                {
                    //Do something to let them know that their credentials were not valid
                    ModelState.AddModelError("", "Los datos ingresados no son correctos.");
                }

            }
            else
                // If we got this far, something failed, redisplay form
                ModelState.AddModelError("", "Ocurrio un error.");
            
            return View(model);
        }

        //
        // POST: /Account/LogOut
        public ActionResult LogOut()
        {
            AuditoriaController.AuditoriaLogOut(User.Identity.Name);
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

    }
}
