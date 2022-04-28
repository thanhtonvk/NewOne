using NewOne.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewOne.Controllers
{
    public class HomeController : Controller
    {
        string baseUrl = Comon.baseUrl;
        DBContext db = new DBContext();
        public ActionResult Index(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return View();
            }
            var model = db.Accounts.FirstOrDefault(x => x.Username == username && x.Password == password);
            if (model == null)
            {
                ViewBag.ThongBao = "Tài khoản hoặc mật khẩu không chính xác";
                return RedirectToAction("Index");
            }
            else
            {
                if (model.Status == 1)
                {
                    //nhân viên
                    return RedirectToAction("Index", "Customers", new { area = "NhanVien" });

                }
                else if (model.Status == 2)
                {
                    //admin
                    return RedirectToAction("Index", "Accounts", new { area = "Admin" });
                }
                else
                {

                    ViewBag.ThongBao = "Tài khoản không hợp lệ";
                    return RedirectToAction("Index");
                }
            }

        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(Account account)
        {
            if (account == null)
            {
                return View();
            }
            else
            {
                account.Status = 1;
                db.Accounts.Add(account);
                int rs = db.SaveChanges();
                if (rs > 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.ThongBao = "Đăng ký tài khoản không thành công";
                    return View(account);
                }
            }
        }
    }
}
