using System.Configuration;

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

        public double? Price { get; set; }

        public double AfterPrice { get; set; }

        [StringLength(255)]
        public string Thumbnails { get; set; }

        public int? Discount { get; set; }

        public int? isBestSeller { get; set; }

        public int? isNew { get; set; }

        public int? isSpecial { get; set; }

        public string Description { get; set; }

        [StringLength(255)]
        public string Status { get; set; }

        public DateTime? CreateAt { get; set; }

        public int ManufactureID { get; set; }

        public int CategoryID { get; set; }

        public int Quantity { get; set; }

        public int? Remain { get; set; }

        public int? Sales { get; set; }

        [StringLength(255)]
        public string Note { get; set; }


        public virtual Category Category { get; set; }

        public virtual Manufacture Manufacture { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public string GetDefaultThumbnails()
        {
            if (this.Thumbnails != null && this.Thumbnails.Length > 0)
            {
                var arrayThumbnails = this.Thumbnails.Split(',');
                if (arrayThumbnails.Length > 0)
                {
                    return
                        ConfigurationManager.AppSettings["CloudinaryPrefix"] + arrayThumbnails[0];
                }

            }

            return
                ConfigurationManager.AppSettings["ImageNull"];
        }
        public string[] GetThumbnails()
        {
            if (this.Thumbnails != null && this.Thumbnails.Length > 0)
            {
                var arrayThumbnails = this.Thumbnails.Split(',');
                if (arrayThumbnails.Length > 0)
                {
                    return arrayThumbnails;
                }

            }

            return new string[0];
        }

        public string[] GetThumbnailIDs()
        {
            var idThumbnail = new List<string>();
            var thumbnails = GetThumbnails();
            foreach (var i in thumbnails)
            {
                // image/upload/v1587720852/trang-phuc-nakroth-bboy-cong-nghe-compressed_ewu3rb_qj7zct.jpg#81ad3dee47db0da23fae48523665b35024516448
                var SplittedThumbnails = i.Split('/');
                // [image,   upload,  v1587720852,  trang-phuc-nakroth-bboy-cong-nghe-compressed_ewu3rb_qj7zct.jpg#81ad3dee47db0da23fae48523665b35024516448] = 4
                //   0    ,  1 ,       2 ,             3]
                if (SplittedThumbnails.Length != 4)
                {
                    continue;
                }
                //[trang-phuc-nakroth-bboy-cong-nghe-compressed_ewu3rb_qj7zct.jpg#81ad3dee47db0da23fae48523665b35024516448]
                idThumbnail.Add(SplittedThumbnails[3].Split('.')[0]);
                // [trang-phuc-nakroth-bboy-cong-nghe-compressed_ewu3rb_qj7zct , jpg#81ad3dee47db0da23fae48523665b35024516448]
                // id = trang-phuc-nakroth-bboy-cong-nghe-compressed_ewu3rb_qj7zct 

            }
            return idThumbnail.ToArray();
        }
      
    }
}
