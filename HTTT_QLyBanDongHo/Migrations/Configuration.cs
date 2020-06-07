using HTTT_QLyBanDongHo.Models;

namespace HTTT_QLyBanDongHo.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<HTTT_QLyBanDongHo.Models.QLBanDongHoDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(HTTT_QLyBanDongHo.Models.QLBanDongHoDBContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            //
            // context.CustomerTypes.AddOrUpdate(
            //     new CustomerType() { ID = 1,Type = "Khách mới" },
            //     new CustomerType() { ID = 2, Type = "Khách hàng tiềm năng" },
            //     new CustomerType() { ID = 3, Type = "VIP" },
            //     new CustomerType() { ID = 4, Type = "Khách quen" }
            // );
            // context.Categories.AddOrUpdate(
            //     new Category() { ID = 1,Name = "Đồng hồ Nam" },
            //     new Category() { ID = 2, Name = "Đồng hồ Nữ" }
            // );
            // context.OrderStatus.AddOrUpdate(
            //     new OrderStatus() { ID = 1, Status = "Đang chờ xử lý" },
            //     new OrderStatus() { ID = 2, Status = "Đang xử lý" },
            //     new OrderStatus() { ID = 3, Status = "Đang đóng gói" },
            //     new OrderStatus() { ID = 4, Status = "Đang chuyển phát" },
            //     new OrderStatus() { ID = 5, Status = "Đang thanh toán" },
            //     new OrderStatus() { ID = 6, Status = "Đã hoàn tất" },
            //     new OrderStatus() { ID = 7, Status = "Hủy" }
            // );
            // context.PaymentTypes.AddOrUpdate(
            //     new PaymentType() { PaymentTypeID = 1, Type = "Chuyển khoản thẻ ATM" },
            //     new PaymentType() { PaymentTypeID = 2, Type = "Ví điện tử" },
            //     new PaymentType() { PaymentTypeID = 3, Type = "Tiền mặt" },
            //     new PaymentType() { PaymentTypeID = 4, Type = "Mobile Banking" }
            // );
            // // // ---------------------------------- Seeding Customer  ---------------------------------- //
            // Random random = new Random();
            //
            // string[] RandomLastName =
            // {
            //     "Adams", "Adamson", "Wilson", "Burton", "Chapman", "Webb", "Allen", "Knight", "Young",
            //     "Smith", "Griffiths","White","Hall","Cox","Webb","David","Bethany","Emily"
            // };
            //
            // string[] RandomNames =
            // {
            //     "abby", "abigail", "adele", "adrian" ,"john","dadwe",
            //     "Rufus", "Bear", "Dakota", "Fido",
            //     "Vanya", "Samuel", "Koani", "Volodya",
            //     "Prince", "Yiska","Maggie", "Penny", "Saya", "Princess",
            //     "Abby", "Laila", "Sadie", "Olivia",
            //     "Starlight", "Talla","Adelaide","Aboli","Drusilla","Durra","Erin","Esperanza",
            //     "Faye","Fayola","Frida","Ganesa","Gemma","Glenda","Jade","Ladonna","Keva","Oscar","Pandora","Peach","Philomena","Phoenix","Radley",
            //     "Rose","Rosie","Rowan","Zel","Zelda","Zulema","Zoey","Xavia","Usha","Heulwen","Dragon","Tulen","Tran",
            //     "Hoang","Long","Ngan"
            // };
            // string[] RandomGender =
            // {
            //     "Nam", "Nữ"
            // };
            // {
            //
            //     string[] RandomAddress = {
            //     "California", "Florida", "New Jersey", "Indiana","Georgia","Ohio","Mississippi","Illinois","Alabama","Maine",
            //     "Missouri","Michigan","Minnesota","Oregon","Abu Dhabi","Ajman","Dubai","Fujairah","	Ras Al Khaimah","Sharjah",
            //     "Umm Al Quwain","Fukushima","Hiroshima","Hokkaido","Ishikawa","Kanagawa","Kyoto","Nagasaki","Okayama","Yamagata",
            //     "Hakodate","Amur","Khabarovsk","Magadan"
            // };
            //     int[] RandomDate =
            //     {
            //     0 , -1,-2,-3,-7,-14,-30,-60,-180,-365,-400,-287,-700,-1232,-1567,-2000,-20332
            // };
            //     DateTime start = new DateTime(1950, 1, 1);
            //     DateTime end = new DateTime(2005, 1, 1);
            //
            //
            //
            //     for (int i = 1; i <= 100; i++)
            //     {
            //         int indexName = random.Next(0, RandomNames.Length);
            //         int indexLastName = random.Next(0, RandomLastName.Length);
            //         int indexAddress = random.Next(RandomAddress.Length);
            //         int randomPhone = random.Next(1000000, 9000000);
            //         int indexGender = random.Next(RandomGender.Length);
            //         int range = (end - start).Days;
            //         int indexDate = random.Next(RandomDate.Length);
            //         var cus = new Customer()
            //         {
            //             ID = "Customer" + DateTime.Now.Millisecond + i +DateTime.Now.Hour,
            //             Name = RandomNames[indexName]+" "+RandomLastName[indexLastName],
            //             Address = RandomAddress[indexAddress],
            //             Birthday = start.AddDays(random.Next(range)),
            //             Phonenumber = String.Concat("0", random.Next(9), random.Next(5), randomPhone),
            //             Gender = RandomGender[indexGender],
            //             Email = RandomLastName[indexLastName]+RandomNames[indexName] +random.Next(10,1000)+ "@gmail.com",
            //             YearOld = DateTime.Now.Year - start.AddDays(random.Next(range)).Year,
            //             CreateAt = DateTime.Now.AddYears(random.Next(-3,0)).AddDays(random.Next(-100,10)).AddHours(random.Next(0, 24)).AddMinutes(random.Next(2000, 5000)),
            //             CustomerTypeID = random.Next(1, 5),
            //         };
            //         context.Customers.AddOrUpdate(cus);
            //     }
            // }

            // // // ---------------------------------- Seeding Order  ---------------------------------- //


        }
    }
}
