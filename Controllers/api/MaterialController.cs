using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NewOne.Models;

namespace NewOne.Controllers.api
{
    public class MaterialController : ApiController
    {
        DBContext db = new DBContext();
        [Route("api/Materials/getmaterial")]
        public IQueryable<Material> GetMaterials(String keyword)
        {
            if (string.IsNullOrEmpty(keyword)) return db.Materials;
            var model = db.Materials.Where(x => x.Status >= 0);
            return model.Where(x=>x.Name.ToLower().Contains(keyword.ToLower())|| x.Unit.ToLower().Contains(keyword.ToLower()));
        }
        [Route("api/Materials/getmaterialbyID")]
         public Material GetMaterialByID(int id)
        {
            return db.Materials.Find(id);
        }

        [Route("api/Materials/postmaterial")]
        public int Post ([FromBody]Material material)
        {
            var check = db.Materials.FirstOrDefault(x => x.Name == material.Name);
            {
                if(check == null)
                {
                    material.Status = byte.Parse(1 + "");
                    db.Materials.Add(material);
                   return db.SaveChanges();
                }
                else
                {
                    return -1;
                }
            }
        }
        [Route("api/Materials/putmaterial")]
        public int Put ([FromBody] Material material)
        {
            db.Materials.Add(material);
            db.Entry(material).State = System.Data.Entity.EntityState.Modified;
            return db.SaveChanges();
        }
        [Route("api/Materials/deletematerial")]
        public int Delete(int id)
        {
            var check = db.Materials.Find(id);
            check.Status = byte.Parse(-1 + "") ;
            db.Materials.Add(check);
            db.Entry(check).State = System.Data.Entity.EntityState.Modified;
            return db.SaveChanges();

        }
       

    }
}
