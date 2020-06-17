namespace HTTT_QLyBanDongHo.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Email")]
    public partial class Email
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Email()
        {
            Customers = new HashSet<Customer>();
        }

        [StringLength(255)]
        public string ID { get; set; }

        [StringLength(255)]
        public string Subject { get; set; }

        [StringLength(255)]
        public string Content { get; set; }

        [StringLength(255)]
        public string Receiver { get; set; }

        [Column("Create At")]
        public DateTime? Create_At { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Customer> Customers { get; set; }
    }
}
