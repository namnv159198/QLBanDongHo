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
    public class OrdersController : Controller
    {
        private QLBanDongHoDBContext db = new QLBanDongHoDBContext();

        // GET: Orders
        public ActionResult Index(string sortOrder, int? page, string Status, string searchString, string currentFilter, DateTime? start, DateTime? end, int? pageSize, decimal? minPrice, decimal? maxPrice)
        {
            var orders = db.Orders.Include(p => p.OrderStatus).Include(p => p.Customer);
            orders = orders.AsQueryable();
            ViewBag.TotalEnity = orders.Count();
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
                orders = orders.Where(s => s.Customer.Name.Contains(searchString));
            }
            if (minPrice.HasValue)
            {
                orders = orders.Where(s => s.Total_Price >= (double?)minPrice);
            }
            if (maxPrice.HasValue)
            {
                orders = orders.Where(s => s.Total_Price <= (double?)maxPrice);
            }
            if (!String.IsNullOrEmpty(Status))
            {
                orders = orders.Where(s => s.OrderStatus.ID.Equals(Status));
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
                orders = orders.Where(p => p.Create_At >= startDate);
            }
            if (end != null)
            {
                var endDate = end.GetValueOrDefault().Date;
                endDate = endDate.Date + new TimeSpan(23, 59, 59);
                orders = orders.Where(p => p.Create_At <= endDate);
            }

            var listOrderStatus = db.OrderStatus.ToList();

            SelectList statusList = new SelectList(listOrderStatus, "ID", "Status");
            ViewBag.StatusList = statusList;


            // Set vào ViewBag
            ViewBag.PageSize = new List<SelectListItem>()
                {
                new SelectListItem() { Value="5", Text= "5" },
                new SelectListItem() { Value="10", Text= "10" },
                new SelectListItem() { Value="15", Text= "15" },
                new SelectListItem() { Value="25", Text= "25" },
                new SelectListItem() { Value="50", Text= "50" },
                new SelectListItem() { Value = orders.ToList().Count().ToString(), Text= "All" },
            };

            switch (sortOrder)
            {
                case "name-asc":
                    orders = orders.OrderBy(p => p.Customer.Name);
                    break;
                case "name-desc":
                    orders = orders.OrderByDescending(p => p.Customer.Name);
                    break;
                case "date-asc":
                    orders = orders.OrderBy(p => p.Create_At);
                    break;
                case "date-desc":
                    orders = orders.OrderByDescending(p => p.Create_At);
                    break;
                default:
                    orders = orders.OrderByDescending(p => p.Create_At);
                    break;
            }

            int pageNumber = (page ?? 1);

            if (!orders.Any())
            {
                TempData["message"] = "NotFound";
            }
            return View(orders.ToPagedList(pageNumber, defaSize));
        }

        // GET: Orders/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            ViewBag.PaymentTypeID = new SelectList(db.PaymentTypes, "PaymentTypeID", "Type");
            ViewBag.CustomerID = new SelectList(db.Customers, "ID", "Name");
            ViewBag.OrderStatusID = new SelectList(db.OrderStatus, "ID", "Status");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Total_Quantity,Total_Price,Payment_Type,Discount,Create_At,CustomerID,OrderStatusID")] Order order)
        {
            if (ModelState.IsValid)
            {
                order.PaymentTypeID = order.PaymentTypeID + 1;
                order.Create_At = DateTime.Now;
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PaymentTypeID = new SelectList(db.PaymentTypes, "PaymentTypeID", "Type", order.PaymentTypeID);
            ViewBag.CustomerID = new SelectList(db.Customers, "ID", "Name", order.CustomerID);
            ViewBag.OrderStatusID = new SelectList(db.OrderStatus, "ID", "Status", order.OrderStatusID);
            return View(order);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.PaymentTypeID = new SelectList(db.PaymentTypes, "PaymentTypeID", "Type", order.PaymentTypeID);
            ViewBag.CustomerID = new SelectList(db.Customers, "ID", "Name", order.CustomerID);
            ViewBag.OrderStatusID = new SelectList(db.OrderStatus, "ID", "Status", order.OrderStatusID);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Total_Quantity,Total_Price,Payment_Type,Discount,Create_At,CustomerID,OrderStatusID")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PaymentTypeID = new SelectList(db.PaymentTypes, "PaymentTypeID", "Type", order.PaymentTypeID);

            ViewBag.CustomerID = new SelectList(db.Customers, "ID", "Name", order.CustomerID);
            ViewBag.OrderStatusID = new SelectList(db.OrderStatus, "ID", "Status", order.OrderStatusID);
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
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
