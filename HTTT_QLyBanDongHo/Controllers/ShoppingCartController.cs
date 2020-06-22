using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using HTTT_QLyBanDongHo.Models;


namespace HTTT_QLyBanDongHo.Controllers
{
    public class ShoppingCartController : Controller
    {
        private QLBanDongHoDBContext db = new QLBanDongHoDBContext();
        public const String ShoppingCartSession = "SHOPPING_CART";
        // GET: ShoppingCart
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddItem(int? id)
        {
            var existingProduct = db.Products.FirstOrDefault(m => m.ID == id);

            if (existingProduct == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (Session[ShoppingCartSession] == null)
            {
                List<Cart> listCart = new List<Cart>
                {
                    new Cart(existingProduct,1)
                };
                Session[ShoppingCartSession] = listCart;
            }
            else
            {
                List<Cart> listCart = (List<Cart>)Session[ShoppingCartSession];
                int checkExistingProduct = CheckExistingProduct(id);
                if (checkExistingProduct == -1)
                {
                    listCart.Add(new Cart(existingProduct, 1));
                }
                else
                {
                    listCart[checkExistingProduct].Quantity++;
                }
                Session[ShoppingCartSession] = listCart;
            }
            return Json("success");
        }
        public ActionResult AddItemQuantity(int? id, int quantity)
        {
            var existingProduct = db.Products.FirstOrDefault(m => m.ID == id);

            if (existingProduct == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (Session[ShoppingCartSession] == null)
            {
                List<Cart> listCart = new List<Cart>
                {
                    new Cart(existingProduct,quantity)
                };
                Session[ShoppingCartSession] = listCart;
            }
            else
            {
                List<Cart> listCart = (List<Cart>)Session[ShoppingCartSession];
                int checkExistingProduct = CheckExistingProduct(id);
                if (checkExistingProduct == -1)
                {
                    listCart.Add(new Cart(existingProduct, quantity));
                }
                else
                {
                    listCart[checkExistingProduct].Quantity = +quantity;
                }
                Session[ShoppingCartSession] = listCart;
            }
            return Json("success");
        }
        public ActionResult UpdateCart(int productID, int quantity)
        {
            var existingProduct = db.Products.FirstOrDefault(m => m.ID == productID);

            if (existingProduct == null)
            {
                return new HttpNotFoundResult();
            }
            List<Cart> listCart = (List<Cart>)Session[ShoppingCartSession];
            for (int i = 0; i < listCart.Count; i++)
            {
                if (listCart[i].Product.ID == productID)
                {
                    listCart[i].Quantity = quantity;
                }
            }
            Session[ShoppingCartSession] = listCart;
            TempData["message"] = "UpdateSuccess";
            return Redirect("Index");
        }
        public ActionResult DeleteItem(int? productID)
        {
            var existingProduct = db.Products.FirstOrDefault(m => m.ID == productID);

            if (existingProduct == null)
            {
                return new HttpNotFoundResult();
            }
            List<Cart> listCart = (List<Cart>)Session[ShoppingCartSession];
            for (int i = 0; i < listCart.Count; i++)
            {
                if (listCart[i].Product.ID == productID)
                {
                    listCart.RemoveAt(i);
                }
            }
            Session[ShoppingCartSession] = listCart;
            TempData["message"] = "Delele";
            return Redirect("Index");
        }

        [ValidateAntiForgeryToken]
        public ActionResult Checkout(CheckoutModel o)
        {
            var random = new Random();

            List<Cart> listCart = (List<Cart>)Session[ShoppingCartSession];


            if (ModelState.IsValid)
            {
                Customer cus = new Customer()
                {
                    ID = "KH" + DateTime.Now.Millisecond,
                    Address = o.Address,
                    Phonenumber = o.PhoneNumber,
                    Name = o.Name,
                    CustomerTypeID = 1,
                    Email = o.Email,
                    Gender = o.Gender,
                    CreateAt = DateTime.Now
                };
                db.Customers.Add(cus);
                db.SaveChanges();
                Order order = new Order()
                {
                    ID = "Order" + DateTime.Now.Millisecond+DateTime.Now.Year,
                    OrderStatusID = 1,
                    Create_At = DateTime.Now,
                    CustomerID = cus.ID,
                    Discount = 0,
                    Total_Price = listCart.Sum(x => x.Product.AfterPrice * x.Quantity),
                    PaymentTypeID = o.TypePayment,
                    Total_Quantity = listCart.Sum(x => x.Quantity)
                };

                db.Orders.Add(order);
                db.SaveChanges();

                foreach (Cart cart in listCart)
                {
                    OrderDetail orderDetails = new OrderDetail()
                    {
                        OrderID = order.ID,
                        ProductID = cart.Product.ID,
                        Quantity = cart.Quantity,
                    };

                    var p = db.Products.Find(cart.Product.ID);
                    p.Sales += cart.Quantity;
                    p.Remain = p.Quantity - p.Sales;
                    db.Entry(p).State = EntityState.Modified;
                    db.OrderDetails.Add(orderDetails);
                    db.SaveChanges();
                }

                var senderEmail = new MailAddress("namkun159198@gmail.com", "BoxShop");
                var receiverEmail = new MailAddress(o.Email, "Receiver");
                var password = "0963404604";
                var sub = "[ BoxShop ] : Đơn hàng mới  #" +order.ID;
                var body = "";
                foreach (Cart cart in listCart)
                {
                    body += " Sản phẩm : " + cart.Product.Name + " - " + "Số lượng : " + cart.Quantity + " - " + "Giá : " + String.Format("{0:N0}", (cart.Product.Price)) +" VNĐ "+ " - " +"Thành tiền : " + String.Format("{0:N0}", (cart.Product.Price * cart.Quantity)) + " VNĐ ";
                    body += "\n";
                }

                body += "Tổng đơn hàng : " + String.Format("{0:N0}", (listCart.Sum(x => x.Product.Price * x.Quantity))) + " VNĐ " +"\n";
                body += "Giao nhận : Giao hàng miễn phí" +"\n";
                body += "Phương thức thanh toán : Trả tiền mặt khi nhận hàng";
                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(senderEmail.Address, password)
                };
                using (var mess = new MailMessage(senderEmail, receiverEmail)
                {
                    Subject = sub,
                    Body = body
                })
                {
                    smtp.Send(mess);
                }


                Session.Remove(ShoppingCartSession);
                TempData["message"] = "success";
                return Redirect("../Client/Index");
            }
            return View("Index");

        }
        private int CheckExistingProduct(int? id)
        {
            List<Cart> listCart = (List<Cart>)Session[ShoppingCartSession];
            for (int i = 0; i < listCart.Count; i++)
            {
                if (listCart[i].Product.ID == id) return i;
            }
            return -1;
        }
    }
}