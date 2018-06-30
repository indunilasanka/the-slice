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
    public class WishListController : Controller
    {
        // GET: WishList
        public ActionResult Index()
        {
            //this method will get wishlist details and return the wishlist view
            CartServices service = new CartServices();
            if (Session[SessionConstants.SESSION_CONTEXT_INSTANCE] != null)
            {
                //get wishlist details for the logged in user's userid
                User user = (User)(Session[SessionConstants.SESSION_CONTEXT_INSTANCE]);
                List<wishListItem> list = service.GetWishListItems(user.UserId);
                return View(list);
            }
            else
            {
                List<wishListItem> list = new List<wishListItem>();
                return View(list);
            }
        }
    }
}