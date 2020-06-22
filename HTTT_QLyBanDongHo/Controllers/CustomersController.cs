using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HTTT_QLyBanDongHo.Models;
using OfficeOpenXml;
using PagedList;

namespace HTTT_QLyBanDongHo.Controllers
{
    public class CustomersController : Controller
    {
        private QLBanDongHoDBContext db = new QLBanDongHoDBContext();

        // GET: Customers
        public ActionResult Index(string sortOrder, int? page, string Status, string searchString, string currentFilter, DateTime? start, DateTime? end, int? pageSize, decimal? minOld, decimal? maxOld)
        {
            var customers = db.Customers.Include(c => c.CustomerType);
            customers = customers.AsQueryable();
            ViewBag.TotalEnity = customers.Count();
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
                customers = customers.Where(s => s.Name.Contains(searchString));
            }
            if (minOld.HasValue)
            {
                customers = customers.Where(s => s.Age >= (double?)minOld);
            }
            if (maxOld.HasValue)
            {
                customers = customers.Where(s => s.Age <= (double?)maxOld);
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
            else if (sortOrder.Equals("old-desc"))
            {
                ViewBag.DateSort = "old-asc";
                ViewBag.ColerSortIconUp = "#e0d2d2";
                ViewBag.ColerSortIconDown = "black";
            }
            else if (sortOrder.Equals("old-asc"))
            {
                ViewBag.DateSort = "old-asc";
                ViewBag.ColerSortIconUp = "#black";
                ViewBag.ColerSortIconDown = "#e0d2d2";
            }
            else if (sortOrder.Equals("email-desc"))
            {
                ViewBag.DateSort = "email-asc";
                ViewBag.ColerSortIconUp = "#e0d2d2";
                ViewBag.ColerSortIconDown = "black";
            }
            else if (sortOrder.Equals("email-asc"))
            {
                ViewBag.DateSort = "email-asc";
                ViewBag.ColerSortIconUp = "#black";
                ViewBag.ColerSortIconDown = "#e0d2d2";
            }
            else if (sortOrder.Equals("phone-desc"))
            {
                ViewBag.DateSort = "phone-asc";
                ViewBag.ColerSortIconUp = "#e0d2d2";
                ViewBag.ColerSortIconDown = "black";
            }
            else if (sortOrder.Equals("phone-asc"))
            {
                ViewBag.DateSort = "phone-asc";
                ViewBag.ColerSortIconUp = "#black";
                ViewBag.ColerSortIconDown = "#e0d2d2";
            }
            if (start != null)
            {
                var startDate = start.GetValueOrDefault().Date;
                startDate = startDate.Date + new TimeSpan(0, 0, 0);
                customers = customers.Where(p => p.CreateAt >= startDate);
            }
            if (end != null)
            {
                var endDate = end.GetValueOrDefault().Date;
                endDate = endDate.Date + new TimeSpan(23, 59, 59);
                customers = customers.Where(p => p.CreateAt <= endDate);
            }
            ViewBag.PageSize = new List<SelectListItem>()
            {
                new SelectListItem() { Value="5", Text= "5" },
                new SelectListItem() { Value="10", Text= "10" },
                new SelectListItem() { Value="15", Text= "15" },
                new SelectListItem() { Value="25", Text= "25" },
                new SelectListItem() { Value="50", Text= "50" },
                new SelectListItem() { Value = customers.ToList().Count().ToString(), Text= "All" },
            };

            switch (sortOrder)
            {
                case "email-asc":
                    customers = customers.OrderBy(p => p.Email);
                    break;
                case "email-desc":
                    customers = customers.OrderByDescending(p => p.Email);
                    break;
                case "old-asc":
                    customers = customers.OrderBy(p => p.Age);
                    break;
                case "old-desc":
                    customers = customers.OrderByDescending(p => p.Age);
                    break;
                case "name-asc":
                    customers = customers.OrderBy(p => p.Name);
                    break;
                case "name-desc":
                    customers = customers.OrderByDescending(p => p.Name);
                    break;
                case "date-asc":
                    customers = customers.OrderBy(p => p.CreateAt);
                    break;
                case "date-desc":
                    customers = customers.OrderByDescending(p => p.CreateAt);
                    break;
                case "phone-asc":
                    customers = customers.OrderBy(p => p.Phonenumber);
                    break;
                case "phone-desc":
                    customers = customers.OrderByDescending(p => p.Phonenumber);
                    break;
                default:
                    customers = customers.OrderByDescending(p => p.CreateAt);
                    break;
            }

            int pageNumber = (page ?? 1);

            if (!customers.Any())
            {
                TempData["message"] = "NotFound";
            }
            return View(customers.ToPagedList(pageNumber, defaSize));
        }

