using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Business;

namespace WebApp.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetCategories()
        {
            var business = new CategoryLogic();
            var categories = business.GetCategories();

            return Json(categories, JsonRequestBehavior.AllowGet);
        }
    }
}