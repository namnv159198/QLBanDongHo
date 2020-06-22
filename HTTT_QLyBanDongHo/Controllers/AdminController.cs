using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HTTT_QLyBanDongHo.Models;

namespace HTTT_QLyBanDongHo.Controllers
{
    public class AdminController : Controller
    {
        private QLBanDongHoDBContext db = new QLBanDongHoDBContext();
        // GET: Admin
        public ActionResult Index()
        {
            ViewBag.TotalClient = db.Customers.Count();
            ViewBag.TotalRevenuDay =  db.Orders.AsEnumerable().Where(x => x.Create_At.Value.ToString("MM/dd/yyyy") ==  DateTime.Now.ToString("MM/dd/yyyy") ).Sum(x => x.Total_Price);
            ViewBag.TotalProduct = db.Orders.AsEnumerable().Where(x => x.Create_At.Value.ToString("MM/dd/yyyy") == DateTime.Now.ToString("MM/dd/yyyy")).Sum(x => x.Total_Quantity); 
            ViewBag.TotalOrderDay = db.Orders.AsEnumerable().Count(x => x.Create_At.Value.ToString("MM/dd/yyyy") == DateTime.Now.ToString("MM/dd/yyyy"));
            var orders = db.Orders.AsEnumerable().Where(x => x.Create_At.Value.ToString("MM/dd/yyyy") == DateTime.Now.ToString("MM/dd/yyyy")).ToList();


            return View(orders);
        }
    }
}