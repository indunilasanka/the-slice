using TheSlice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Configuration;

namespace TheSlice.Repository
{
    public class ShopRepository : RepositoryBase<TheSliceContext>
    {

        public List<ProductDetailsModel> GetProductDetails()
        {
            List<ProductDetailsModel> leadsList = new List<ProductDetailsModel>();
            PetaPoco.Database db = null;
            var dbConn = DataContext.Database.Connection;
            try
            {//ncrmsp_SearchAllLeads

                dbConn.Open();
                db = new PetaPoco.Database(dbConn);
                db.EnableAutoSelect = false;
                leadsList = db.Query<ProductDetailsModel>("exec slice_SearchAllProducts",
                    new
                    {

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

            return leadsList;
        }

        public List<Order> GetOrders()
        {
            List<Order> leadsList = new List<Order>();
            PetaPoco.Database db = null;
            var dbConn = DataContext.Database.Connection;
            try
            {//ncrmsp_SearchAllLeads

                dbConn.Open();
                db = new PetaPoco.Database(dbConn);
                db.EnableAutoSelect = false;
                leadsList = db.Query<Order>("exec slice_allOrders",
                    new
                    {

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

            return leadsList;
        }

    }
}