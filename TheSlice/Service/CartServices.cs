using TheSlice.Models;
using TheSlice.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TheSlice.Service
{
    public class CartServices
    {
        // GET: AdminServices

        private CartRepository _repository;

        public CartServices()
        {
            _repository = new CartRepository();
        }

        public List<CartItem> GetCartItems(int UserId)
        {
            try
            {
                List<CartItem> list = new List<CartItem>();
                list = _repository.GetCartItems(UserId);
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<wishListItem> GetWishListItems(int UserId)
        {
            try
            {
                List<wishListItem> list = new List<wishListItem>();
                list = _repository.GetWishListItems(UserId);
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int AddCartItems(CartItem item)
        {
            try
            {
                int id = _repository.AddCartItems(item);
                return id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int EditCartItems(CartItem item)
        {
            try
            {
                int id = _repository.EditCartItems(item);
                return id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int AddWishListItems(wishListItem item)
        {
            try
            {
                int id = _repository.AddWishListItems(item);
                return id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DeleteWish(int productId, int UserId)
        {
            int id = 0;
            try
            {
                id = _repository.DeleteWish(productId, UserId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return id;
        }

        public int DeleteCart(int productId, int UserId)
        {
            int id = 0;
            try
            {
                id = _repository.DeleteCart(productId, UserId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return id;
        }

        public int Checkout(int UserId)
        {
            int id = 0;
            try
            {
                id = _repository.Checkout(UserId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return id;
        }

    }
}