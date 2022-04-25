using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NewOne.Models;

namespace NewOne.Controllers.api
{
    public class UsedMaterialController : ApiController
    {
        DBContext db = new DBContext();
        [Route("api/UsedMaterials/GetUsedMaterials")]
        public IEnumerable<UsedMaterial> GetUsedMaterials()
        {
            return db.UsedMaterials.ToList();
        }
        [Route("api/UsedMaterials/GetUsedMaterialsbyID")]
        public UsedMaterial usedMaterial( int id)
        {
            return db.UsedMaterials.Find(id);
        }
        [Route("api/UsedMaterials/Post")]
        public int Post([FromBody] UsedMaterial usedMaterial)
        {
            var check = db.UsedMaterials.FirstOrDefault(x => x.Quantity == usedMaterial.Quantity);
            if(check == null)
            {
                db.UsedMaterials.Add(usedMaterial);
                return db.SaveChanges();
            }
            else
            {
                return -1;
            }
        }
        [Route("api/UsedMaterials/Put")]
        public int Put([FromBody] UsedMaterial usedMaterial)
        {
            db.UsedMaterials.Add(usedMaterial);
            db.Entry(usedMaterial).State = System.Data.Entity.EntityState.Modified;
            return db.SaveChanges();
        }
        [Route("api/UsedMaterials/Delete")]
        public int Delete(int id)
        {
            var check = db.UsedMaterials.Find(id);
            db.UsedMaterials.Remove(check);
            return db.SaveChanges();
        }
        [Route("api/UsedMaterials/getUsedMaterialsbyFoodDeltail")]
        public IEnumerable <UsedMaterial> GetUsedMaterials(int idfooddeltail)
        {
            return db.UsedMaterials.Where(x => x.IDFoodDeltails == idfooddeltail);
        }
    }
}