        public void ExportToExcel()
        {
            var customer = db.Customers.ToList();
            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Report");

            ws.Cells["A2"].Value = "Danh sách khách hàng";

            ws.Cells["A3"].Value = "Ngày xuất";
            ws.Cells["B3"].Value = string.Format("{0:dd/MM/yyyy HH:mm}", DateTimeOffset.Now);

            ws.Cells["A6"].Value = "Mã khách hàng";
            ws.Cells["B6"].Value = "Tên khách hàng";
            ws.Cells["C6"].Value = "Tuổi";
            ws.Cells["D6"].Value = "Ngày sinh";
            ws.Cells["E6"].Value = "Số điện thoại";
            ws.Cells["F6"].Value = "Địa chỉ";
            ws.Cells["G6"].Value = "Địa chỉ email";
            ws.Cells["H6"].Value = "Loại khách hàng";
            ws.Cells["I6"].Value = "Ngày tạo";

            int rowStart = 7;
            foreach (var i in customer)
            {
                if (i.CustomerTypeID == 3 || i.Birthday == null )
                {
                    ws.Row(rowStart).Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    ws.Row(rowStart).Style.Fill.BackgroundColor
                        .SetColor(ColorTranslator.FromHtml(string.Format("yellow")));
                }

              
                ws.Cells[string.Format("A{0}", rowStart)].Value = i.ID;
                ws.Cells[string.Format("B{0}", rowStart)].Value = i.Name;
                ws.Cells[string.Format("C{0}", rowStart)].Value = i.Age;
                if (i.Birthday == null || i.CreateAt == null)
                {
                   
                    ws.Cells[string.Format("D{0}", rowStart)].Value = "Chưa có";
                    ws.Cells[string.Format("I{0}", rowStart)].Value = "Chưa có";
                }
                else
                {
                    ws.Cells[string.Format("D{0}", rowStart)].Value = i.Birthday.Value.ToString("dd/MM/yyyy");
                    ws.Cells[string.Format("I{0}", rowStart)].Value = i.CreateAt.Value.ToString("dd/MM/yyyy");
                }
               
                ws.Cells[string.Format("E{0}", rowStart)].Value = i.Phonenumber;
                ws.Cells[string.Format("F{0}", rowStart)].Value = i.Address;
                ws.Cells[string.Format("G{0}", rowStart)].Value = i.Email;
                ws.Cells[string.Format("H{0}", rowStart)].Value = i.CustomerType.Type;
               
                rowStart++;
            }

            ws.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment; filename=DanhSachKhachHang.xlsx");
            Response.BinaryWrite(pck.GetAsByteArray());
            Response.End();
          
        }
        public ActionResult CheckList(string ListCategoryIDs)
        {
            {
                if (ListCategoryIDs != null)
                {
                    string[] listID = ListCategoryIDs.Split(',');
                    foreach (string c in listID)
                    {
                        Customer obj = db.Customers.Find(c);
                        db.Customers.Remove(obj);
                    }
                    db.SaveChanges();
                    TempData["message"] = "Delete";
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index", TempData["message"] = "CheckFail");
            }
        }

        // GET: Customers/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            ViewBag.ID = new SelectList(db.Accounts, "ID", "UserName");
            ViewBag.CustomerTypeID = new SelectList(db.CustomerTypes, "ID", "Type");
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Email,Address,Phonenumber,Gender,Birthday,YearOld,CreateAt,CustomerTypeID,AccountID")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                customer.ID = "Cus" + DateTime.Now.Millisecond;
                customer.CreateAt = DateTime.Now;
                customer.Age = DateTime.Now.Year - customer.Birthday.Value.Year;
                db.Customers.Add(customer);
                db.SaveChanges();
                TempData["message"] = "Create";
                return RedirectToAction("Index");
            }

            
            ViewBag.CustomerTypeID = new SelectList(db.CustomerTypes, "ID", "Type", customer.CustomerTypeID);
            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerTypeID = new SelectList(db.CustomerTypes, "ID", "Type", customer.CustomerTypeID);
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Customer customer)
        {
            var BirthDay = db.Customers.FirstOrDefault(c => c.ID == customer.ID);
            if (ModelState.IsValid)
            {
                if (customer.Birthday != null)
                {
                    customer.Age = DateTime.Now.Year - customer.Birthday.Value.Year;
                    db.Entry(customer).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["message"] = "Edit";
                    return RedirectToAction("Index");
                }
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                TempData["message"] = "Edit";
                return RedirectToAction("Index");
            }
            ViewBag.CustomerTypeID = new SelectList(db.CustomerTypes, "ID", "Type", customer.CustomerTypeID);
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            db.Entry(customer).State = EntityState.Modified;
            db.Customers.Remove(customer);
            db.SaveChanges();
            TempData["message"] = "Delete";
            return RedirectToAction("Index");
            
        }

        // POST: Customers/Delete/5
        

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
