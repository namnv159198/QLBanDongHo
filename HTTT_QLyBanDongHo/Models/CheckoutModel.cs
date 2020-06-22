using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HTTT_QLyBanDongHo.Models
{
    public class CheckoutModel
    {
        public Order Order { get; set; }
        [Required]
        public string  Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public String Email { get; set; }
        [Required]
        public int TypePayment { get; set; }
        [Required]
        public string Gender { get; set; }
    }
}