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

namespace HTTT_QLBanDongHo.Controllers
{
    public class CategoriesController : Controller
    {
        private QLBanDongHoDBContext db = new QLBanDongHoDBContext();

        // GET: Categories
        public static string ActiveStatus = "Đã kích hoạt";
        public static string DeActiveStatus = "Chưa kích hoạt";
        public ActionResult Index(string sortOrder,int? page, string Status, string searchString, string currentFilter, DateTime? start, DateTime? end, int? pageSize)
        {
            ViewBag.Active = ActiveStatus;
            ViewBag.DeActive = DeActiveStatus;
            var categories = from s in db.Categories select s;
            categories = categories.AsQueryable();
            ViewBag.TotalEnity = categories.Count();
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
                categories = categories.Where(s => s.Name.Contains(searchString));
            }
            if (!String.IsNullOrEmpty(Status))
            {
                categories = categories.Where(s => s.Status.Contains(Status));
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
                categories = categories.Where(p => p.Create_At >= startDate);
            }
            if (end != null)
            {
                var endDate = end.GetValueOrDefault().Date;
                endDate = endDate.Date + new TimeSpan(23, 59, 59);
                categories = categories.Where(p => p.Create_At <= endDate);
            }
            ViewBag.PageSize = new List<SelectListItem>()
            {
               
                new SelectListItem() { Value="5", Text= "5" },
                new SelectListItem() { Value="10", Text= "10" },
                new SelectListItem() { Value="15", Text= "15" },
                new SelectListItem() { Value="25", Text= "25" },
                new SelectListItem() { Value="50", Text= "50" },
                new SelectListItem() { Value = categories.ToList().Count().ToString(), Text= "All" },
            };
            switch (sortOrder)
            {
                case "name-asc":
                    categories = categories.OrderBy(p => p.Name);
                    break;
                case "name-desc":
                    categories = categories.OrderByDescending(p => p.Name);
                    break;
                case "date-asc":
                    categories = categories.OrderBy(p => p.Create_At);
                    break;
                case "date-desc":
                    categories = categories.OrderByDescending(p => p.Create_At);
                    break;
                default:
                    categories = categories.OrderByDescending(p => p.Create_At);
                    break;
            }

            int pageNumber = (page ?? 1);
          
            return View(categories.ToPagedList(pageNumber, defaSize));
        }

        public ActionResult CheckList(string ListCategoryIDs)
        {
            {
                if (ListCategoryIDs != null)
                {
                    string[] listID = ListCategoryIDs.Split(',');
                    foreach (string c in listID)
                    {
                        Category obj = db.Categories.Find(Convert.ToInt32(c));
                        db.Categories.Remove(obj);
                    }
                    db.SaveChanges();
                    TempData["message"] = "Delete";
                    return RedirectToAction("Index");
                }
                TempData["message"] = "CheckFail";
                return RedirectToAction("Index");
            }
        }

        // GET: Categories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Create_At")] Category category)
        {
           
            if (ModelState.IsValid)
            {
                var checkCategory = db.Categories.AsEnumerable().Where(c => c.Name.ToString() == category.Name);
                if (!checkCategory.Any())
                {
                    category.Status = ActiveStatus;
                    db.Categories.Add(category);
                    db.SaveChanges();
                    TempData["message"] = "Create";
                    return RedirectToAction("Index");
                }
                TempData["message"] = "Fail";
                return View(category);
            }
            

            return View(category);
        }

        // GET: Categories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }

            ViewBag.Active = ActiveStatus;
            ViewBag.DeActive = DeActiveStatus;
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Create_At")] Category category,string Status)
        {

            var checkCategory = db.Categories.Where(c => c.Name.ToString() == category.Name && c.ID != category.ID);
            if (checkCategory.Any())
            {
                TempData["message"] = "Fail";
                return View(category);
            }
            if (ModelState.IsValid)
            {
                     category.Status = Status;
                     db.Entry(category).State = EntityState.Modified;
                     db.SaveChanges();
                    TempData["message"] = "Edit";
                    return RedirectToAction("Index");
            }
            return View(category);

        }
        


        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            db.Entry(category).State = EntityState.Modified;
           
                db.Categories.Remove(category);
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
