using TheSlice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Configuration;

namespace TheSlice.Repository
{
    public class CartRepository : RepositoryBase<TheSliceContext>
    {

        public List<CartItem> GetCartItems(int UserId)
        {
            List<CartItem> itemsList = new List<CartItem>();
            PetaPoco.Database db = null;
            var dbConn = DataContext.Database.Connection;
            try
            {

                dbConn.Open();
                db = new PetaPoco.Database(dbConn);
                db.EnableAutoSelect = false;
                itemsList = db.Query<CartItem>("exec slice_GetCartItems " + "@UserId",
                    new
                    {
                        UserId = UserId
                    }).ToList();
            }
            catch (Exception ex)
            {


            }
            finally
            {
                if (dbConn != null)
                { dbConn.Close(); }
            }

            return itemsList;
        }


        public List<wishListItem> GetWishListItems(int UserId)
        {
            List<wishListItem> itemsList = new List<wishListItem>();
            PetaPoco.Database db = null;
            var dbConn = DataContext.Database.Connection;
            try
            {

                dbConn.Open();
                db = new PetaPoco.Database(dbConn);
                db.EnableAutoSelect = false;
                itemsList = db.Query<wishListItem>("exec slice_GetWishListItems "+"@UserId",
                    new
                    {
                        UserId = UserId
                    }).ToList();
            }
            catch (Exception ex)
            {


            }
            finally
            {
                if (dbConn != null)
                { dbConn.Close(); }
            }

            return itemsList;
        }

        public int AddCartItems(CartItem item)
        {
            int id=0;
            PetaPoco.Database db = null;
            var dbConn = DataContext.Database.Connection;
            try
            {

                dbConn.Open();
                db = new PetaPoco.Database(dbConn);
                db.EnableAutoSelect = false;
                id = db.Query<int>("exec slice_addcartItems " + "@UserId,@Id,@Qty",
                    new
                    {
                        UserId = item.UserId,
                        Id=item.Id,
                        Qty=item.Qty
                    }).FirstOrDefault();
            }
            catch (Exception ex)
            {


            }
            finally
            {
                if (dbConn != null)
                { dbConn.Close(); }
            }

            return id;
        }

        public int EditCartItems(CartItem item)
        {
            int id = 0;
            PetaPoco.Database db = null;
            var dbConn = DataContext.Database.Connection;
            try
            {

                dbConn.Open();
                db = new PetaPoco.Database(dbConn);
                db.EnableAutoSelect = false;
                id = db.Query<int>("exec slice_editcartItems " + "@UserId,@Id,@Qty",
                    new
                    {
                        UserId = item.UserId,
                        Id = item.Id,
                        Qty = item.Qty
                    }).FirstOrDefault();
            }
            catch (Exception ex)
            {


            }
            finally
            {
                if (dbConn != null)
                { dbConn.Close(); }
            }

            return id;
        }


        public int AddWishListItems(wishListItem item)
        {
            int id = 0;
            PetaPoco.Database db = null;
            var dbConn = DataContext.Database.Connection;
            try
            {
                dbConn.Open();
                db = new PetaPoco.Database(dbConn);
                db.EnableAutoSelect = false;
                id = db.Query<int>("exec slice_addwishlistItems " + "@UserId,@Id",
                    new
                    {
                        UserId = item.UserId,
                        Id = item.Id
                    }).FirstOrDefault();
            }
            catch (Exception ex)
            {


            }
            finally
            {
                if (dbConn != null)
                { dbConn.Close(); }
            }
            return id;
        }

        public int DeleteWish(int Id,int UserId)
        {
            int id = 0;
            try
            {
                PetaPoco.Database db = null;
                var dbConn = DataContext.Database.Connection;
                dbConn.Open();
                db = new PetaPoco.Database(dbConn);
                db.EnableAutoSelect = false;

                id = db.Query<int>("exec slice_DeleteWish" + " @Id,@UserId", new { Id = Id, UserId = UserId }).SingleOrDefault();
                dbConn.Close();
            }
            catch (Exception ex)
            {

            }
            return id;
        }

        public int DeleteCart(int Id, int UserId)
        {
            int id = 0;
            try
            {
                PetaPoco.Database db = null;
                var dbConn = DataContext.Database.Connection;
                dbConn.Open();
                db = new PetaPoco.Database(dbConn);
                db.EnableAutoSelect = false;

                id = db.Query<int>("exec slice_DeleteCart" + " @Id,@UserId", new { Id = Id, UserId = UserId }).SingleOrDefault();
                dbConn.Close();
            }
            catch (Exception ex)
            {

            }
            return id;
        }

        public int Checkout(int UserId)
        {
            int id = 0;
            try
            {
                PetaPoco.Database db = null;
                var dbConn = DataContext.Database.Connection;
                dbConn.Open();
                db = new PetaPoco.Database(dbConn);
                db.EnableAutoSelect = false;

                id = db.Query<int>("exec slice_Checkout" + " @UserId", new { UserId = UserId }).SingleOrDefault();
                dbConn.Close();
            }
            catch (Exception ex)
            {

            }
            return id;
        }

    }
}