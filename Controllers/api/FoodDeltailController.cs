using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NewOne.Models;

namespace NewOne.Controllers.api
{
    public class FoodDetailController : ApiController
    {
        DBContext db = new DBContext();
        [Route("api/FoodDetails/GetFoodDetail")]
        public IEnumerable<FoodDetail> FoodDetails()
        {

            return db.FoodDetails.ToList();
        }
        [Route("api/FoodDetails/GetFoodDetailbyID")]
        public FoodDetail FoodDetail( int id)
        {
            return db.FoodDetails.Find(id);
        }
        [Route("api/FoodDetails/Post")]
        public int Post([FromBody] FoodDetail FoodDetail)
        {
            var check = db.FoodDetails.FirstOrDefault(x => x.Status == FoodDetail.Status);
            if(check == null)
            {
                FoodDetail.Status = byte.Parse(-1 + "");
                db.FoodDetails.Add(FoodDetail);
                return db.SaveChanges();
            }
            else
            {
                return -1;
            }

        }
        [Route ("api/FoodDetails/Put")]
        public int Put([FromBody] FoodDetail FoodDetail)
        {
            db.FoodDetails.Add(FoodDetail);
            db.Entry(FoodDetail).State = System.Data.Entity.EntityState.Modified;
            return db.SaveChanges();
        }
        
        [Route("api/FeoodDeltails/Delete")]
        public int Delete(int id)
        {
            var check = db.FoodDetails.Find(id);
            check.Status = byte.Parse(-1 + "");
            db.Entry(check).State = System.Data.Entity.EntityState.Modified;
            return db.SaveChanges();
        }
    }
}
