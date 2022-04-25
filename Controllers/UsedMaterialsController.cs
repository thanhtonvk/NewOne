using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using NewOne.Models;

namespace NewOne.Controllers
{
    public class UsedMaterialsController : Controller
    {
        private DBContext db = new DBContext();
        private string baseUrl = Comon.baseUrl;

        // GET: UsedMaterials
        public ActionResult Index()
        {
            List<UsedMaterial> usedMaterials = new List<UsedMaterial>();
            using( var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                var respone = client.GetAsync("api/UsedMaterials/GetUsedMaterials");
                respone.Wait();
                var result = respone.Result;
                if (result.IsSuccessStatusCode)
                {
                    usedMaterials = result.Content.ReadAsAsync<List<UsedMaterial>>().Result;
                }
            }
            return View(db.UsedMaterials.ToList());
        }

        // GET: UsedMaterials/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UsedMaterial usedMaterial = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                var respone = client.GetAsync($"api/Materials/getmaterialbyID?id={id}");
                respone.Wait();
                var result = respone.Result;
                if (result.IsSuccessStatusCode)
                {
                    usedMaterial = result.Content.ReadAsAsync<UsedMaterial>().Result;
                }
            }
            if (usedMaterial == null)
            {
                return HttpNotFound();
            }
            return View(usedMaterial);
        }

        // GET: UsedMaterials/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UsedMaterials/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,IDFoodDeltails,IDMaterial,Quantity")] UsedMaterial usedMaterial)
        {
            if (ModelState.IsValid)
            {
                int check = -1;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUrl);
                    var respone = client.PostAsJsonAsync<UsedMaterial>("api/UsedMaterials/Post", usedMaterial);
                    respone.Wait();
                    var result = respone.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        check = result.Content.ReadAsAsync<int>().Result;

                    }
                    if(check == 1)
                    {
                        return RedirectToAction("Index");
                    }
                    else if(check == 0){
                        ViewBag.ThongBao = "Thêm không thành công";

                    }
                    else
                    {
                        ViewBag.ThongBao = " Đã tồn tại";
                    }
                }
             
            }

            return View(usedMaterial);
        }

        // GET: UsedMaterials/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UsedMaterial usedMaterial = null;
            using(var client= new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                
            }
            if (usedMaterial == null)
            {
                return HttpNotFound();
            }
            return View(usedMaterial);
        }

        // POST: UsedMaterials/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,IDFoodDeltails,IDMaterial,Quantity")] UsedMaterial usedMaterial)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usedMaterial).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(usedMaterial);
        }

        // GET: UsedMaterials/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UsedMaterial usedMaterial = db.UsedMaterials.Find(id);
            if (usedMaterial == null)
            {
                return HttpNotFound();
            }
            return View(usedMaterial);
        }

        // POST: UsedMaterials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UsedMaterial usedMaterial = db.UsedMaterials.Find(id);
            db.UsedMaterials.Remove(usedMaterial);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
