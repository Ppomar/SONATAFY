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
            var categories = business.GetAll();

            return Json(categories, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Create([Bind(Include = "Name, Description")] Category category) 
        {
            if (ModelState.IsValid)
            {
                var business = new CategoryLogic();
                var response = business.Create(category);
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("MNOK", JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id, Name, Description")]Category category)
        {
            if (ModelState.IsValid)
            {
                var business = new CategoryLogic();
                var response = business.Edit(category);
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("MNOK", JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult Delete([Bind(Include = "Id")] Category category)
        {
            if (category.Id > 0) 
            {
                var business = new CategoryLogic();
                var response = business.Delete(category.Id);
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("MNOK", JsonRequestBehavior.AllowGet);
            }
        }
    }
}