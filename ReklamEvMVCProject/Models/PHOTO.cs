namespace ReklamEvMVCProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PHOTO")]
    public partial class PHOTO
    {
        public int ID { get; set; }

        public string URL { get; set; }

        public int? PRODUCT_ID { get; set; }
    }
}
