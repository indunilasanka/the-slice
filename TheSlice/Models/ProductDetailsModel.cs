using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;


namespace TheSlice.Models
{
    public class ProductDetailsModel 
    {
            public int Id { get; set; }
            public int EnteredQuantity { get; set; }
            public string Name { get; set; }
            public string ImageUrl { get; set; }       
            public string AlternateText { get; set; }
            public string Title { get; set; }
            public string Sku { get; set; }
            public string ShortDescription { get; set; }
            public string StockAvailability { get; set; }
            public decimal? Price { get; set; }
            HttpPostedFileBase File { get; set; }

    }

    public class Image
    {
        public int Id { get; set; }
        public HttpPostedFileBase File { get; set; }
    }

    public class Order
    {
        public string Name { get; set; }
        public string FullName { set; get; }
        public int Qty { set; get; }
        public string Address { set; get; }
    }

    public class OrderViewModel
    {
        public List<Order> OrderList { set; get; } = new List<Order>();
    }

    public class ProductViewModel
    {
        public List<ProductDetailsModel> ProductList { set; get; } = new List<ProductDetailsModel>();
    }

    public class ShopModel
    {
        public List<ProductDetailsModel> ProductList { set; get; } = new List<ProductDetailsModel>();
        public HeaderDetails headerDetails { get; set; } = new HeaderDetails();
    }

}