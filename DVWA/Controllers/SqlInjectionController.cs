using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DVWA.Controllers
{
    [Authorize]
    public class SqlInjectionController : Controller
    {
        // GET: SqlInjection
        public ActionResult Index()
        {
            return View();
        }
    }
}