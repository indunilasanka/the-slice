using TheSlice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace TheSlice.Repository
{
    public class AdminRepository : RepositoryBase<TheSliceContext>
    {
        public AdminRepository()
        {
        }

        public ProductDetailsModel GetProduct(int? Id)
        {
            PetaPoco.Database db = null;
            var dbConn = DataContext.Database.Connection;
            ProductDetailsModel user = new ProductDetailsModel();

            try
            {
                using (DataContext)
                {
                    dbConn.Open();
                    db = new PetaPoco.Database(dbConn);
                    db.EnableAutoSelect = false;
                    user = db.Query<ProductDetailsModel>("exec slice_GetProduct " + "@Id",
                            new { Id = Id }).FirstOrDefault();
                    dbConn.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            finally
            {
                if (dbConn != null)
                {
                    dbConn.Close();
                }
            }
            return user;
        }

        public int AddProduct(ProductDetailsModel product)
        {
            int id = 0;
            PetaPoco.Database db = null;
            var dbConn = DataContext.Database.Connection;

            try
            {
                dbConn.Open();
                db = new PetaPoco.Database(dbConn);
                db.EnableAutoSelect = false;
                id = db.ExecuteScalar<int>("declare @@ret INT; exec @@ret = slice_AddProduct " + " @Name,@Title,@Sku,@AlternateText,@ShortDescription,@StockAvailability,@Price; select @@ret;",
                        new
                        {
                            Name = product.Name,
                            Title = product.Title,
                            Sku = product.Sku,
                            AlternateText = product.AlternateText,
                            ShortDescription = product.ShortDescription,
                            StockAvailability = product.StockAvailability,
                            Price = product.Price
                        });

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

        public int UpdateProduct(ProductDetailsModel product)
        {
            PetaPoco.Database db = null;
            var dbConn = DataContext.Database.Connection;
            int id = 0;

            try
            {
                dbConn.Open();
                db = new PetaPoco.Database(dbConn);
                db.EnableAutoSelect = false;
                id = db.ExecuteScalar<int>("declare @@ret INT; exec slice_EditProduct " +
                             " @Id,@Name,@Title,@Sku,@AlternateText,@ShortDescription,@StockAvailability,@Price; select @@ret; ",
                      new
                      {
                          Id = product.Id,
                          Name = product.Name,
                          Title = product.Title,
                          Sku = product.Sku,
                          AlternateText = product.AlternateText,
                          ShortDescription = product.ShortDescription,
                          StockAvailability = product.StockAvailability,
                          Price = product.Price
                      });
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

        public int DeleteProduct(int? Id)
        {
            int id = 0;
            try
            {
                PetaPoco.Database db = null;
                var dbConn = DataContext.Database.Connection;
                dbConn.Open();
                db = new PetaPoco.Database(dbConn);
                db.EnableAutoSelect = false;

                id = db.Query<int>("exec slice_DeleteProduct" + " @Id", new { Id = Id }).SingleOrDefault();
                dbConn.Close();
            }
            catch (Exception ex)
            {

            }
            return id;
        }

        public int Image(String Image)
        {
            int id = 0;
            try
            {
                PetaPoco.Database db = null;
                var dbConn = DataContext.Database.Connection;
                dbConn.Open();
                db = new PetaPoco.Database(dbConn);
                db.EnableAutoSelect = false;

                id = db.Query<int>("exec slice_Image" + " @Image", new { Image = Image }).SingleOrDefault();
                dbConn.Close();
            }
            catch (Exception ex)
            {

            }
            return id;
        }

    }
}

