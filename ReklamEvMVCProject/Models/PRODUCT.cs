namespace ReklamEvMVCProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PRODUCT")]
    public partial class PRODUCT
    {
        public int ID { get; set; }

        [StringLength(150)]
        public string NAME { get; set; }

        public string DESCRIPTION { get; set; }

        public double? PRICE { get; set; }

        public int? CATEGORY_ID { get; set; }

        public int? USER_ID { get; set; }

        public string SINGLE_PHOTO { get; set; }

        public virtual CATEGORY CATEGORY { get; set; }

        public virtual USERS USERS { get; set; }
    }
}
