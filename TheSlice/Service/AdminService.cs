using TheSlice.Models;
using TheSlice.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheSlice.Service
{
    public class AdminService
    {
        private readonly AdminRepository _repository;

        public AdminService()
        {
            _repository = new AdminRepository();
        }

        public ProductDetailsModel GetProduct(int? Id)
        {
            ProductDetailsModel product = new ProductDetailsModel();
            try
            {
                product = _repository.GetProduct(Id);
            }

            catch (Exception ex)
            {
                throw;
            }

            return product;
        }

        public int UpdateProduct(ProductDetailsModel product)
        {
            int id = 0;
            try
            {
                id = _repository.UpdateProduct(product);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return id;
        }

        public int DeleteProduct(int? Id)
        {
            int id = 0;
            try
            {
                id = _repository.DeleteProduct(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return id;
        }

        public int Image(String ImageUrl)
        {
            int id = 0;
            try
            {
                id = _repository.Image(ImageUrl);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return id;
        }

        public int AddProduct(ProductDetailsModel product)
        {
            int id = 0;
            try
            {
                id = _repository.AddProduct(product);

            }

            catch (Exception ex)
            {
                throw ex;
            }
            return id;
        }

    }
}