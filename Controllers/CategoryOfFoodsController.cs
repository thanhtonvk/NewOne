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
    public class CategoryOfFoodsController : Controller
    {
        private DBContext db = new DBContext();
        private string baseUrl = Comon.baseUrl;

        // GET: CategoryOfFoods
        public ActionResult Index()
        {
            //khởi tạo dữ liệu trả về
            List<CategoryOfFood> categoryOfFoods = new List<CategoryOfFood>();

            //khởi tạo client
            using (var client = new HttpClient())
            {
                //set base address cho client
                client.BaseAddress = new Uri(baseUrl);
                //request tới api và trả về response
                var response = client.GetAsync("api/categories/getcategorylist");
                //đợi yêu cầu
                response.Wait();
                //trả về kết
                var result = response.Result;
                //kiểm tra status code: 200,400,500
                if (result.IsSuccessStatusCode)
                {
                    //gán dữ liệu vừa get được
                    categoryOfFoods = result.Content.ReadAsAsync<List<CategoryOfFood>>().Result;
                }
            }

            return View(categoryOfFoods);
        }



        // GET: CategoryOfFoods/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryOfFood categoryOfFood = null;

            //get api
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                var response = client.GetAsync($"api/categories/getcategorybyid?id={id}");
                response.Wait();
                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    categoryOfFood = result.Content.ReadAsAsync<CategoryOfFood>().Result;
                }
            }





            if (categoryOfFood == null)
            {
                return HttpNotFound();
            }
            return View(categoryOfFood);
        }

        // GET: CategoryOfFoods/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoryOfFoods/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Image,Status")] CategoryOfFood categoryOfFood)
        {
            if (ModelState.IsValid)
            {
                int check = -1;
                using(var client = new HttpClient())
                {
                    client.BaseAddress= new Uri(baseUrl);
                    var response = client.PostAsJsonAsync<CategoryOfFood>("api/categories/add", categoryOfFood);
                    response.Wait();
                    var result = response.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        check = result.Content.ReadAsAsync<int>().Result;
                    }
                }
                if (check == 1)
                {
                    return RedirectToAction("Index");
                }
                else if(check==0)
                {
                    ViewBag.ThongBao = "Thêm không thành công";
                }
                else
                {
                    ViewBag.ThongBao = "Tên đã tồn tại";
                }
                
            }

            return View(categoryOfFood);
        }

        // GET: CategoryOfFoods/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryOfFood categoryOfFood = null;

            //get api
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                var response = client.GetAsync($"api/categories/getcategorybyid?id={id}");
                response.Wait();
                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    categoryOfFood = result.Content.ReadAsAsync<CategoryOfFood>().Result;
                }
            }
            if (categoryOfFood == null)
            {
                return HttpNotFound();
            }
            return View(categoryOfFood);
        }

        // POST: CategoryOfFoods/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Image,Status")] CategoryOfFood categoryOfFood)
        {
            if (ModelState.IsValid)
            {
                int check = 0;
                using(var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUrl);
                    var response = client.PutAsJsonAsync<CategoryOfFood >("api/categories/put", categoryOfFood);
                    response.Wait();
                    var result = response.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        check = result.Content.ReadAsAsync<int>().Result;
                    }
                }

                if (check == 1)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.ThongBao = "Cập nhật không thành công";
                }

               
            }
            return View(categoryOfFood);
        }

        // GET: CategoryOfFoods/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryOfFood categoryOfFood = null;

            //get api
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                var response = client.GetAsync($"api/categories/getcategorybyid?id={id}");
                response.Wait();
                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    categoryOfFood = result.Content.ReadAsAsync<CategoryOfFood>().Result;
                }
            }
            if (categoryOfFood == null)
            {
                return HttpNotFound();
            }
            return View(categoryOfFood);
        }

        // POST: CategoryOfFoods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            int check = 0;
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                var response = client.DeleteAsync($"api/categories/delete?id={id}");
                response.Wait();
                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    check = result.Content.ReadAsAsync<int>().Result;
                }
            }
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
