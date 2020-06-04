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

        public decimal? UnitPrice { get; set; }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProductID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(255)]
        public string OrderID { get; set; }

        public virtual Order Order { get; set; }

        public virtual Product Product { get; set; }
    }
}
