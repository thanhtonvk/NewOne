using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NewOne.Models;

namespace NewOne.Controllers.api
{
    public class OrderController : ApiController
    {
        DBContext db = new DBContext();
        [Route("api/Orders/getorder")]
        public IQueryable <Order> GetOrders( String keywrod)
        {
            var model = db.Orders.Where(x => x.Status >=0);
            return db.Orders.Where(x => x.Position.ToLower().Contains(keywrod.ToLower()) );
        }

        [Route("api/Orders/getorderbyid")]
        public Order order (int id)
        {
            return db.Orders.Find(id);

        }
        [Route("api/Orders/Postorder")]
        public int Post([FromBody] Order order)
        {
            var check = db.Orders.FirstOrDefault(x => x.Position == order.Position);
            if (check == null)
            {
                order.Status = byte.Parse(-1 + "");
                db.Orders.Add(order);
                return db.SaveChanges();
            }
            else
            {
                return -1;
            }
        }
        [Route("api/Orders/Putorder")]
        public int Put([FromBody] Order order)
        {
            db.Orders.Add(order);
            db.Entry(order).State = System.Data.Entity.EntityState.Modified;
            return db.SaveChanges();
        }
        [Route("api/Orders/Deleteorder")]
        public int Delete(int id)
        {
            var check = db.Orders.Find(id);
            check.Status = byte.Parse(-1 + "");
            db.Entry(check).State = System.Data.Entity.EntityState.Modified;
            return db.SaveChanges();
        }
        [Route("api/Orders/getorderdetailbyidorder")]
        public IEnumerable<OrderDetail> GetOrderDetails(int idorder)
        {
            return db.OrderDetails.Where(x => x.IDOrder == idorder);
        }
        [Route("api/Orders/getcustomerbyidorder")]
        public IEnumerable<Order> GetOrder(int idcustomer)
        {
            return db.Orders.Where(x => x.IDCustomer == idcustomer);
        }

    }
}
