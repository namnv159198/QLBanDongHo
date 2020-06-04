namespace HTTT_QLyBanDongHo.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int ID { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        public decimal? Price { get; set; }

        public decimal? AfterPrice { get; set; }

        [StringLength(255)]
        public string Thumbnails { get; set; }

        public int? Discount { get; set; }

        public int? isBestSeller { get; set; }

        public int? isNew { get; set; }

        public int? isSpecial { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        [StringLength(255)]
        public string Status { get; set; }

        [Column("Create At")]
        public DateTime? Create_At { get; set; }

        public int ManufactureID { get; set; }

        public int CategoryID { get; set; }

        public virtual Category Category { get; set; }

        public virtual Manufacture Manufacture { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
