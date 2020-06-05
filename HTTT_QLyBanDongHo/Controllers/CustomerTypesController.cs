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
    public class CustomerTypesController : Controller
    {
        private QLBanDongHoDBContext db = new QLBanDongHoDBContext();

        // GET: CustomerTypes
        public static string ActiveStatus = "Đã kích hoạt";
        public static string DeActiveStatus = "Chưa kích hoạt";
        public ActionResult Index(string sortOrder, int? page, string Status, string searchString, string currentFilter, DateTime? start, DateTime? end, int? pageSize)
        {

            ViewBag.Active = ActiveStatus;
            ViewBag.DeActive = DeActiveStatus;
            var cus = from s in db.CustomerTypes select s;
            cus = cus.AsQueryable();
            ViewBag.TotalEnity = cus.Count();
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
                cus = cus.Where(s => s.Type.Contains(searchString));
            }
            if (!String.IsNullOrEmpty(Status))
            {
                cus = cus.Where(s => s.Status.Contains(Status));
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
                cus = cus.Where(p => p.Create_At >= startDate);
            }
            if (end != null)
            {
                var endDate = end.GetValueOrDefault().Date;
                endDate = endDate.Date + new TimeSpan(23, 59, 59);
                cus = cus.Where(p => p.Create_At <= endDate);
            }
            ViewBag.PageSize = new List<SelectListItem>()
            {

                new SelectListItem() { Value="5", Text= "5" },
                new SelectListItem() { Value="10", Text= "10" },
                new SelectListItem() { Value="15", Text= "15" },
                new SelectListItem() { Value="25", Text= "25" },
                new SelectListItem() { Value="50", Text= "50" },
                new SelectListItem() { Value = cus.ToList().Count().ToString(), Text= "All" },
            };
            switch (sortOrder)
            {
                case "name-asc":
                    cus = cus.OrderBy(p => p.Type);
                    break;
                case "name-desc":
                    cus = cus.OrderByDescending(p => p.Type);
                    break;
                case "date-asc":
                    cus = cus.OrderBy(p => p.Create_At);
                    break;
                case "date-desc":
                    cus = cus.OrderByDescending(p => p.Create_At);
                    break;
                default:
                    cus = cus.OrderByDescending(p => p.Create_At);
                    break;
            }

            int pageNumber = (page ?? 1);

            return View(cus.ToPagedList(pageNumber, defaSize));
        }
        public ActionResult CheckList(string ListCategoryIDs)
        {
            {
                if (ListCategoryIDs != null)
                {
                    string[] listID = ListCategoryIDs.Split(',');
                    foreach (string c in listID)
                    {
                        CustomerType obj = db.CustomerTypes.Find(Convert.ToInt32(c));
                        db.CustomerTypes.Remove(obj);
                    }
                    db.SaveChanges();
                    TempData["message"] = "Delete";
                    return RedirectToAction("Index");
                }
                TempData["message"] = "CheckFail";
                return RedirectToAction("Index");
            }
        }
        // GET: CustomerTypes/Details/5

        // GET: CustomerTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CustomerTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Type")] CustomerType customerType)
        {
            if (ModelState.IsValid)
            {
                var checkCategory = db.CustomerTypes.AsEnumerable().Where(c => c.Type.ToString() == customerType.Type);
                if (!checkCategory.Any())
                {
                    customerType.Status = ActiveStatus;
                    customerType.Create_At = DateTime.Now;
                    db.CustomerTypes.Add(customerType);
                    db.SaveChanges();
                    TempData["message"] = "Create";
                    return RedirectToAction("Index");
                }
                TempData["message"] = "Fail";
                return View(customerType);
            }
            TempData["message"] = "Fail";
            return View(customerType);
        }

        // GET: CustomerTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerType customerType = db.CustomerTypes.Find(id);
            if (customerType == null)
            {
                return HttpNotFound();
            }
            return View(customerType);
        }

        // POST: CustomerTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Type")] CustomerType customerType, string Status)
        {
            var checkCategory = db.CustomerTypes.Where(c => c.Type.ToString() == customerType.Type && c.ID != customerType.ID);
            if (checkCategory.Any())
            {
                TempData["message"] = "Fail";
                return View(customerType);
            }
            if (ModelState.IsValid)
            {
                customerType.Status = Status;
                db.Entry(customerType).State = EntityState.Modified;
                db.SaveChanges();
                TempData["message"] = "Edit";
                return RedirectToAction("Index");
            }
            TempData["message"] = "Fail";
            return View(customerType);
        }

        // GET: CustomerTypes/Delete/5
       
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerType customerType = db.CustomerTypes.Find(id);
            if (customerType == null)
            {
                return HttpNotFound();
            }
            db.Entry(customerType).State = EntityState.Modified;
            db.CustomerTypes.Remove(customerType);
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
