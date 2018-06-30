using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheSlice.Constants;
using TheSlice.Models;
using TheSlice.Service;

namespace Nop.Web.Controllers
{
    public partial class ShoppingCartController : Controller
    {

        public ActionResult Index()
        {
            //this method will get shopping cart details and return the shooping cart view
            decimal? Subtotal = 0;
            int Count = 0;
            CartServices service = new CartServices();
            Cart cart = new Cart();
            if (Session[SessionConstants.SESSION_CONTEXT_INSTANCE] != null)
            {
                //get cart details for the logged in user's userid
                User user = (User)(Session[SessionConstants.SESSION_CONTEXT_INSTANCE]);
                List<CartItem> list = service.GetCartItems(user.UserId);
                //calculate count and subtotal of the cart
                if (list != null)
                {
                    foreach (var item in list)
                    {
                        Count = Count + item.Qty;
                        Subtotal = Subtotal + (item.Qty * item.Price);
                    }
                }
                cart.CartItemList = list;
                cart.Count = Count;
                cart.SubTotal = Subtotal;
                return View(cart);
            }
            else
            {
                cart.CartItemList = new List<CartItem>();
                return View(cart);
            }
        }

        public virtual JsonResult AddProductToCart_Details(int productId, int shoppingCartTypeId, int qty)
        {
            if (Session[SessionConstants.SESSION_CONTEXT_INSTANCE] != null)
            {
                CartServices service = new CartServices();
                //check seesion is available, only logged in users can add to cart/wishlist
                User user = (User)(Session[SessionConstants.SESSION_CONTEXT_INSTANCE]);
                //add to shopping cart
                if (shoppingCartTypeId==1)
                {
                    CartItem item = new CartItem();
                    int cartId = 0;
                    item.Id = productId;
                    item.UserId = user.UserId;
                    item.Qty = qty;
                    List<CartItem> list = service.GetCartItems(item.UserId);
                    //check item is already exists in cart
                    foreach (var cartItem in list)
                    {
                        if (cartItem.Id == item.Id)
                        {
                            cartId = cartItem.cartId;
                            break;
                        }
                    }

                    if (cartId > 0)
                    {
                        //add new items to the cart
                        int id = service.EditCartItems(item);
                        return Json(new { Status = true, Message = "Succesfully updated the shopping cart..." }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        //update the cart with new quantitiesS
                        int id = service.AddCartItems(item);
                        return Json(new { Status = true, Message = "Succesfully added to the shopping cart..." }, JsonRequestBehavior.AllowGet);
                    }
                }

                //add to wishlist
                else
                {
                    wishListItem item = new wishListItem();
                    item.UserId = user.UserId;
                    item.Id = productId;
                    List<wishListItem> list = service.GetWishListItems(item.UserId);
                    //check item is already exists in wishlist
                    foreach (var wishItem in list)
                    {
                        if (wishItem.Id == item.Id)
                        {
                            //if already item exists alret user
                            return Json(new { Status = true, Message = "Item already exists in wishlist..." }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    //if item not in wishlist,add item to the user's wishlist
                    int id = service.AddWishListItems(item);
                    return Json(new { Status = true, Message = "Succesfully added to the wishlist..." }, JsonRequestBehavior.AllowGet);
                }
                    
            }
            else
            {
                return Json(new { Status = false, Message = "Not logged-in..." }, JsonRequestBehavior.AllowGet);
            }
        }


        public JsonResult DeleteWish(int productId)
        {
            int id = 0;
            try
            {
                //removing the item from wishlist 
                CartServices service = new CartServices();
                User user = (User)(Session[SessionConstants.SESSION_CONTEXT_INSTANCE]);
                id = service.DeleteWish(productId,user.UserId);
                return Json(new { Status = true, Message = "Delete Success" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { Status = false, Message = "Error Occured" }, JsonRequestBehavior.AllowGet);

            }
        }

        public JsonResult DeleteCart(int productId)
        {
            int id = 0;
            try
            {
                //removing the item from cart
                CartServices service = new CartServices();
                User user = (User)(Session[SessionConstants.SESSION_CONTEXT_INSTANCE]);
                id = service.DeleteCart(productId, user.UserId);
                return Json(new { Status = true, Message = "Delete Success" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { Status = false, Message = "Error Occured" }, JsonRequestBehavior.AllowGet);

            }
        }

        public JsonResult Checkout()
        {
            int id = 0;
            try
            {
                //removing from shopping cart and add those details to order table
                CartServices service = new CartServices();
                User user = (User)(Session[SessionConstants.SESSION_CONTEXT_INSTANCE]);
                id = service.Checkout(user.UserId);
                return Json(new { Status = true, Message = "Checkout Success" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { Status = false, Message = "Error Occured" }, JsonRequestBehavior.AllowGet);

            }
        }

    }
}
