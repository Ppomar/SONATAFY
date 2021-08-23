using System.Web.Mvc;
using Business;
using Models;

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

        [HttpPost]
        public ActionResult Create(Category category) 
        {
            if (!ModelState.IsValid)
            {
                var business = new CategoryLogic();
                var response = business.CreateCategory(category);
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("MNOK", JsonRequestBehavior.AllowGet);
            }
        }


    }
}