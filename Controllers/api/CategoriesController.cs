using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NewOne.Models;
namespace NewOne.Controllers.api
{
    public class CategoriesController : ApiController
    {
        DBContext db = new DBContext();
        [Route("api/categories/getcategorylist")]
        public IQueryable<CategoryOfFood> GetCategoryOfFoods()
        {
            return db.CategoryOfFoods;
        }


        [Route("api/categories/getcategorybyid")]
        public CategoryOfFood GetCategoryById(int id)
        {
            return db.CategoryOfFoods.Find(id);
        }



        [Route("api/categories/getfoodbycategory")]
        public CategoryOfFood GetFoodsByCategory(int idCategory)
        {
            return db.CategoryOfFoods.Find(idCategory);
        }



        [Route("api/categories/add")]
        public int Post([FromBody] CategoryOfFood categoryOfFood)
        {
            var check = db.CategoryOfFoods.FirstOrDefault(x=>x.Name== categoryOfFood.Name);
            if (check == null)
            {
                db.CategoryOfFoods.Add(categoryOfFood);
                return db.SaveChanges();
            }
            else
            {
                return -1;
            }
        }
        [Route("api/categories/put")]
        public int Put([FromBody] CategoryOfFood categoryOfFood)
        {
            db.CategoryOfFoods.Add(categoryOfFood);
            db.Entry(categoryOfFood).State = System.Data.Entity.EntityState.Modified;
            return db.SaveChanges();
        }
        [Route("api/categories/delete")]
        public int Delete(int id)
        {
            var model = db.CategoryOfFoods.Find(id);
            db.CategoryOfFoods.Remove(model);
            return db.SaveChanges();
        }
        [Route("api/CategoryOfFoods/getfoodbycategory")]
        public IEnumerable<Food> GetFoods(int idcategory)
        {
            return db.Foods.Where(x => x.IDCategory == idcategory);

        }
    }
}
