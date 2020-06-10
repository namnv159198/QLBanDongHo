using System.Configuration;

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
        public Double? Total_Price { get; set; }

        public int? Discount { get; set; }

        [Column("Create At")]
        public DateTime? Create_At { get; set; }

 
        [StringLength(255)]
        
        public string CustomerID { get; set; }

        public int OrderStatusID { get; set; }

        public int PaymentTypeID { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual OrderStatus OrderStatus { get; set; }

        public virtual PaymentType PaymentType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public void AddOrderDetails(OrderDetail orderDetails)
        {
            if (this.OrderDetails == null)
            {
                this.OrderDetails = new List<OrderDetail>();
            }
            this.Total_Price += orderDetails.UnitPrice* orderDetails.Quantity;
            this.Total_Quantity += orderDetails.Quantity;
        }
     
    }
}
