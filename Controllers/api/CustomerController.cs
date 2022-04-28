using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NewOne.Models;

namespace NewOne.Controllers.api
{
    public class CustomerController : ApiController
    {
        DBContext db = new DBContext();
        [Route("api/Customers/Getcustomer")]
        public IQueryable<Customer> Getcustomers()
        {
            return db.Customers;
        }
        [Route("api/Customers/Getcustomerbyid")]
        public Customer Getcustomer(int id)
        {
            return db.Customers.Find(id);
        }
        [Route("api/Customers/Postcustomers")]
        public int Post([FromBody] Customer customer)
        {
            var check = db.Customers.FirstOrDefault(x => x.FullName == customer.FullName);
           
            if( check== null)
            {
                db.Customers.Add(customer);
               return db.SaveChanges();
            }
            else
            {
                return -1;
            }
        }
        [Route("api/Customers/Putcustomers")]
        public int Put([FromBody] Customer customer)
        {
            db.Customers.Add(customer);
            db.Entry(customer).State = System.Data.Entity.EntityState.Modified;
            return db.SaveChanges();
        }
        [Route("api/Customers/Deletecustomers")]
        public int Delete(int id)
        {
            var model = db.Customers.Find(id);
            db.Customers.Remove(model);
           return db.SaveChanges();
        }
    }
}
