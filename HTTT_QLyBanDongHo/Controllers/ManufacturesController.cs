using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HTTT_QLyBanDongHo.Models;
using PagedList;

namespace HTTT_QLyBanDongHo.Controllers
{
    public class ManufacturesController : Controller
    {
        private QLBanDongHoDBContext db = new QLBanDongHoDBContext();
        public static string ActiveStatus = "Đã kích hoạt";
        public static string DeActiveStatus = "Chưa kích hoạt";
        // GET: Manufactures
        public ActionResult Index(string sortOrder, int? page, string Status, string searchString, string currentFilter, DateTime? start, DateTime? end, int? pageSize)
        {
            ViewBag.Active = ActiveStatus;
            ViewBag.DeActive = DeActiveStatus;
            var manufacture = from s in db.Manufactures select s;
            manufacture = manufacture.AsQueryable();
            ViewBag.TotalEnity = manufacture.Count();
            int defaSize = (pageSize ?? 5);
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;
            if (!String.IsNullOrEmpty(searchString))
            {
                manufacture = manufacture.Where(s => s.Name.Contains(searchString));
            }
            if (!String.IsNullOrEmpty(Status))
            {
                manufacture = manufacture.Where(s => s.Status.Contains(Status));
            }
            if (string.IsNullOrEmpty(sortOrder) || sortOrder.Equals("date-asc"))
            {
                ViewBag.DateSort = "date-desc";
                ViewBag.ColerSortIconUp = "black";
                ViewBag.ColerSortIconDown = "#e0d2d2";

            }
            else if (sortOrder.Equals("date-desc"))
            {
                ViewBag.DateSort = "date-asc";
                ViewBag.ColerSortIconUp = "#e0d2d2";
                ViewBag.ColerSortIconDown = "black";
            }
            else if (sortOrder.Equals("name-desc"))
            {
                ViewBag.DateSort = "name-asc";
                ViewBag.ColerSortIconUp = "#e0d2d2";
                ViewBag.ColerSortIconDown = "black";
            }
            else if (sortOrder.Equals("name-asc"))
            {
                ViewBag.DateSort = "name-asc";
                ViewBag.ColerSortIconUp = "#black";
                ViewBag.ColerSortIconDown = "#e0d2d2";
            }


            if (start != null)
            {
                var startDate = start.GetValueOrDefault().Date;
                startDate = startDate.Date + new TimeSpan(0, 0, 0);
                manufacture = manufacture.Where(p => p.Create_At >= startDate);
            }
            if (end != null)
            {
                var endDate = end.GetValueOrDefault().Date;
                endDate = endDate.Date + new TimeSpan(23, 59, 59);
                manufacture = manufacture.Where(p => p.Create_At <= endDate);
            }
            ViewBag.PageSize = new List<SelectListItem>()
            {

                new SelectListItem() { Value="5", Text= "5" },
                new SelectListItem() { Value="10", Text= "10" },
                new SelectListItem() { Value="15", Text= "15" },
                new SelectListItem() { Value="25", Text= "25" },
                new SelectListItem() { Value="50", Text= "50" },
                new SelectListItem() { Value = manufacture.ToList().Count().ToString(), Text= "All" },
            };
            switch (sortOrder)
            {
                case "name-asc":
                    manufacture = manufacture.OrderBy(p => p.Name);
                    break;
                case "name-desc":
                    manufacture = manufacture.OrderByDescending(p => p.Name);
                    break;
                case "date-asc":
                    manufacture = manufacture.OrderBy(p => p.Create_At);
                    break;
                case "date-desc":
                    manufacture = manufacture.OrderByDescending(p => p.Create_At);
                    break;
                default:
                    manufacture = manufacture.OrderByDescending(p => p.Create_At);
                    break;
            }

            int pageNumber = (page ?? 1);
            return View(manufacture.ToPagedList(pageNumber, defaSize));
        }

        // GET: Manufactures/Details/5
        public ActionResult CheckList(string ListCategoryIDs)
        {
            {
                if (ListCategoryIDs != null)
                {
                    string[] listID = ListCategoryIDs.Split(',');
                    foreach (string c in listID)
                    {
                        Manufacture obj = db.Manufactures.Find(Convert.ToInt32(c));
                        db.Manufactures.Remove(obj);
                    }
                    db.SaveChanges();
                    TempData["message"] = "Delete";
                    return RedirectToAction("Index");
                }
                TempData["message"] = "CheckFail";
                return RedirectToAction("Index");
            }
        }

        // GET: Manufactures/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Manufactures/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Logo,ProductID,Create_At,Status")] Manufacture manufacture, string[] thumbnails)
        {
            if (ModelState.IsValid)
            {
                var checkExists = db.Manufactures.AsEnumerable().Where(c => c.Name.ToString() == manufacture.Name);
                if (!checkExists.Any())
                {
                    if (thumbnails != null && thumbnails.Length > 0)
                    {
                        manufacture.Logo = string.Join(",", thumbnails);
                    }
                    manufacture.Status = ActiveStatus;
                    manufacture.Create_At = DateTime.Now;
                    db.Manufactures.Add(manufacture);
                    db.SaveChanges();
                    TempData["message"] = "Create";
                    return RedirectToAction("Index");
                }
                TempData["message"] = "Fail";
                return View(manufacture);
            }
            TempData["message"] = "Fail";
            return View(manufacture);
        }

        // GET: Manufactures/Edit/5
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
            ViewBag.Active = ActiveStatus;
            ViewBag.DeActive = DeActiveStatus;
            return View(manufacture);
        }



        // POST: Manufactures/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Logo,ProductID,Create_At,Status")] Manufacture manufacture, string Status, string[] thumbnails)
        {
            var checkExists = db.Manufactures.Where(c => c.Name.ToString() == manufacture.Name && c.ID != manufacture.ID);
            if (checkExists.Any())
            {
                TempData["message"] = "Fail";
                return View(manufacture);
            }
            if (ModelState.IsValid)
            {
                if (thumbnails != null && thumbnails.Length > 0)
                {
                    manufacture.Logo = string.Join(",", thumbnails);
                }
                manufacture.Status = Status;
                db.Entry(manufacture).State = EntityState.Modified;
                db.SaveChanges();
                TempData["message"] = "Edit";
                return RedirectToAction("Index");
            }
            TempData["message"] = "Fail";
            return View(manufacture);
        }

        // GET: Manufactures/Delete/5
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
            db.Entry(manufacture).State = EntityState.Modified;
            db.Manufactures.Remove(manufacture);
            db.SaveChanges();
            TempData["message"] = "Delete";
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
