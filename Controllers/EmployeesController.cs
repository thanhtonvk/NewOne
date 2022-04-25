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
    public class EmployeesController : Controller
    {
        private DBContext db = new DBContext();
        private string baseUrl = Comon.baseUrl;
        // GET: Employees
        public ActionResult Index()
        {
            List<Employee> employees = new List<Employee>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                var response = client.GetAsync($"api/Employees/Getemployee?keyword={""}");
                response.Wait();
                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    employees = result.Content.ReadAsAsync<List<Employee>>().Result;

                }

            }
                return View(employees);
        }

        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = null;
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                var response = client.GetAsync($"api/Employees/GetemployeebyID?id={id}");
                response.Wait();
                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    employee = result.Content.ReadAsAsync<Employee>().Result;
                }
            }
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,FullName,DateOfBirth,Address,PhoneNumber,Postion,Avatar,Username")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                int check = -1;
                using(var client= new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUrl);
                    var respone = client.PostAsJsonAsync<Employee>("api/Employees/Postemployee", employee);
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
                    ViewBag.ThongBao = "Thêm nhân viên không thành công";
                }
                else
                {
                    ViewBag.ThongBao = "Nhân viên đã tồn tại";
                }
            }

            return View(employee);
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                var respone = client.GetAsync($"api/Employees/GetemployeebyID?id={id}");
                respone.Wait();
                var result = respone.Result;
                if (result.IsSuccessStatusCode)
                {
                    employee = result.Content.ReadAsAsync<Employee>().Result;

                }
            }
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FullName,DateOfBirth,Address,PhoneNumber,Postion,Avatar,Username")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                int check = 0;
                using(var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUrl);
                    var respone = client.PutAsJsonAsync<Employee>("api/Employees/Putemployee", employee);
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
            return View(employee);
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                var respone = client.GetAsync($"api/Employees/GetemployeebyID?id={id}");
                respone.Wait();
                var result = respone.Result;
                if (result.IsSuccessStatusCode)
                {
                    employee = result.Content.ReadAsAsync<Employee>().Result;
                }
            }
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = null;
            int check = 0;
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                var respone = client.DeleteAsync("api/Employees/Deleteemployee?id={id}");
                respone.Wait();
                var result = respone.Result;
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
