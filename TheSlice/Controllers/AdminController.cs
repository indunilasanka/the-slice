using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheSlice.Models;
using TheSlice.Service;

namespace TheSlice.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Dashboard()
        {
            //return admin dashboard view
            return View();
        }
   
        public ActionResult Image()
        {
            //return admin image view
            return View();
        }

        public ActionResult ManageProducts()
        {
            //get all product list and return that product list to manage product view
            ProductViewModel productModel = new ProductViewModel();
            ShopServices service = new ShopServices();
            //get product list from database
            productModel.ProductList = service.GetProductDetails();
            return View(productModel);
        }

        public ActionResult ManageOrders()
        {
            //get all order list and return that order list to manage order view
            OrderViewModel productModel = new OrderViewModel();
            ShopServices service = new ShopServices();
            //get order list from database
            productModel.OrderList = service.GetOrders();
            return View(productModel);
        }


        public ActionResult AddEditProduct(int? Id)
        {
            //return add edit product view
            AdminService service = new AdminService();
            ProductDetailsModel product = new ProductDetailsModel();
            product = service.GetProduct(Id);
            if (product == null)
            {
                product = new ProductDetailsModel();
            }
            return View("AddEditProduct", product);
        }

        public ActionResult SaveProduct(ProductDetailsModel product)
        {
            //this method will update a product or add a new product to database
            int id = 0;
            AdminService service = new AdminService();
            try
            {
                if (product.Id > 0)
                {
                    //if product id has value then update that product
                    id = service.UpdateProduct(product);
                    return Json(new { Status = true, Id = id, Message = "Record updated successfully" }, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    //if product id has no value then insert a new product
                    id = service.AddProduct(product);
                    return Json(new { Status = true, Id = 0, Message = "Record added successfully" }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                return Json(new { Status = false, Message = "Error Occured" }, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult FileUpload(HttpPostedFileBase file)
        {
            if (file != null)
            {
                AdminService service = new AdminService();
                string pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(
                                       Server.MapPath("~/Content/Images/Thumbs"), pic);
                // file is uploaded
                file.SaveAs(path);

                // save the image path path to the database or you can send image
                // directly to database
                // in-case if you want to store byte[] ie. for DB
                using (MemoryStream ms = new MemoryStream())
                {
                    file.InputStream.CopyTo(ms);
                    byte[] array = ms.GetBuffer();
                }

                int id = service.Image(file.FileName);

            }
            // after successfully uploading redirect the user
            return RedirectToAction("ManageProducts");
        }

        public JsonResult DeleteProduct(int? Id)
        {
            int id = 0;
            try
            {
                if (Id.HasValue || Id.Value > 0)
                {
                    //delete the product with productId = Id
                    AdminService service = new AdminService();
                    id = service.DeleteProduct(Id);
                }
                return Json(new { Status = true, Message = "Delete Success" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { Status = false, Message = "Error Occured" }, JsonRequestBehavior.AllowGet);

            }
        }

        public ActionResult GoBackProduct()
        {
            return RedirectToAction("ManageProducts");
        }

    }
}