namespace HTTT_QLyBanDongHo.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
        [StringLength(255)]
        public string ID { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(255)]
        public string Gender { get; set; }

        [StringLength(255)]
        public string Birthday { get; set; }

        public int? YearOld { get; set; }

        [StringLength(255)]
        public string Address { get; set; }

        [StringLength(255)]
        public string Status { get; set; }

        [Required]
        [StringLength(255)]
        public string RoleID { get; set; }

        [StringLength(255)]
        public string Email { get; set; }

        [StringLength(255)]
        public string Password { get; set; }

        [StringLength(255)]
        public string Username { get; set; }

        [Required]
        [StringLength(255)]
        public string AccountID { get; set; }

        public virtual Account Account { get; set; }

        public virtual Role Role { get; set; }
    }
}
