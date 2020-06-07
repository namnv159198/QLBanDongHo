using System.Configuration;

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



        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product> Products { get; set; }
        public string GetDefaultThumbnails()
        {
            if (this.Logo != null && this.Logo.Length > 0)
            {
                var arrayThumbnails = this.Logo.Split(',');
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
            if (this.Logo != null && this.Logo.Length > 0)
            {
                var arrayThumbnails = Logo.Split(',');
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
