namespace HTTT_QLyBanDongHo.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Order")]
    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        [StringLength(255)]
        public string ID { get; set; }

        [Column("Total Quantity")]
        public int? Total_Quantity { get; set; }

        [Column("Total Price")]
        public decimal? Total_Price { get; set; }

        [Column("Payment Type")]
        [StringLength(255)]
        public string Payment_Type { get; set; }

        public int? Discount { get; set; }

        [Column("Create At")]
        public DateTime? Create_At { get; set; }

        public int OrderStatusID { get; set; }

        [Required]
        [StringLength(255)]
        public string CustomerID { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual OrderStatu OrderStatu { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
