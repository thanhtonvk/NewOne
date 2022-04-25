using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NewOne.Models;

namespace NewOne.Controllers.api
{
    public class FoodController : ApiController
    {
        DBContext db = new DBContext();
        [Route("api/foods/getfood")]
        public IQueryable<Food> GetFoods(String keyword)
        {
            if (string.IsNullOrEmpty(keyword)) return db.Foods;
            var model = db.Foods.Where(x => x.Status >= 0);
            return db.Foods.Where(x => x.Name.ToLower().Contains(keyword.ToLower()) || x.Image.ToLower().Contains(keyword.ToLower()));
        }
        [Route("api/foods/getfoodbyid")]
        public Food Getfood (int id)
        {
            return db.Foods.Find(id);
        }
        [Route("api/foods/postfood")]
        public int Post([FromBody] Food food)
        {
            var check = db.Foods.FirstOrDefault(x => x.Name == food.Name);
            if(check == null)
            {
                food.Status = byte.Parse(1 + "");
                db.Foods.Add(food);
                return db.SaveChanges();
            }
            else
            {
                return -1;
            }
        }
        [Route("api/foods/Putfood")]
        public int Put([FromBody] Food food)
        {
            db.Foods.Add(food);
            db.Entry(food).State = System.Data.Entity.EntityState.Modified;
           return db.SaveChanges();
        }
        [Route("api/foods/Deletefood")]
        public int Delete( int id)
        {
            var check = db.Foods.Find(id);
            check.Status = byte.Parse(-1 + "");
            db.Entry(check).State = System.Data.Entity.EntityState.Modified;
            return db.SaveChanges();
        }
        [Route("api/foods/getfooddetailbyidfood")]
        public IEnumerable<FoodDeltail> GetFoodDeltails(int idfood)
        {
            return db.FoodDeltails.Where(x => x.IDFood == idfood);
        }

    }
}
