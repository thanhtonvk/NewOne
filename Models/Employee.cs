namespace NewOne.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Employee")]
    public partial class Employee
    {
        

        public int ID { get; set; }

        [StringLength(50)]
        public string FullName { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DateOfBirth { get; set; }

        [StringLength(100)]
        public string Address { get; set; }

        [StringLength(10)]
        public string PhoneNumber { get; set; }

        [StringLength(30)]
        public string Postion { get; set; }

        [Column(TypeName = "ntext")]
        public string Avatar { get; set; }

        [Required]
        [StringLength(20)]
        public string Username { get; set; }

       

       
    }
}
