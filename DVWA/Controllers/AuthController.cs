using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using DVWA.Models.Auth;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace DVWA.Controllers
{
    public class AuthController : Controller
    {
        // GET: Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model, string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                var claims = new[] {
                    new Claim(ClaimTypes.Name, model.Username),
                    new Claim(ClaimTypes.Email, ""),
                    // can add more claims
                };

                var identity = new ClaimsIdentity(claims, "ApplicationCookie");
                var context = Request.GetOwinContext();
                var authManager = context.Authentication;

                authManager.SignIn(new Microsoft.Owin.Security.AuthenticationProperties
                    { IsPersistent = true }, identity);
            }
            
            return Redirect(ReturnUrl);
        }

        public void Logout()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie,
                DefaultAuthenticationTypes.ExternalCookie);
        }
        private IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;
    }
}