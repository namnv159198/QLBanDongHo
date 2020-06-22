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
    public class OrdersController : Controller
    {
        private QLBanDongHoDBContext db = new QLBanDongHoDBContext();

        // GET: Orders
        public ActionResult Index(string sortOrder, int? page, string OrdersStatus, string searchString, string currentFilter, DateTime? start, DateTime? end, int? pageSize, decimal? minPrice, decimal? maxPrice)
        {
            var orders = db.Orders.Include(p => p.OrderStatus).Include(p => p.Customer);
            orders = orders.AsQueryable();


            ViewBag.TotalEnity = orders.Count();
            ViewBag.minPrice = (minPrice * 1000000);
            ViewBag.maxPrice = maxPrice * 1000000;
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

                orders = orders.Where(s => s.Total_Price >= (double?)minPrice* 1000000);
            }
            if (maxPrice.HasValue)
            {
                orders = orders.Where(s => s.Total_Price <= (double?)maxPrice * 1000000);
            }
            if (!String.IsNullOrEmpty(OrdersStatus))
            {
                orders = orders.Where(s => s.OrderStatus.Status == OrdersStatus);
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
                ViewBag.DateSort = "name-desc";
                ViewBag.ColerSortIconUp = "#black";
                ViewBag.ColerSortIconDown = "#e0d2d2";
            }
            else if (sortOrder.Equals("quantity-asc"))
            {
                ViewBag.DateSort = "quantity-desc";
                ViewBag.ColerSortIconUp = "#black";
                ViewBag.ColerSortIconDown = "#e0d2d2";
            }
            else if (sortOrder.Equals("quantity-desc"))
            {
                ViewBag.DateSort = "quantity-asc";
                ViewBag.ColerSortIconUp = "#black";
                ViewBag.ColerSortIconDown = "#e0d2d2";
            }
            else if (sortOrder.Equals("price-asc"))
            {
                ViewBag.DateSort = "price-desc";
                ViewBag.ColerSortIconUp = "#black";
                ViewBag.ColerSortIconDown = "#e0d2d2";
            }
            else if (sortOrder.Equals("price-desc"))
            {
                ViewBag.DateSort = "price-asc";
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

            SelectList statusList = new SelectList(listOrderStatus, "Status", "Status");
            ViewBag.StatusList = statusList;
            SelectList statusListID = new SelectList(listOrderStatus, "ID", "Status");
            ViewBag.StatusListID = statusListID;

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
                case "price-asc":
                    orders = orders.OrderBy(p => p.Total_Price);
                    break;
                case "price-desc":
                    orders = orders.OrderByDescending(p => p.Total_Price);
                    break;
                case "quantity-asc":
                    orders = orders.OrderBy(p => p.Total_Quantity);
                    break;
                case "quantity-desc":
                    orders = orders.OrderByDescending(p => p.Total_Quantity);
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


        public void ExportToExcel()
        {
            var order = db.Orders.ToList();
            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Report");

            ws.Cells["A2"].Value = "Danh sách sản phẩm";

            ws.Cells["H2"].Value = "Tổng số đơn hàng ";
            ws.Cells["I2"].Value = order.Count();

            ws.Cells["H3"].Value = "Tổng số  doanh thu ";
            ws.Cells["I3"].Value = String.Format("{0:N0}", order.Sum(x => x.Total_Price)) + "VNĐ";

            ws.Cells["H4"].Value = "Tổng số sản phẩm đã bán ";
            ws.Cells["I4"].Value = String.Format("{0:N0}", order.Sum(x => x.Total_Quantity));

           

            ws.Cells["A3"].Value = "Ngày xuất";
            ws.Cells["B3"].Value = string.Format("{0:dd/MM/yyyy HH:mm}", DateTimeOffset.Now);

            ws.Cells["A7"].Value = "Mã đơn hàng";
            ws.Cells["B7"].Value = "Tên khách hàng";
            ws.Cells["C7"].Value = "Email";
            ws.Cells["D7"].Value = "Địa chỉ";
            ws.Cells["E7"].Value = "Số điện thoại";
            ws.Cells["F7"].Value = "Giới tính";
            ws.Cells["G7"].Value = "Sản phẩm";
            ws.Cells["H7"].Value = "Tổng số lượng";
            ws.Cells["I7"].Value = "Tổng tiền";
            ws.Cells["K7"].Value = "Phương thức thanh toán";
            ws.Cells["L7"].Value = "Trạng thái";
            ws.Cells["M7"].Value = "Ngày tạo";
           

            int rowStart = 8;
            foreach (var i in order)
            {
                if (i.Total_Price >= 20000000)
                {
                    ws.Row(rowStart).Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    ws.Row(rowStart).Style.Fill.BackgroundColor
                        .SetColor(ColorTranslator.FromHtml(string.Format("yellow")));
                }


                ws.Cells[string.Format("A{0}", rowStart)].Value = i.ID;
                ws.Cells[string.Format("B{0}", rowStart)].Value = i.Customer.Name;
                ws.Cells[string.Format("C{0}", rowStart)].Value = i.Customer.Email;
                ws.Cells[string.Format("D{0}", rowStart)].Value = i.Customer.Address;
                ws.Cells[string.Format("E{0}", rowStart)].Value = i.Customer.Phonenumber;
                ws.Cells[string.Format("F{0}", rowStart)].Value = i.Customer.Gender;
                foreach (var item in i.OrderDetails)
                {
                    ws.Cells[string.Format("G{0}", rowStart)].Value += item.Product.Name + "(" + item.Quantity +":"+  String.Format("{0:N0}", item.Product.AfterPrice) +") ,";
                }

                ws.Cells[string.Format("H{0}", rowStart)].Value = String.Format("{0:N0}", (i.Total_Quantity));
                ws.Cells[string.Format("I{0}", rowStart)].Value = String.Format("{0:N0}", (i.Total_Price));
                ws.Cells[string.Format("K{0}", rowStart)].Value = i.PaymentType.Type;
                ws.Cells[string.Format("L{0}", rowStart)].Value = i.OrderStatus.Status;
                ws.Cells[string.Format("M{0}", rowStart)].Value = i.Create_At.Value.ToString("dd/MM/yyyy");

                rowStart++;
            }


            ws.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment; filename=DanhSachDoanhThuDonHang.xlsx");
            Response.BinaryWrite(pck.GetAsByteArray());
            Response.End();

        }

        public ActionResult CheckList(string ListCategoryIDs, int OrdersStatusCheckList)
        {
            {
                if (ListCategoryIDs != null)
                {
                    string[] listID = ListCategoryIDs.Split(',');
                    foreach (string c in listID)
                    {
                        Order obj = db.Orders.Find(c);
                        obj.OrderStatusID = OrdersStatusCheckList;
                    }
                    db.SaveChanges();
                    TempData["message"] = "ChangeStatus";
                    return RedirectToAction("Index");
                }
                TempData["message"] = "CheckFail";
                return RedirectToAction("Index");
            }
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
        public ActionResult Create( Order order)
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
        public ActionResult Edit( Order order)
        {
            if (ModelState.IsValid)
            {
                if (order.Discount != null)
                {
                    order.Total_Price = order.Total_Price - ((order.Total_Price * order.Discount) / 100);
                }
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                TempData["message"] = "Edit";
                return RedirectToAction("Index");
            }

            ViewBag.PaymentTypeID = new SelectList(db.PaymentTypes, "PaymentTypeID", "Type", order.PaymentTypeID);
            ViewBag.CustomerID = new SelectList(db.Customers, "ID", "Name", order.CustomerID);
            ViewBag.OrderStatusID = new SelectList(db.OrderStatus, "ID", "Status", order.OrderStatusID);
            return View(order);
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
