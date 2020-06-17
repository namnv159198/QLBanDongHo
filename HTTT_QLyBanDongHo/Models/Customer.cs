namespace HTTT_QLyBanDongHo.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Customer")]
    public partial class Customer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Customer()
        {
            Orders = new HashSet<Order>();
            Emails = new HashSet<Email>();
        }

        [StringLength(255)]
        public string ID { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(255)]
        public string Email { get; set; }

        [StringLength(255)]
        public string Address { get; set; }

        [StringLength(255)]
        public string Phonenumber { get; set; }

        [StringLength(255)]
        public string Gender { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Birthday { get; set; }

        public int? Age { get; set; }

        public DateTime? CreateAt { get; set; }

        public int CustomerTypeID { get; set; }

 
        [StringLength(255)]
        public string AccountID { get; set; }

        public virtual Account Account { get; set; }

        public virtual CustomerType CustomerType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Email> Emails { get; set; }
    }
}
