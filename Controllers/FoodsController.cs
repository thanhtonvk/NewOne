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
    public class FoodsController : Controller
    {
        private DBContext db = new DBContext();
        private string baseUrl = Comon.baseUrl;
        // GET: Foods
        public ActionResult Index()
        {
            List<Food> foods = new List<Food>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                var respone = client.GetAsync($"api/foods/getfood?keyword={""}");
                respone.Wait();
                var result = respone.Result;
                if (result.IsSuccessStatusCode)
                {
                    foods = result.Content.ReadAsAsync<List<Food>>().Result;

                }
            }
            return View(foods);
        }

        // GET: Foods/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Food food = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                var respone = client.GetAsync($"api/foods/getfoodbyid?id={id}");
                respone.Wait();
                var result = respone.Result;
                if (result.IsSuccessStatusCode)
                {
                    food = result.Content.ReadAsAsync<Food>().Result;
                }
            }
            if (food == null)
            {
                return HttpNotFound();
            }
            return View(food);
        }

        // GET: Foods/Create
        public ActionResult Create()
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

            ViewBag.Category = new SelectList(categoryOfFoods, "ID", "Name");
            return View();
        }

        // POST: Foods/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,IDCategory,Name,Image,Status")] Food food)
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

            ViewBag.Category = new SelectList(categoryOfFoods, "ID", "Name");

            if (ModelState.IsValid)
            {
                int check = -1;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUrl);
                    var respone = client.PostAsJsonAsync<Food>("api/foods/postfood", food);
                    respone.Wait();
                    var result = respone.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        check = result.Content.ReadAsAsync<int>().Result;
                    }
                }
                if (check == 1)
                {
                    return RedirectToAction("Index");
                }
                else if (check == 0)
                {
                    ViewBag.ThongBao = "Thêm không thành công";
                }
                else
                {
                    ViewBag.ThongBao = "Đồ ăn đã tồn tại";
                }


            }

            return View(food);
        }

        // GET: Foods/Edit/5
        public ActionResult Edit(int? id)
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

            ViewBag.Category = new SelectList(categoryOfFoods, "ID", "Name");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Food food = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                var respone = client.GetAsync($"api/foods/getfoodbyid?id={id}");
                respone.Wait();
                var result = respone.Result;
                if (result.IsSuccessStatusCode)
                {
                    food = result.Content.ReadAsAsync<Food>().Result;
                }
            }
            if (food == null)
            {
                return HttpNotFound();
            }
            return View(food);
        }

        // POST: Foods/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,IDCategory,Name,Image,Status")] Food food)
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

            ViewBag.Category = new SelectList(categoryOfFoods, "ID", "Name");
            if (ModelState.IsValid)
            {
                int check = 0;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUrl);
                    var respone = client.PutAsJsonAsync<Food>("api/foods/Putfood", food);
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
            return View(food);
        }

        // GET: Foods/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Food food = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                var respone = client.GetAsync($"api/foods/getfoodbyid?id={id}");
                respone.Wait();
                var result = respone.Result;
                if (result.IsSuccessStatusCode)
                {
                    food = result.Content.ReadAsAsync<Food>().Result;
                }
            }
            if (food == null)
            {
                return HttpNotFound();
            }
            return View(food);
        }

        // POST: Foods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Food food = null;
            int check = 0;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                var respone = client.DeleteAsync("api/foods/Deletefood?id={id}");
                respone.Wait();
                var result = respone.Result;
                if (result.IsSuccessStatusCode)
                {
                    check = result.Content.ReadAsAsync<int>().Result;
                }
            }
            db.Foods.Remove(food);
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
