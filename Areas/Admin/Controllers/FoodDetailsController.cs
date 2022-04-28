using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NewOne.Models;

namespace NewOne.Areas.Admin.Controllers
{
    public class FoodDetailsController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Admin/FoodDetails
        public ActionResult Index(int idFood)
        {
            return View(db.FoodDetails.Where(x=>x.IDFood ==idFood).ToList());
        }

        // GET: Admin/FoodDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FoodDetail foodDetail = db.FoodDetails.Find(id);
            if (foodDetail == null)
            {
                return HttpNotFound();
            }
            return View(foodDetail);
        }

        // GET: Admin/FoodDetails/Create
        public ActionResult Create()
        {
            var model = db.Foods.ToList();
            ViewBag.Food = new SelectList(model,"ID","Name");
            return View();
        }

        // POST: Admin/FoodDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,IDFood,Size,Cost,Image,Point,Status")] FoodDetail foodDetail)
        {
            if (ModelState.IsValid)
            {
                var model = db.Foods.ToList();
                ViewBag.Food = new SelectList(model, "ID", "Name");
                foodDetail.Status = 1;
                db.FoodDetails.Add(foodDetail);
                db.SaveChanges();
                return RedirectToAction("Index", "FoodDetails", new {idFood = foodDetail.IDFood});
            }

            return View(foodDetail);
        }

        // GET: Admin/FoodDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FoodDetail foodDetail = db.FoodDetails.Find(id);
            if (foodDetail == null)
            {
                return HttpNotFound();
            }
            return View(foodDetail);
        }

        // POST: Admin/FoodDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,IDFood,Size,Cost,Image,Point,Status")] FoodDetail foodDetail)
        {
            if (ModelState.IsValid)
            {
                var model = db.Foods.ToList();
                ViewBag.Food = new SelectList(model, "ID", "Name");
                db.Entry(foodDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "FoodDetails", new { idFood = foodDetail.IDFood });
            }
            return View(foodDetail);
        }

        // GET: Admin/FoodDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FoodDetail foodDetail = db.FoodDetails.Find(id);
            if (foodDetail == null)
            {
                return HttpNotFound();
            }
            return View(foodDetail);
        }

        // POST: Admin/FoodDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FoodDetail foodDetail = db.FoodDetails.Find(id);
            db.FoodDetails.Remove(foodDetail);
            db.SaveChanges();
            return RedirectToAction("Index", "FoodDetails", new { idFood = foodDetail.IDFood });
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
