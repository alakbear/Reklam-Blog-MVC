using ReklamEvMVCProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReklamEvMVCProject.Controllers
{
    public class HomeController : Controller
    {
        MyDbContext dbContext = new MyDbContext();

        // GET: Home
        public ActionResult Index()
        {
            List<PRODUCT> products = dbContext.PRODUCT.ToList();
            var getCategoryNames = dbContext.CATEGORY.ToList();
            SelectList list = new SelectList(getCategoryNames, "ID", "NAME");
            ViewBag.categorylistname = list;
            var getAllProductsIndex = (from p in products orderby p.ID descending select p).Take(6);
            //dbContext.PRODUCT.OrderByDescending(i => i.ID == PRODUCT.ID).Take(6);//dbContext.PRODUCT.ToList();
            //SelectList list = new SelectList(getAllProducts);
            ViewBag.ProductListIndex = getAllProductsIndex;
            return View();
        }

        public ActionResult AboutUs()
        {
            return View();
        }

        public ActionResult Products()
        {
            List<PRODUCT> products = dbContext.PRODUCT.ToList();
            //List<PHOTO> photos = dbContext.PHOTO.ToList();
            var getAllProducts = from p in products orderby p.ID descending select p;
            //SelectList list = new SelectList(getAllProducts);
            ViewBag.ProductsList = getAllProducts;
            return View();
        }

        public ActionResult Services()
        {
            return View();
        }

        public ActionResult Results()
        {
            return View();
        }

        public ActionResult Contacts()
        {
            return View();
        }

        object myCatProductlist;

        [HttpPost]
        public ActionResult Index(PRODUCT model)
        {
            var getAllProductsById = dbContext.PRODUCT.Where(i => i.CATEGORY_ID == model.CATEGORY_ID).ToList();
            //SelectList list = new SelectList(getAllProducts);
            /*ViewBag.ProductListById*/
            myCatProductlist = getAllProductsById;
            return RedirectToAction("Products");
        }


        public ActionResult ProductDetail(int id)
        {
            PRODUCT product = new PRODUCT();
            product = dbContext.PRODUCT.Where(i => i.ID == id).FirstOrDefault();
            return View(product);
        }
    }
}