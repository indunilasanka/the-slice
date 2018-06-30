using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheSlice.Models
{
    public class User
    {
        public string Password { set; get; }
        public string ConfirmPassword { set; get; }
        public string Email { set; get; }
        public string FullName { set; get; }
        public string Address { set; get; }
        public int UserId { set; get; }
        public int FormId { set; get; }

    }

}