﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EasyAuth;
using PersonalWebsite.Models;

namespace PersonalWebsite.Controllers
{
    [EzAuthorize]
    public class AccountController : Controller
    {
        //
        // GET: /Account/
        
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Account/Login

        [EzAllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        //
        // POST: /Account/Login

        [HttpPost]
        [EzAllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            // textboxes filled in & user is valid
            if (ModelState.IsValid && Authentication.Login(model.Username, model.Password))
            {
                return RedirectToLocal(returnUrl);
            }

            // textboxes filled in but user is not valid
            if (ModelState.IsValid)
            {
                ModelState.AddModelError("", "Invalid user credentials.");
                using (var context = new WebsiteContext())
                {
                    context.FailedAttempts.Add(
                        new FailedAttempt {
                            DateAttempted = DateTime.Now,
                            UsernameGiven = model.Username,
                            IPAddress = HttpContext.Request.UserHostAddress
                        }
                    );
                    context.SaveChanges();
                }
            }

            return View(model);
        }

        //
        // GET: /Account/Logout

        public ActionResult Logout()
        {
            Authentication.Logout();
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/Make

        [EzAllowAnonymous]
        public ActionResult Make()
        {
            Authentication.UserStore.AddUser("test", "test");
            return RedirectToAction("Login", "Account");            
        }

        //
        // GET: /Account/UpdatePassword

        public ActionResult UpdatePassword()
        {
            return View();
        }

        //
        // POST: /Account/UpdatePassword

        [HttpPost]
        [EzAllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult UpdatePassword(UpdatePasswordViewModel model)
        {
            var user = Authentication.CurrentUser;

            if (ModelState.IsValid && Authentication.Login(user.Username, model.CurrentPassword))
            {
                user.Hash = Authentication.HashPassword(model.NewPassword, user.Salt);                 
                Authentication.UserStore.UpdateUserById(user.UserId, user);

                Authentication.Logout();
                if (Authentication.Login(user.Username, model.NewPassword))
                {
                    // everything was changed OK
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Password was not changed for some reason.");
                }
            }

            return View(model);
        }

        #region ChildActions
        [ChildActionOnly]
        [EzAllowAnonymous]
        public PartialViewResult FailedAttempts()
        {
            const int limit = 7;
            var model = new FailedAttemptsViewModel();

            using (var context = new WebsiteContext())
            {
                model.FailedAttempts = (from t in context.FailedAttempts
                                        orderby t.FailedAttemptId descending
                                        select t).Take(limit).ToList();
            }

            return PartialView(model);
        }
        #endregion

        #region Helpers
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl)) return Redirect(returnUrl);
            else return RedirectToAction("Index", "Home");
        }
        #endregion
    }
}
