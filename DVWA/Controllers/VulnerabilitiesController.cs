using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DVWA.Models.ADO;
using DVWA.Models.Books;

namespace DVWA.Controllers
{
    [Authorize]
    public class VulnerabilitiesController : Controller
    {
        // GET: SqlInjection
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetBooks(string qr)
        {
            using (var db = new DVWAEntities())
            {
                var books = db.Database.SqlQuery<BookViewModel>($@"select book_id, a.name, b.name author, price
                                                            from Book a
                                                            left join Author b
                                                            on a.author_id = b.author_id
                                                            where a.name like '%{qr}%'");
                return Json(books, JsonRequestBehavior.AllowGet);
            }
        }
    }
}