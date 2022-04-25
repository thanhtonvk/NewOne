namespace NewOne.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CategoryOfFood")]
    public partial class CategoryOfFood
    {

        public int ID { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [Column(TypeName = "ntext")]
        public string Image { get; set; }

        public byte? Status { get; set; }

    }
}
