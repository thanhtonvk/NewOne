using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NewOne.Models;

namespace NewOne.Controllers.api
{
    public class EmployeeController : ApiController
    {
        DBContext db = new DBContext();
        [Route("api/Employees/Getemployee")]
        public IQueryable<Employee> GetEmployees(String keyword)
        {
            if (string.IsNullOrEmpty(keyword))
            {
                return db.Employees;
            }
            return db.Employees.Where(x => x.FullName.ToLower().Contains(keyword.ToLower()) || x.PhoneNumber.ToLower().Contains(keyword.ToLower()) ||
            x.Username.ToLower().Contains(keyword.ToLower()));
        }
        [Route("api/Employees/GetemployeebyID")]
        public Employee GetEmployeeByID( int id)
        {
            return db.Employees.Find(id);
        }
        [Route("api/Employees/Postemployee")]
        public int Post([FromBody]Employee employee)
        {
            var check = db.Employees.FirstOrDefault(x => x.FullName == employee.FullName);
            if(check == null)
            {
                db.Employees.Add(employee);
                return db.SaveChanges();
            }
            else
            {
                return -1;
            }
        }
        [Route("api/Employees/Putemployee")]
        public int Put([FromBody] Employee employee)
        { 
            db.Employees.Add(employee);
            db.Entry(employee).State = System.Data.Entity.EntityState.Modified;
           return db.SaveChanges();
        }
        [Route ("api/Employees/Deleteemployee")]
        public int Delete( int id)
        {
            var model = db.Employees.Find(id);
            db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            return db.SaveChanges();
        }

    }
}
