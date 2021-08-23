using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using Business;

namespace WebApp.Controllers
{
    public class SalesController : Controller
    {
        // GET: Sales
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreatePurchase(List<Product> products)
        {
            if (products.Count() <= 0)
            {
                return Json("NOK");
            }
            else
            {
                var business = new SalesLogic();
                var responses = business.CreatePurchase(products);

                if (responses.Item1 == "OK")
                {
                    return PartialView("_Ticket", responses.Item2);
                }
                else
                {
                    return Json(responses.Item1, JsonRequestBehavior.AllowGet);

                }
            }
        }
    }
}