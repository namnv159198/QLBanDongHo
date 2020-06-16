using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HTTT_QLyBanDongHo.Models
{
    public class CheckoutModel
    {
        public Order Order { get; set; }
        public string  Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public String Email { get; set; }
    }
}