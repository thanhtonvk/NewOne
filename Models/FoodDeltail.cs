namespace NewOne.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class FoodDeltail
    {
        

        public int ID { get; set; }

        public int IDFood { get; set; }

        [StringLength(20)]
        public string Size { get; set; }

        public int? Cost { get; set; }

        [Column(TypeName = "ntext")]
        public string Image { get; set; }

        public int? Point { get; set; }

        public byte? Status { get; set; }


        
    }
}
