using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NewOne.Models;

namespace NewOne.Controllers.api
{
    public class OrderDetailController : ApiController
    {
        DBContext db = new DBContext();
        [Route("api/OrderDetails/GetOrderDetails")]
        public IEnumerable<OrderDetail> GetOrderControllers()
        {
            return db.OrderDetails.ToList();
        }
        [Route("api/OrderDetails/GetOrderDeatailsbyID")]
        public OrderDetail GetOrderDetail(int id)
        {
            return db.OrderDetails.Find(id);
        }
        [Route("api/OrderDetails/Post")]
        public int Post([FromBody] OrderDetail orderDetail)
        {
            int index = checkorderdetails(orderDetail.IDFoodDeltails);
            if (index > -1)
            {
                var model = db.OrderDetails.ToList()[index];
                model.Quantity += 1;
                db.OrderDetails.Add(model);
                db.Entry(orderDetail).State= System.Data.Entity.EntityState.Modified;
            }
            else
            {
                db.OrderDetails.Add(orderDetail);

            }
            return db.SaveChanges();

        }
        public int checkorderdetails(int idfooddetails)
        {
            var model = db.OrderDetails.FirstOrDefault(x => x.IDFoodDeltails == idfooddetails);
            if (model == null)
            {
                return -1;

            }
            return db.OrderDetails.ToList().IndexOf(model);

        }
        [Route("api/OrderDetails/Put")]
        public int Put ([FromBody]OrderDetail orderDetail)
        {
            db.OrderDetails.Add(orderDetail);
            db.Entry(orderDetail).State = System.Data.Entity.EntityState.Modified;
            return db.SaveChanges();
        }
        [Route("api/OrderDetails/Delete")]
        public int Delete (int idorderdetails)
        {
            var moderl = db.OrderDetails.Find(idorderdetails);
            db.Entry(moderl).State = System.Data.Entity.EntityState.Modified;
            return db.SaveChanges();
        }

    }
}

