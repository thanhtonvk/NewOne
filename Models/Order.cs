namespace NewOne.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Order")]
    public partial class Order
    {


        public int ID { get; set; }
        [DisplayName("Mã khách hàng")]

        public int IDCustomer { get; set; }

        public int IDEmployee { get; set; }
        [DisplayName("Ngày đặt")]

        public DateTime? OrderDate { get; set; }

        [DisplayName("Kiểu")]
        public byte? Type { get; set; }
        [DisplayName("Trạng thái")]

        public byte? Status { get; set; }

        [Column(TypeName = "ntext")]
        [DisplayName("Vị trí")]
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
