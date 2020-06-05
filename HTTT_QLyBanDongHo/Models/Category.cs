namespace HTTT_QLyBanDongHo.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Category")]
    public partial class Category
    {
        
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Category()
        {
            Products = new HashSet<Product>();
          
        }

        public int ID { get; set; }

        [StringLength(255)]
        [Required(ErrorMessage ="Không được để trống")]
        [Display(Name = "Tên danh mục")]
        public string Name { get; set; }

        [Column("Create At")]
        public DateTime? Create_At { get; set; }

        [StringLength(255)]
        [Display(Name = "Trạng thái")]
        public string Status { get; set; }

       
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product> Products { get; set; }
    }
}
