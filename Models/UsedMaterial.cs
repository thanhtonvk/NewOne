namespace NewOne.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UsedMaterial")]
    public partial class UsedMaterial
    {
        public int ID { get; set; }

        public int IDFoodDetails { get; set; }

        public int IDMaterial { get; set; }

        public double? Quantity { get; set; }

      
    }
}
