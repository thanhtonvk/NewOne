namespace NewOne.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Food")]
    public partial class Food
    {
        

        public int ID { get; set; }

        public int IDCategory { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [Column(TypeName = "ntext")]
        public string Image { get; set; }

        public byte? Status { get; set; }

        

    }
}
