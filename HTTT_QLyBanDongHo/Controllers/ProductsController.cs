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
    public class ProductsController : Controller
    {
        private QLBanDongHoDBContext db = new QLBanDongHoDBContext();

        public static string ActiveStatus = "Kích hoạt";
        public static string DeActiveStatus = "Chưa kích hoạt";

        // GET: Products
        public ActionResult Index(string sortOrder,int? category, int? page, string Status, string searchString, string currentFilter, DateTime? start, DateTime? end, int? pageSize,decimal? minPrice, decimal? maxPrice,string FilterPrice)
        {
            ViewBag.Active = ActiveStatus;
            ViewBag.DeActive = DeActiveStatus;
            var listOrderStatus = db.Categories.ToList();

            SelectList Categorylist = new SelectList(listOrderStatus, "ID", "Name");
            ViewBag.Categorylist = Categorylist;


            var products = db.Products.Include(p => p.Category).Include(p => p.Manufacture);
            products = products.AsQueryable();
            ViewBag.TotalEnity = products.Count();
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
                products = products.Where(s => s.Name.Contains(searchString));
            }

            if (category.HasValue)
            {
                products = products.Where(s => s.Category.ID == category);
            }
            if (minPrice.HasValue)
            {
                products = products.Where(s => s.AfterPrice >= (double?) minPrice*100000);
                ViewBag.minPrice = minPrice * 100000;
            }
            if (maxPrice.HasValue)
            {
                products = products.Where(s => s.AfterPrice <= (double?) maxPrice * 100000);
                ViewBag.maxPrice = maxPrice * 100000;
            }
            if (!String.IsNullOrEmpty(Status))
            {
                products = products.Where(s => s.Status == Status);
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
            else if (sortOrder.Equals("price-desc"))
            {
                ViewBag.DateSort = "price-asc";
                ViewBag.ColerSortIconUp = "#e0d2d2";
                ViewBag.ColerSortIconDown = "black";
            }
            else if (sortOrder.Equals("price-asc"))
            {
                ViewBag.DateSort = "price-asc";
                ViewBag.ColerSortIconUp = "#black";
                ViewBag.ColerSortIconDown = "#e0d2d2";
            }
            if (start != null)
            {
                var startDate = start.GetValueOrDefault().Date;
                startDate = startDate.Date + new TimeSpan(0, 0, 0);
                products = products.Where(p => p.CreateAt >= startDate);
            }
            if (end != null)
            {
                var endDate = end.GetValueOrDefault().Date;
                endDate = endDate.Date + new TimeSpan(23, 59, 59);
                products = products.Where(p => p.CreateAt <= endDate);
            }
           
            ViewBag.StatusListID = new List<SelectListItem>()
            {
                new SelectListItem() { Value="Kích hoạt", Text= "Kích hoạt" },
                new SelectListItem() { Value="Chưa kích hoạt", Text= "Chưa kích hoạt" },
            }; ;

            ViewBag.PageSize = new List<SelectListItem>()
            {
                new SelectListItem() { Value="5", Text= "5" },
                new SelectListItem() { Value="10", Text= "10" },
                new SelectListItem() { Value="15", Text= "15" },
                new SelectListItem() { Value="25", Text= "25" },
                new SelectListItem() { Value="50", Text= "50" },
                new SelectListItem() { Value = products.ToList().Count().ToString(), Text= "All" },
            };
            ViewBag.FilterPrice = new List<SelectListItem>()
            {
                new SelectListItem() { Value="1tr", Text= "dưới 1tr" },
                new SelectListItem() { Value="1-5tr", Text= "từ 1tr - 5tr" },
                new SelectListItem() { Value="5-10tr", Text= "từ 5tr - 10tr" },
                new SelectListItem() { Value="10-20tr", Text= "từ 10tr - 20tr" },
                new SelectListItem() { Value="20tr", Text= "Hơn 20tr" },
                new SelectListItem() { Value = products.ToList().Count().ToString(), Text= "All" },
            };
            switch (FilterPrice)
            {
                case "1tr":
                    products = products.Where(s => s.AfterPrice <=  1000000);
                    break;
                case "1-5tr":
                    products = products.Where(s => s.AfterPrice >= 1000000 && s.AfterPrice <= 5000000);
                    break;
                case "5-10tr":
                    products = products.Where(s => s.AfterPrice >= 5000000 && s.AfterPrice <= 10000000);
                    break;
                case "10-20tr":
                    products = products.Where(s => s.AfterPrice >= 10000000 && s.AfterPrice <= 20000000);
                    break;
                case "20tr":
                    products = products.Where(s => s.AfterPrice >= 20000000 );
                    break;
                default:
                    products = products.OrderByDescending(p => p.CreateAt);
                    break;
            }
            switch (sortOrder)
            {
                case "name-asc":
                    products = products.OrderBy(p => p.Name);
                    break;
                case "name-desc":
                    products = products.OrderByDescending(p => p.Name);
                    break;
                case "price-asc":
                    products = products.OrderBy(p => p.AfterPrice);
                    break;
                case "price-desc":
                    products = products.OrderByDescending(p => p.AfterPrice);
                    break;
                case "date-asc":
                    products = products.OrderBy(p => p.CreateAt);
                    break;
                case "date-desc":
                    products = products.OrderByDescending(p => p.CreateAt);
                    break;
                default:
                    products = products.OrderByDescending(p => p.CreateAt);
                    break;
            }

            int pageNumber = (page ?? 1);

            if (!products.Any())
            {
                TempData["message"] = "NotFound";
            }
            return View(products.ToPagedList(pageNumber, defaSize));
        }

        public void ExportToExcel()
        {
            var Products = db.Products.ToList();
            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Report");

            ws.Cells["A2"].Value = "Danh sách sản phẩm";

            ws.Cells["H2"].Value = "Tổng số sản phẩm ";
            ws.Cells["I2"].Value = Products.Count();

            ws.Cells["H3"].Value = "Tổng số  lượng sản phẩm ";
            ws.Cells["I3"].Value = String.Format("{0:N0}", Products.Sum(x => x.Quantity)) ;

            ws.Cells["H4"].Value = "Đã bán ";
            ws.Cells["I4"].Value = String.Format("{0:N0}", Products.Sum(x => x.Sales));

            ws.Cells["H5"].Value = "Tồn kho";
            ws.Cells["I5"].Value = String.Format("{0:N0}", Products.Sum(x => x.Remain));

            ws.Cells["A3"].Value = "Ngày xuất";
            ws.Cells["B3"].Value = string.Format("{0:dd/MM/yyyy HH:mm}", DateTimeOffset.Now);

            ws.Cells["A7"].Value = "Mã sản phẩm";
            ws.Cells["B7"].Value = "Tên sản phẩm";
            ws.Cells["C7"].Value = "Giá";
            ws.Cells["D7"].Value = "Giá bán";
            ws.Cells["E7"].Value = "Giảm giá";
            ws.Cells["F7"].Value = "Số lượng";
            ws.Cells["G7"].Value = "Đã bán";
            ws.Cells["H7"].Value = "Còn lại";
            ws.Cells["I7"].Value = "Trạng thái";
            ws.Cells["K7"].Value = "Nhà sản xuất";
            ws.Cells["L7"].Value = "Loại danh mục";
            ws.Cells["M7"].Value = "Ngày tạo";
            ws.Cells["N7"].Value = "Ghi chú";

            int rowStart = 8;
            foreach (var i in Products)
            {
                if (i.Remain <= 10 )
                {
                    ws.Row(rowStart).Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    ws.Row(rowStart).Style.Fill.BackgroundColor
                        .SetColor(ColorTranslator.FromHtml(string.Format("yellow")));
                }


                ws.Cells[string.Format("A{0}", rowStart)].Value = i.ID;
                ws.Cells[string.Format("B{0}", rowStart)].Value = i.Name;
                ws.Cells[string.Format("C{0}", rowStart)].Value = String.Format("{0:N0}", (i.Price)) + "VNĐ";
                if ( i.CreateAt == null || i.Note == null)
                {

                    ws.Cells[string.Format("M{0}", rowStart)].Value = "Chưa có";
                    ws.Cells[string.Format("N{0}", rowStart)].Value = "Chưa có";
                }
                else
                {
                    ws.Cells[string.Format("M{0}", rowStart)].Value = i.CreateAt.Value.ToString("dd/MM/yyyy");
                    ws.Cells[string.Format("N{0}", rowStart)].Value = i.Note;
                }

                ws.Cells[string.Format("D{0}", rowStart)].Value = String.Format("{0:N0}", (i.AfterPrice)) +"VNĐ";
                ws.Cells[string.Format("E{0}", rowStart)].Value = i.Discount;
                ws.Cells[string.Format("F{0}", rowStart)].Value = i.Quantity;
                ws.Cells[string.Format("G{0}", rowStart)].Value = i.Sales;
                ws.Cells[string.Format("H{0}", rowStart)].Value = i.Remain;
                ws.Cells[string.Format("I{0}", rowStart)].Value = i.Status;
                ws.Cells[string.Format("K{0}", rowStart)].Value = i.Manufacture.Name;
                ws.Cells[string.Format("L{0}", rowStart)].Value = i.Category.Name;
         
                rowStart++;
            }


            ws.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment; filename=DanhSachSanPham.xlsx");
            Response.BinaryWrite(pck.GetAsByteArray());
            Response.End();

        }

        public ActionResult CheckList(string ListCategoryIDs, string ProductsStatusCheckList)
        {
            {
                if (ListCategoryIDs != null && ProductsStatusCheckList == null)
                {
                    string[] listID = ListCategoryIDs.Split(',');
                    foreach (string c in listID)
                    {
                        Product obj = db.Products.Find(Convert.ToInt32(c));
                        db.Products.Remove(obj);
                    }

                    db.SaveChanges();
                    TempData["message"] = "Delete";
                    return RedirectToAction("Index");
                }
                if (ListCategoryIDs != null && ProductsStatusCheckList!= null)
                {
                    string[] listID = ListCategoryIDs.Split(',');
                    if (listID.Any())
                    {
                        foreach (string c in listID)
                        {
                            Product obj = db.Products.Find(Convert.ToInt32(c));
                            obj.Status = ProductsStatusCheckList;
                        }

                        db.SaveChanges();
                        TempData["message"] = "ChangeStatus";
                        return RedirectToAction("Index");
                    }
                }
                TempData["message"] = "CheckFail";
                return RedirectToAction("Index");
            }
        }
        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            ViewBag.Active = ActiveStatus;
            ViewBag.DeActive = DeActiveStatus;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {

            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "Name");
            ViewBag.ManufactureID = new SelectList(db.Manufactures, "ID", "Name");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Product product, string[] thumbnails)
        {
            if (ModelState.IsValid)
            {
                var checkExists = db.Products.AsEnumerable().Where(c => c.Name.ToString() == product.Name);
                if (!checkExists.Any())
                {
                    if (thumbnails != null && thumbnails.Length > 0)
                    {
                        product.Thumbnails = string.Join(",", thumbnails);
                    }

                    product.AfterPrice = (double) (product.AfterPrice - ((product.AfterPrice * product.Discount) / 100)); 
                    product.CreateAt = DateTime.Now;
                    db.Products.Add(product);
                    db.SaveChanges();
                    TempData["message"] = "Create";
                    return RedirectToAction("Index");
                }
                TempData["message"] = "Fail";
                return View(product);
            }
            TempData["message"] = "Fail";

            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "Name", product.CategoryID);
            ViewBag.ManufactureID = new SelectList(db.Manufactures, "ID", "Name", product.ManufactureID);
            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "Name", product.CategoryID);
            ViewBag.ManufactureID = new SelectList(db.Manufactures, "ID", "Name", product.ManufactureID);

            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Product product, string[] thumbnails)
        {
            var checkExists = db.Products.Where(c => c.Name.ToString() == product.Name && c.ID != product.ID);
            if (checkExists.Any())
            {
                TempData["message"] = "Fail";
                return View(product);
            }
            if (ModelState.IsValid)
            {
                if (thumbnails != null && thumbnails.Length > 0)
                {
                    product.Thumbnails = string.Join(",", thumbnails);
                }
                product.AfterPrice = (double) (product.AfterPrice - ((product.AfterPrice * product.Discount) / 100));
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                TempData["message"] = "Edit";
                return RedirectToAction("Index");
            }
            TempData["message"] = "Fail";
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "Name", product.CategoryID);
            ViewBag.ManufactureID = new SelectList(db.Manufactures, "ID", "Name", product.ManufactureID);

            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            db.Entry(product).State = EntityState.Modified;
            db.Products.Remove(product);
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
