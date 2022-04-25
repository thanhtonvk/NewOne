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
    public class MaterialsController : Controller
    {
        private DBContext db = new DBContext();
        private string baseUrl = Comon.baseUrl;


        // GET: Materials
        public ActionResult Index()
        {
            List<Material> materials = new List<Material>();
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                var resopne = client.GetAsync($"api/Materials/getmaterial?keyword={""}");
                resopne.Wait();
                var result = resopne.Result;
                if (result.IsSuccessStatusCode)
                {
                    materials = result.Content.ReadAsAsync<List<Material>>().Result;
                }
            }
            return View(materials);
        }

        // GET: Materials/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Material material = null;
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                var respone = client.GetAsync($"api/Materials/getmaterialbyID?id={id}");
                respone.Wait();
                var result = respone.Result;
                if (result.IsSuccessStatusCode)
                {
                    material = result.Content.ReadAsAsync<Material>().Result;
                }
            }
            if (material == null)
            {
                return HttpNotFound();
            }
            return View(material);
        }

        // GET: Materials/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Materials/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Category,Name,Quantity,Unit,Cost,Status")] Material material)
        {
            if (ModelState.IsValid)
            {
                int check = -1;
                using(var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUrl);
                    var respone = client.PostAsJsonAsync<Material>("api/Materials/postmaterial", material);
                    respone.Wait();
                    var result = respone.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        check = result.Content.ReadAsAsync<int>().Result;

                    }
                }
                if(check == 1)
                {
                    return RedirectToAction("Index");
                }
                else if ( check == 0)
                {
                    ViewBag.ThongBao = "Thêm không thành công";
                }
                else
                {
                    ViewBag.ThongBao = " Nguyễn liệu đã tồm tại ";
                }
                
            }

            return View(material);
        }

        // GET: Materials/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Material material = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                var respone = client.GetAsync($"api/Materials/getmaterialbyID?id={id}");
                respone.Wait();
                var result = respone.Result;
                if (result.IsSuccessStatusCode)
                {
                    material = result.Content.ReadAsAsync<Material>().Result;
                }
            }
            if (material == null)
            {
                return HttpNotFound();
            }
            return View(material);
        }

        // POST: Materials/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Category,Name,Quantity,Unit,Cost,Status")] Material material)
        {
            if (ModelState.IsValid)
            {
                int check = 0;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUrl);
                    var respone = client.PutAsJsonAsync<Material>("api/Materials/putmaterial", material);
                    respone.Wait();
                    var result = respone.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        check = result.Content.ReadAsAsync<int>().Result;
                    }
                }
                if (check > 0)
                {
                    return RedirectToAction("Index");
                }
              
            }
            return View(material);
        }

        // GET: Materials/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Material material = null;
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                var respone = client.GetAsync($"api/Materials/getmaterialbyID?id={id}");
                respone.Wait();
                var result = respone.Result;
                if (result.IsSuccessStatusCode)
                {
                    material = result.Content.ReadAsAsync<Material>().Result;
                }
            }
            if (material == null)
            {
                return HttpNotFound();
            }
            return View(material);
        }

        // POST: Materials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            int check = 0;
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                var respone = client.DeleteAsync("api/Materials/deletematerial?id={id}");
                respone.Wait();
                var result = respone.Result;
                if (result.IsSuccessStatusCode)
                {
                    check = result.Content.ReadAsAsync<int>().Result;
                }
            }
            Material material = db.Materials.Find(id);
            db.Materials.Remove(material);
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
