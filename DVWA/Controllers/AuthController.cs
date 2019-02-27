using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using DVWA.Models.ADO;
using DVWA.Models.Auth;
using DVWA.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace DVWA.Controllers
{
    public class AuthController : Controller
    {
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                using (var db = new DVWAEntities())
                {
                    var isExist = db.Users.Count(x => x.username.Equals(model.Username));
                    if (isExist >= 1)
                    {
                        ModelState.AddModelError("Username", "Username has already been taken");
                        goto final;
                    }

                    var user = model.Add(db);
                    Auth.SignIn(user,Request.GetOwinContext());

                    return RedirectToAction("Index", "Home");
                }
            }
        final:
            return View(model);
        }
        // GET: Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                using (var db = new DVWAEntities())
                {
                    var pwd = model.Password.ToMd5();
                    var user = db.Users.FirstOrDefault(x => x.username.Equals(model.Username) && x.password.Equals(pwd));
                    if (user == null) goto final;
                    Auth.SignIn(user, Request.GetOwinContext());
                }
            }
        final:
            ModelState.AddModelError("Username", "Username or password is incorrect");
            return View(model);
        }

        public ActionResult Logout()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie,
                DefaultAuthenticationTypes.ExternalCookie);
            return RedirectToAction("Login", "Auth");
        }
        private IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;
    }
}