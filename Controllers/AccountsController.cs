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
    public class AccountsController : Controller
    {
        private DBContext db = new DBContext();
        private string baseUrl = Comon.baseUrl;

        // GET: Accounts
        public ActionResult Index()
        {
            List<Account> accounts = new List<Account>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                var response = client.GetAsync("api/Accounts/GetAccount");
                response.Wait();
                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    accounts = result.Content.ReadAsAsync<List<Account>>().Result;
                }
            }
            return View(accounts);
        }

        // GET: Accounts/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = new Account();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                var response = client.GetAsync($"api/Accounts/GetAccountbyUsername?username={id}");
                response.Wait();
                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    account = result.Content.ReadAsAsync<Account>().Result;
                }
                if(account == null)
                {
                    return HttpNotFound();
                }
            }
            return View(account);
        }

        // GET: Accounts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Accounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Username,Password,Status")] Account account)
        {
            if (ModelState.IsValid)
            {
                int check = -1;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUrl);
                    var response = client.PostAsJsonAsync<Account>("api/Accounts/Postaccount", account);
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
                else if(check == 0)
                {
                    ViewBag.ThongBao = "Thêm không thành công";
                }
                else
                {
                    ViewBag.ThongBao = "Tên đã tồn tại";
                }
                
            }
            

            return View(account);
        }

        // GET: Accounts/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                var response = client.GetAsync($"api/Accounts/GetAccountbyUsername?username={id}");
                response.Wait();
                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    account = result.Content.ReadAsAsync<Account>().Result;

                }
            }
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // POST: Accounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Username,Password,Status")] Account account)
        {
            if (ModelState.IsValid)
            {
                int check = 0;
                using(var client= new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUrl);
                    var response = client.PutAsJsonAsync<Account>("api/Accounts/Putaccount", account);
                    response.Wait();
                    var result = response.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        check = result.Content.ReadAsAsync<int>().Result;
                    }
                }
                if(check == 1)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.ThongBao = "Cập nhật không thành công";
                }
                
               
            }
            return View(account);
        }

        // GET: Accounts/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                var response = client.GetAsync($"api/Accounts/GetAccountbyUsername?username={id}");
                response.Wait();
                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    account = result.Content.ReadAsAsync<Account>().Result;
                }
            }
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // POST: Accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            int check = 0;
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                var response = client.DeleteAsync($"api/Accounts/Deleteaccount?username={id}");
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
