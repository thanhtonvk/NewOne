namespace NewOne.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Customer")]
    public partial class Customer
    {
       

        public int ID { get; set; }

        [StringLength(50)]
        public string FullName { get; set; }

        [StringLength(100)]
        public string Address { get; set; }

        [StringLength(10)]
        public string PhoneNumber { get; set; }

        public int? Point { get; set; }

        [Column(TypeName = "ntext")]
        public string Avatar { get; set; }

        [Required]
        [StringLength(20)]
        public string Username { get; set; }

       
    }
}
