using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NewOne.Models;

namespace NewOne.Controllers.api
{
    public class FoodDeltailController : ApiController
    {
        DBContext db = new DBContext();
        [Route("api/FoodDeltails/GetFoodDeltail")]
        public IEnumerable<FoodDeltail> foodDeltails()
        {

            return db.FoodDeltails.ToList();
        }
        [Route("api/FoodDeltails/GetFoodDeltailbyID")]
        public FoodDeltail foodDeltail( int id)
        {
            return db.FoodDeltails.Find(id);
        }
        [Route("api/FoodDeltails/Post")]
        public int Post([FromBody] FoodDeltail foodDeltail)
        {
            var check = db.FoodDeltails.FirstOrDefault(x => x.Status == foodDeltail.Status);
            if(check == null)
            {
                foodDeltail.Status = byte.Parse(-1 + "");
                db.FoodDeltails.Add(foodDeltail);
                return db.SaveChanges();
            }
            else
            {
                return -1;
            }

        }
        [Route ("api/FoodDeltails/Put")]
        public int Put([FromBody] FoodDeltail foodDeltail)
        {
            db.FoodDeltails.Add(foodDeltail);
            db.Entry(foodDeltail).State = System.Data.Entity.EntityState.Modified;
            return db.SaveChanges();
        }
        
        [Route("api/FeoodDeltails/Delete")]
        public int Delete(int id)
        {
            var check = db.FoodDeltails.Find(id);
            check.Status = byte.Parse(-1 + "");
            db.Entry(check).State = System.Data.Entity.EntityState.Modified;
            return db.SaveChanges();
        }
    }
}
