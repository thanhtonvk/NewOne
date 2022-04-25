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
    public class CustomersController : Controller
    {
        private DBContext db = new DBContext();
        private string baseUrl = Comon.baseUrl;

        // GET: Customers
        public ActionResult Index()
        {
            List<Customer> customers = new List<Customer>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                var response = client.GetAsync("api/Customers/Getcustomer");
                response.Wait();
                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    customers = result.Content.ReadAsAsync<List<Customer>>().Result;
                }
            }
            return View(customers);
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = null;
       
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                var response = client.GetAsync($"api/Customers/Getcustomerbyid?id={id}");
                response.Wait();
                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    customer = result.Content.ReadAsAsync<Customer>().Result;

                }
            }
        
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,FullName,Address,PhoneNumber,Point,Avartar,Username")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                int check = -1;
                using(var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUrl);
                    var respone = client.PostAsJsonAsync<Customer>("api/Customers/Postcustomers", customer);
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
                else if(check == 0)
                {
                    ViewBag.ThongBao = "Thêm không thành công";
                }
                else
                {
                    ViewBag.ThongBao = "Khách hàng đã tồn tại";
                }
              
            }
            

            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                var response = client.GetAsync($"api/Customers/Getcustomerbyid?id={id}");
                response.Wait();
                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    customer = result.Content.ReadAsAsync<Customer>().Result;

                }
            }
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FullName,Address,PhoneNumber,Point,Avartar,Username")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                int check = 0;
                using(var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUrl);
                    var response = client.PutAsJsonAsync<Customer>("api/Customers/Putcustomers", customer);
                    response.Wait();
                    var result = response.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        check = result.Content.ReadAsAsync<int>().Result;
                        
                    }
                }
               if(check >0)
                {
                    return RedirectToAction("Index");
                }
              
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                var response = client.GetAsync($"api/Customers/Getcustomerbyid?id={id}");
                response.Wait();
                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    customer = result.Content.ReadAsAsync<Customer>().Result;
                }
            }
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            int check = 0;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                var response = client.DeleteAsync("api/Customers/Deletecustomers?id={id}");
                response.Wait();
                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    check = result.Content.ReadAsAsync<int>().Result;

                }
            }
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
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
