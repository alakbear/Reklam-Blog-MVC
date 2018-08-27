namespace ReklamEvMVCProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CATEGORY")]
    public partial class CATEGORY
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CATEGORY()
        {
            PRODUCT = new HashSet<PRODUCT>();
        }

        public int ID { get; set; }

        [StringLength(150)]
        public string NAME { get; set; }

        public int? IsSuccessfull { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRODUCT> PRODUCT { get; set; }
    }

    public class ViewCATEGORY
    {
        public int ID { get; set; }

        [StringLength(150)]
        public string NAME { get; set; }

        public int? IsSuccessfull { get; set; }
    }
}
