using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheSlice.Service;
using TheSlice.Models;
using TheSlice.Constants;

namespace TheSlice.Controllers
{
    public class ShopController : Controller
    {

        // GET: Shop
        public ActionResult Index()
        {
            ShopServices service = new ShopServices();
            ShopModel shopmodel = new ShopModel();
            HomeController homeController = new HomeController();
            if (Session[SessionConstants.SESSION_CONTEXT_INSTANCE] != null)
            {
                User user = (User)(Session[SessionConstants.SESSION_CONTEXT_INSTANCE]);
                //get wishlist and cart details of the logged user
                shopmodel.headerDetails = homeController.CreateSessionModel(user.UserId,user.FullName);
            }
            else
            {
                shopmodel.headerDetails = null;
            }
            //get all product details to shop page
            shopmodel.ProductList = service.GetProductDetails();
            return View(shopmodel);
        }



        // GET: Shop/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Shop/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Shop/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Shop/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Shop/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Shop/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Shop/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
