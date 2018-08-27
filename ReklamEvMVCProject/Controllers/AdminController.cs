using ReklamEvMVCProject.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReklamEvMVCProject.Controllers
{
    public class AdminController : Controller
    {

        MyDbContext dbContext = new MyDbContext();


        public ActionResult AdminLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AdminLogin(USERS model)
        {
            USERS user = new USERS();
            user = dbContext.USERS.Where(i => i.USERNAME == model.USERNAME & i.PASSWORD == model.PASSWORD).FirstOrDefault();
            if (user == null)
            {
                return View();
            }
            else
            {
                Session["username"] = model.USERNAME;
                Session["id"] = model.ID;
                return RedirectToAction("ListProduct");
            }
        }

        public ActionResult AdminLogout()
        {
            Session.Remove("username");
            return RedirectToAction("AdminLogin");
        }

        public ActionResult ListProduct()
        {
            List<PRODUCT> products = dbContext.PRODUCT.ToList();
            var getAllProducts = from p in products orderby p.ID descending select p;
            //SelectList list = new SelectList(getAllProducts);
            ViewBag.ProductList = getAllProducts;

            var mySession = Session["username"];

            if (mySession == null)
            {
                return RedirectToAction("AdminLogin");
            }
            return View();
        }


        public ActionResult AddProduct()
        {
            var getCategoryNames = dbContext.CATEGORY.ToList();
            SelectList list = new SelectList(getCategoryNames, "ID", "NAME");
            ViewBag.categorylistname = list;



            var mySession = Session["username"];

            if (mySession == null)
            {
                return RedirectToAction("AdminLogin");
            }
            return View();
        }








        [HttpPost]
        public ActionResult AddProduct(PRODUCT model, HttpPostedFileBase photofile)
        {

            USERS myuser = new USERS();
            var sessionUsername = Session["username"].ToString();
            myuser = dbContext.USERS.Where(i => i.USERNAME == sessionUsername).FirstOrDefault();
            PRODUCT product = new PRODUCT();
            PHOTO myPhoto = new PHOTO();

            try
            {

                product = model;
                product.USER_ID = myuser.ID;
                product.CATEGORY_ID = model.CATEGORY_ID;


                //foreach (HttpPostedFileBase file in photofiles)
                //    {
                if (photofile != null)
                {
                    var _FileName = Path.GetFileName(photofile.FileName);
                    var _path = Path.Combine(Server.MapPath("~/imageServer/"), _FileName);
                    string dbPath = "/imageServer/"+_FileName;
                    photofile.SaveAs(_path);
                    ViewBag.Message = "File Uploaded Successfully!!";
                    myPhoto.URL = dbPath;
                    myPhoto.PRODUCT_ID = product.ID;
                    product.SINGLE_PHOTO = dbPath;
                    dbContext.PHOTO.Add(myPhoto);

                }
                //}

            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                ViewBag.PhotoMessage = "File upload failed!!" + msg;
            }


            dbContext.PRODUCT.Add(product);
            dbContext.SaveChanges();
            return RedirectToAction("AddProduct");
        }











        [HttpPost]
        public JsonResult DeleteProduct(string id)
        {
            int my_id = Convert.ToInt32(id);
            PRODUCT product = new PRODUCT();
            try
            {
                product = dbContext.PRODUCT.Where(i => i.ID == my_id).FirstOrDefault();
                dbContext.PRODUCT.Remove(product);
                dbContext.SaveChanges();
               
            }
            catch (Exception ex)
            {

            }
            return Json(new { success = "success" });
        }




        //public ActionResult DeleteProduct()
        //{


        //    var mySession = Session["username"];

        //    if (mySession == null)
        //    {
        //        return RedirectToAction("AdminLogin");
        //    }
        //    return View();
        //}


      
        public ActionResult EditProduct(int id)
        {
            //int my_id = Convert.ToInt32(id);
            PRODUCT productModel = new PRODUCT();
            var getCategoryNames = dbContext.CATEGORY.ToList();
            SelectList list = new SelectList(getCategoryNames, "ID", "NAME");
            ViewBag.categorylistname = list;


            var mySession = Session["username"];

            if (mySession == null)
            {
                return RedirectToAction("AdminLogin");
            }
            else
            {
                //    var myChosenProduct = dbContext.PRODUCT.Where(i => i.ID == id).FirstOrDefault();
                //    ViewBag.myChosenProduct = myChosenProduct;
                
                productModel = dbContext.PRODUCT.Where(i => i.ID == id).SingleOrDefault();
            }
            return View(productModel);
        }


        [HttpPost]
        public ActionResult EditProduct(PRODUCT model)
        {
            PRODUCT product = new PRODUCT();
            product = dbContext.PRODUCT.Where(i => i.ID == model.ID).FirstOrDefault();
            product.NAME = model.NAME;
            product.DESCRIPTION = model.DESCRIPTION;
            product.PRICE = model.PRICE;
            product.CATEGORY_ID = model.CATEGORY_ID;
            //product.SINGLE_PHOTO = model.SINGLE_PHOTO;
            dbContext.Entry(product).State = System.Data.Entity.EntityState.Modified;
            dbContext.SaveChanges();
            return RedirectToAction("ListProduct");
        }



        public ActionResult AddAction(CATEGORY model)
        {
            CATEGORY categoryModel = new CATEGORY();

            try
            {
                categoryModel = model;
                Session["categoryModel"] = 1;
                dbContext.CATEGORY.Add(categoryModel);
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Session["categoryModel"] = 0;
            }

            return RedirectToAction("AddCategory");
        }


        public ActionResult AddCategory(ViewCATEGORY model)
        {
            var mySession = Session["username"];
            if (mySession == null)
            {
                return RedirectToAction("AdminLogin");
            }
            ViewCATEGORY categoryModel = new ViewCATEGORY();
            return View(categoryModel);
        }

        public ActionResult EditCategory()
        {
            var getCategoryNames = dbContext.CATEGORY.ToList();
            SelectList list = new SelectList(getCategoryNames, "ID", "NAME");
            ViewBag.categorylistname = list;

            var mySession = Session["username"];

            if (mySession == null)
            {
                return RedirectToAction("AdminLogin");
            }
            return View();
        }

        [HttpPost]
        public ActionResult EditCategory(CATEGORY model)
        {
            CATEGORY category = new CATEGORY();
            category = dbContext.CATEGORY.Where(i => i.ID == model.ID).FirstOrDefault();
            category.NAME = model.NAME;
            dbContext.Entry(category).State = System.Data.Entity.EntityState.Modified;
            dbContext.SaveChanges();
            return RedirectToAction("EditCategory");
        }

        public ActionResult DeleteCategory()
        {
            var getCategoryNames = dbContext.CATEGORY.ToList();
            SelectList list = new SelectList(getCategoryNames, "ID", "NAME");
            ViewBag.categorylistname = list;

            var mySession = Session["username"];

            if (mySession == null)
            {
                return RedirectToAction("AdminLogin");
            }
            return View();
        }

        [HttpPost]
        public ActionResult DeleteCategory(CATEGORY model)
        {
            CATEGORY category = new CATEGORY();
            PRODUCT product = new PRODUCT();
            //List<PRODUCT> products = new List<PRODUCT>();
            product = dbContext.PRODUCT.Where(i => i.CATEGORY_ID == model.ID).FirstOrDefault();
            category = dbContext.CATEGORY.Where(i => i.ID == model.ID).FirstOrDefault();
            //dbContext.Entry(category).State = System.Data.Entity.EntityState.Deleted;
            dbContext.PRODUCT.Remove(product);
            dbContext.CATEGORY.Remove(category);
            dbContext.SaveChanges();
            return RedirectToAction("DeleteCategory");
        }




        [HttpPost]
        public bool UploadFile(HttpPostedFileBase file)
        {
            try
            {
                if (file.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(file.FileName);
                    string _path = Path.Combine(Server.MapPath("~/imageServer"), _FileName);
                    file.SaveAs(_path);
                }
                ViewBag.Message = "File Uploaded Successfully!!";
                return true;
            }
            catch
            {
                ViewBag.Message = "File upload failed!!";
                return false;
            }

        }



        [HttpPost]
        public bool UploadFiles(HttpPostedFileBase[] files)
        {

            //Ensure model state is valid  
            if (ModelState.IsValid)
            {   //iterating through multiple file collection   
                foreach (HttpPostedFileBase file in files)
                {
                    //Checking file is available to save.  
                    if (file != null)
                    {
                        var InputFileName = Path.GetFileName(file.FileName);
                        var ServerSavePath = Path.Combine(Server.MapPath("~/UploadedFiles/") + InputFileName);
                        //Save file to server folder  
                        file.SaveAs(ServerSavePath);
                        //assigning file uploaded status to ViewBag for showing message to user.  
                        ViewBag.UploadStatus = files.Count().ToString() + " files uploaded successfully.";
                    }

                }
            }
            return true;
        }
    }
}