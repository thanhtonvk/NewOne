namespace NewOne.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Order")]
    public partial class Order
    {
       

        public int ID { get; set; }

        public int IDCustomer { get; set; }

        public int IDEmployee { get; set; }

        public DateTime? OrderDate { get; set; }

        public byte? Type { get; set; }

        public byte? Status { get; set; }

        [Column(TypeName = "ntext")]
        public string Position { get; set; }

        public Customer customer()
        {
            return new DBContext().Customers.Find(IDCustomer);
        }
        public Employee employee()
        {
            return new DBContext().Employees.Find(IDEmployee);
        }

      
    }
}
