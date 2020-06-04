namespace HTTT_QLyBanDongHo.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Manufacture")]
    public partial class Manufacture
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Manufacture()
        {
            Products = new HashSet<Product>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(255)]
        public string Logo { get; set; }

        [Required]
        [StringLength(255)]
        public string ProductID { get; set; }

        [Column("Create At")]
        public DateTime? Create_At { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product> Products { get; set; }
    }
}
