using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheSlice.Models
{
    public class Cart
    {
        public decimal? SubTotal { set; get; }
        public int Count { set; get; }
        public List<CartItem> CartItemList { set; get; } = new List<CartItem>();
    }

    public class CartItem
    {
        public string Name { set; get; }
        public string ImageUrl { set; get; }
        public string Sku { set; get; }
        public decimal? Price { set; get; }
        public int Id { set; get; }
        public int Qty { set; get; }
        public int UserId { set; get; }
        public int cartId { set; get; }
    }

    public class wishListItem
    {
        public string Name { set; get; }
        public string ImageUrl { set; get; }
        public string Sku { set; get; }
        public decimal? Price { set; get; }
        public int Id { set; get; }
        public int UserId { set; get; }
    }

    public class HeaderDetails
    {
        public Cart cart { set; get; } = new Cart();
        public int wishListCount { set; get; }
        public String FullName { set; get; }
    }

}