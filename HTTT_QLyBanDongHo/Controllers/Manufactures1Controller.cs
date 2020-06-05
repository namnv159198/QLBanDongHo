using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HTTT_QLyBanDongHo.Models;

namespace HTTT_QLyBanDongHo.Controllers
{
    public class Manufactures1Controller : Controller
    {
        private QLBanDongHoDBContext db = new QLBanDongHoDBContext();

        // GET: Manufactures1
        public ActionResult Index()
        {
            return View(db.Manufactures.ToList());
        }

        // GET: Manufactures1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manufacture manufacture = db.Manufactures.Find(id);
            if (manufacture == null)
            {
                return HttpNotFound();
            }
            return View(manufacture);
        }

        // GET: Manufactures1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Manufactures1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Logo,ProductID,Create_At,Status")] Manufacture manufacture)
        {
            if (ModelState.IsValid)
            {
                db.Manufactures.Add(manufacture);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(manufacture);
        }

        // GET: Manufactures1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manufacture manufacture = db.Manufactures.Find(id);
            if (manufacture == null)
            {
                return HttpNotFound();
            }
            return View(manufacture);
        }

        // POST: Manufactures1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Logo,ProductID,Create_At,Status")] Manufacture manufacture)
        {
            if (ModelState.IsValid)
            {
                db.Entry(manufacture).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(manufacture);
        }

        // GET: Manufactures1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manufacture manufacture = db.Manufactures.Find(id);
            if (manufacture == null)
            {
                return HttpNotFound();
            }
            return View(manufacture);
        }

        // POST: Manufactures1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Manufacture manufacture = db.Manufactures.Find(id);
            db.Manufactures.Remove(manufacture);
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
