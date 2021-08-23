using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using Business;

namespace WebApp.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetProducts()
        {
            var business = new ProductLogic();
            var categories = business.GetAll();

            return Json(categories, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Create([Bind(Include = "Name, Presentation, Price, CategoryId")] Product product)
        {
            if (ModelState.IsValid)
            {
                var business = new ProductLogic();
                var response = business.Create(product);
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("MNOK", JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id, Name, Presentation, Price, CategoryId")] Product product)
        {
            if (ModelState.IsValid)
            {
                var business = new ProductLogic();
                var response = business.Edit(product);
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("MNOK", JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult Delete([Bind(Include = "Id")] Product product)
        {
            if (product.Id > 0)
            {
                var business = new ProductLogic();
                var response = business.Delete(product.Id);
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("MNOK", JsonRequestBehavior.AllowGet);
            }
        }
    }
}