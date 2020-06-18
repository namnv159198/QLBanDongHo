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
        public ActionResult CheckList(string ListCategoryIDs)
        {
            {
                if (ListCategoryIDs != null)
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
