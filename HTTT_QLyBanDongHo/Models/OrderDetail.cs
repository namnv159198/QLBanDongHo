namespace HTTT_QLyBanDongHo.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OrderDetail
    {
        public int? Quantity { get; set; }

        public Double? UnitPrice { get; set; }


        [Key, Column(Order = 1)]
        public int ProductID { get; set; }
        [Key, Column(Order = 0)]
        [StringLength(255)]
        public string OrderID { get; set; }
        [ForeignKey("OrderID")]
        public virtual Order Order { get; set; }
        [ForeignKey("ProductID")]
        public virtual Product Product { get; set; }
    }
}
