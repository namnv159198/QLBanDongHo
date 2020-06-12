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
using HTTT_QLyBanDongHo.Models;
namespace HTTT_QLyBanDongHo.Controllers
{
    public class ClientController : Controller
    {
        private QLBanDongHoDBContext db = new QLBanDongHoDBContext();
        // GET: Client
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.Category).Include(p => p.Manufacture);
            return View(products);
        }
      
        public ActionResult Login()
        {

            return View();
        }
        public ActionResult Checkout()
        {

            return View();
        }
        public ActionResult Details(int? id)
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
                return View(product);
        }

        public ActionResult ViewCart()
        {

            return View();
        }
        public ActionResult Shop(string searchString, int? page,int? category, string currentFilter ,int? pageSize)
        {
            var listCategory = db.Categories.ToList();
            SelectList Categorylist = new SelectList(listCategory, "ID", "Name");
            ViewBag.Categorylist = Categorylist;
            var product = db.Products.Include(p => p.Category).Include(p => p.Manufacture);
            product = product.AsQueryable();
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            if (category.HasValue)
            {
                product = product.Where(s => s.Category.ID == category);
            }
            ViewBag.CurrentFilter = searchString;
            int defaSize = (pageSize ?? 12);
            if (!String.IsNullOrEmpty(searchString))
            {
                product = product.Where(s => s.Name.Contains(searchString));
            }
            int pageNumber = (page ?? 1);
            if (!product.Any())
            {
                TempData["message"] = "NotFound";
            }
            return View(product.OrderBy(P=>P.AfterPrice).ToPagedList(pageNumber, defaSize));

        }
        public ActionResult Blog()
        {

            return View();
        }
       
        
        [HttpGet]
        public ActionResult TrackOrder(string OrderId, string OrderEmail)
        {
            if (OrderId != null || OrderEmail != null )
            {
                Order order = db.Orders.Find(OrderId);
                TempData["status"] = "fail";
                if (order != null)
                {
                    TempData["status"] = "success";
                    return View(order);
                }
                return View();
            }
            return View();
        }

        public ActionResult Contact()
        {

            return View();
        }
    }
}