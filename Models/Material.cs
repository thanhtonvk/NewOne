namespace NewOne.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Material")]
    public partial class Material
    {
       

        public int ID { get; set; }

        [StringLength(50)]
        public string Category { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        public double? Quantity { get; set; }

        [StringLength(20)]
        public string Unit { get; set; }

        public int? Cost { get; set; }

        public byte? Status { get; set; }

    }
}
