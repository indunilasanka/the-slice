using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheSlice.Constants;
using TheSlice.Models;
using TheSlice.Service;

namespace TheSlice.Controllers
{
    public class HomeController : ApplicationController<User>
    {
        public ActionResult Index()
        {
            //this method will return the home page
            if (HasSession())
            {
                //if session is available create a model with logged users data(wishlist details and cart details) and return to the view
                var user = GetLogOnSessionModel();
                var details = CreateSessionModel(user.UserId,user.FullName);
                return View(details);
            }
            else
            {
                return View();
            }
        }

        public HeaderDetails CreateSessionModel(int UserId, String FullName)
        {   
            //this method will creating a datamodel(which contains wishlist and cart details of logged user)  
            decimal? Subtotal = 0;
            int Count = 0;
            CartServices service = new CartServices();
            HeaderDetails details = new HeaderDetails();
            List<CartItem> CartItemlist = new List<CartItem>();
            details.cart.CartItemList = service.GetCartItems(UserId);
            details.wishListCount = service.GetWishListItems(UserId).Count();
            details.FullName = FullName;

            //calculate cart subtotal and count
            if (details.cart.CartItemList != null)
            {
                foreach (var item in details.cart.CartItemList)
                {
                    Count = Count + item.Qty;
                    Subtotal = Subtotal + (item.Qty * item.Price);
                }
            }
            details.cart.Count = Count;
            details.cart.SubTotal = Subtotal;

            return details;
            
        }

        public ActionResult Login(int FormId)
        {
            //this method will return login form
            Session[SessionConstants.SESSION_CONTEXT_INSTANCE] = null;
            User user = new User();
            user.FormId = FormId;
            return View(user);
        }

        public ActionResult Register(int FormId)
        {
            //this method will return register view
            User user = new User();
            user.FormId = FormId;
            return View(user);
        }

        public ActionResult Logout(int FormId)
        {
            //this method will abondon logged user's session and redirect to home page
            Session[SessionConstants.SESSION_CONTEXT_INSTANCE] = null;
            AbandonSession();
            if (FormId == 1)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index","Shop");
            }
        }

        public JsonResult LoginSys(String Email, String Password)
        {
            //this method will check user with given email and password exists and if so logged the user
            HomeService service = new HomeService();
            var UserDetails = service.CheckUserExits(Email,Password);
            
            //Create Log on session        
            if ((UserDetails == null))
            {
                //No User
                return Json(new { Status = false, Message = "Incorrect Email or Password..." }, JsonRequestBehavior.AllowGet);
            }
            else if (UserDetails.UserId == -1)
            {   //Network Issue
                return Json(new { Status = false, Message = "Check Your Connection..." }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                //create session and redirect to logged home or shop page
                SetLogOnSessionModel(UserDetails);
                Session[SessionConstants.SESSION_CONTEXT_INSTANCE] = UserDetails;
                return Json(new { Status = true, Message = "Successfully logged in..." }, JsonRequestBehavior.AllowGet);
            }
                
        }

        public JsonResult RegisterSys(User user)
        {
            //this method will check the given email is already is existed, if not so register the user with form details
            HomeService service = new HomeService();
            //check mail is already exists
            int id = service.CheckEmailExists(user.Email);
            if (id > 0)
            {
                return Json(new { Status = false, Message = "Email already exists..." }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                //register the user
                int UserId = service.Register(user);
                if (UserId > 0)
                {
                    return Json(new { Status = true, Message = "Successfully  registered..." }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { Status = false, Message = "Check your network connection..." }, JsonRequestBehavior.AllowGet);
                }
            }         
        }
    }
}