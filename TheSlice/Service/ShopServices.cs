using TheSlice.Models;
using TheSlice.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TheSlice.Service
{
    public class ShopServices
    {
        // GET: AdminServices
       
        private ShopRepository _repository;

        public ShopServices()
        {
            _repository = new ShopRepository();
        }

        public List<ProductDetailsModel> GetProductDetails()
        {
            try
            {
                List<ProductDetailsModel> list = new List<ProductDetailsModel>();
                list = _repository.GetProductDetails();
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Order> GetOrders()
        {
            try
            {
                List<Order> list = new List<Order>();
                list = _repository.GetOrders();
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}