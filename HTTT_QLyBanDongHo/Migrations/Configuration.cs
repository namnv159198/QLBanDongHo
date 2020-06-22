using System.Collections.Generic;
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
            //
            // context.Manufactures.AddOrUpdate(
            //     new Manufacture() { ID = 1, Name = "Apple" ,Logo = "image/upload/v1591544955/Nh%C3%A0%20s%E1%BA%A3n%20xu%E1%BA%A5t/kcKnMMA5i_xoxqxm.jpg" ,Create_At = DateTime.Now},
            //     new Manufacture() { ID = 2, Name = "Rolex", Logo = "image/upload/v1591545064/Nh%C3%A0%20s%E1%BA%A3n%20xu%E1%BA%A5t/9e543bf8bd95d55d5989e1561cc85210_srnajh.jpg", Create_At = DateTime.Now },
            //     new Manufacture() { ID = 3, Name = "Patek Philippe", Logo = "image/upload/v1591545190/Nh%C3%A0%20s%E1%BA%A3n%20xu%E1%BA%A5t/Patek_20Philippe_20_logo_e2hhsj.jpg", Create_At = DateTime.Now },
            //     new Manufacture() { ID = 4, Name = "Omega", Logo = "image/upload/v1591545237/Nh%C3%A0%20s%E1%BA%A3n%20xu%E1%BA%A5t/omega-vector_dgpcin.jpg", Create_At = DateTime.Now },
            //     new Manufacture() { ID = 5, Name = "Tissot", Logo = "image/upload/v1591545360/Nh%C3%A0%20s%E1%BA%A3n%20xu%E1%BA%A5t/b02346ae70c528b816475eb088a81906_ouxdbv.jpg", Create_At = DateTime.Now },
            //     new Manufacture() { ID = 6, Name = "Calvin Klein", Logo = "image/upload/v1591545408/Nh%C3%A0%20s%E1%BA%A3n%20xu%E1%BA%A5t/da16e1667e7e502a185d149ba8e21fc2.w1000.h1000_qziuqx.jpg", Create_At = DateTime.Now },
            //     new Manufacture() { ID = 7, Name = "Movado", Logo = "image/upload/v1591545533/Nh%C3%A0%20s%E1%BA%A3n%20xu%E1%BA%A5t/e4ba64c67262fd1cf3bd629832e373b2_vksk4p.jpg", Create_At = DateTime.Now },
            //     new Manufacture() { ID = 8, Name = "SEIKO", Logo = "image/upload/v1591545557/Nh%C3%A0%20s%E1%BA%A3n%20xu%E1%BA%A5t/og_poczsn.png", Create_At = DateTime.Now },
            //     new Manufacture() { ID = 9, Name = "Citizen", Logo = "image/upload/v1591545570/Nh%C3%A0%20s%E1%BA%A3n%20xu%E1%BA%A5t/citizen-eco-drive-logo_u7r4xx.jpg", Create_At = DateTime.Now },
            //     new Manufacture() { ID = 10, Name = "Orient", Logo = "image/upload/v1591545585/Nh%C3%A0%20s%E1%BA%A3n%20xu%E1%BA%A5t/0844a0abe5b2ea979f2bb37284dafd33_attn66.jpg", Create_At = DateTime.Now }
            // );

            // // // ---------------------------------- Seeding Product  ---------------------------------- //
            
            //     Random random = new Random();
            //     int[] RandomDate =
            //     {
            //         0, -1, -2, -3, -7, -14, -30, -60, -180, -365, -400, -287, -700, -750
            //     };
            //
            //
            // string[] RandomNameWatch =
            //     {
            //         "Apple", "Rolex", "Casio", "Movado", "Orient", "Orient", "SEIKO", "Tissot", "Omega",
            //         "SKAGEN", "Fossil","Kors","Ogival","Daniel Wellington","Anne Klein"
            //     };
            //     
            //     string[] RandomStatusProduct =
            //     {
            //         "Kích hoạt","Chưa kích hoạt"
            //     };
            //     char randomChar = (char)random.Next('A', 'Z');
            //     string[] RandomImage =
            //     {
            //         "image/upload/v1591626262/91_DW00100224-1-399x399_rjbjx4",
            //         "image/upload/v1591626299/26_A3414.41B3Q-399x399_smlzkl",
            //         "image/upload/v1591626262/91_DW00100224-1-399x399_rjbjx4",
            //         "image/upload/v1591626037/112_SFQ810P1-399x399_kx7sht",
            //         "image/upload/v1591625841/58_NH7524-55A-399x399_pi2kya",
            //         "image/upload/v1591625812/65_NJ0110-18L-399x399_kawdwq",
            //         "image/upload/v1591625780/BM6982-11H-399x399_h8cib1",
            //         "image/upload/v1591624880/129_BH3000-09A-399x399_qfcl5h",
            //         "image/upload/v1591625907/119_AU1080-20A-399x399_nn6jtk",
            //         "image/upload/v1591625884/174_ES4313-1-399x399_gwezhg",
            //         "image/upload/v1591625789/221_BM7300-50A-399x399_ryc18o",
            //         "image/upload/v1591625800/164_NH8363-14X-399x399_arsxk1",
            //         "image/upload/v1591626362/79_FS5452-399x399_ej8cyo",
            //         "image/upload/v1591629120/K3M22T26-399x399_sjm5kf",
            //         "image/upload/v1591629143/54_K5S341CZ-399x399_vearmq",
            //         "image/upload/v1591629169/16_K4E2N111-399x399_eocvim",
            //         "image/upload/v1591626342/22_ES3268-399x399_hqcamd",
            //         "image/upload/v1591629143/54_K5S341CZ-399x399_vearmq",
            //         "image/upload/v1591626176/K3M22T26-399x399_cntoue",
            //         "image/upload/v1591626284/73_A3508.1143QZ-2-399x399_wislbx",
            //         "image/upload/v1591626238/72_LTP-1335D-4AVDF-399x399_sypc8c",
            //         "image/upload/v1591626136/LTP-1095Q-7A-399x399_aulngl",
            //         "image/upload/v1591626125/58_K6L2M116-399x399_uxpkgk"
            //     };
            //     
            //   
            //     
            //     for (int i = 1; i <= 500; i++)
            //     {
            //         int indexName = random.Next(0, RandomNameWatch.Length);
            //         int indexThumbnails = random.Next(RandomImage.Length);
            //         int indexDate = random.Next(RandomDate.Length);
            //         int indexStatusProduct = random.Next(RandomStatusProduct.Length);
            //         var p = new Product()
            //         {
            //             Name = RandomNameWatch[indexName] + " " + (char) random.Next('A', 'Z') + " "
            //                    + (char) random.Next('A', 'Z') + random.Next(1000, 9000) + "-" + random.Next(0, 1000) + " " +
            //                    (char) random.Next('A', 'Z'),
            //             isBestSeller = random.Next(0, 2),
            //             isSpecial = random.Next(0, 2),
            //             isNew = random.Next(0, 2),
            //             CreateAt = DateTime.Now.AddDays(RandomDate[indexDate]),
            //             Status = RandomStatusProduct[indexStatusProduct],
            //             ManufactureID = random.Next(1,11),
            //             CategoryID = random.Next(1,3),
            //             Price = random.Next(5,250)*100000,
            //             Discount = (random.Next(0,4))*10,
            //             Thumbnails = RandomImage[indexThumbnails],
            //             Quantity = random.Next(10,50),
            //         
            //             
            //         };
            //         p.Description = "Mẫu " + p.Name +
            //                         "với một vẻ ngoài dành cho những ai yêu thích vẻ hoài cổ với kiểu dáng mặt số vuông truyền thống, chi tiết chữ số được viết theo dạng chữ la mã kết hợp cùng mẫu dây đeo da đen có vân cổ điển lịch lãm.";
            //         p.AfterPrice =  (double) (p.Price - ((p.Price * p.Discount)/100));
            //         p.Sales = random.Next(0, p.Quantity);
            //
            //         p.Remain = p.Quantity - p.Sales;
            //         if (p.Remain == 0)
            //         {
            //             p.Note = "Hết hàng";
            //         }
            //         if (p.Remain < 10)
            //         {
            //             p.Note = "Sắp hết hàng";
            //         }
            //         if (p.Remain > 10 && p.Remain <= 30)
            //         {
            //             p.Note = "Đang bán tốt";
            //         }
            //         if (p.Remain > 30 && p.Remain <= 45)
            //         {
            //             p.Note = "Hàng đang tồn đọng !";
            //         }
            //         if (p.Remain > 45 )
            //         {
            //             p.Note = "Hàng đang ế !";
            //         }
            //
            //     context.Products.AddOrUpdate(p);
            //     }
            //
            //

            // // // ---------------------------------- Seeding Customer  ---------------------------------- //
            // Random random = new Random();
            //
            // string[] RandomLastName =
            // {
            //     "Nguyễn", "Trần", "Lê", "Phạm", "Hoàng", "Huỳnh", "Phan", "Vũ", "Võ",
            //     "Đặng", "Bùi","Đỗ","Hồ","Ngô","Dương","Lý","Trương","Lưu","Đoàn"
            // };
            // string[] RandomMiddleName =
            // {
            //     "Văn", "Thị", "Quang", "Bảo", "Đức", "Công", "Tuấn", "Lê", "Bích", "Thu", "Thúy",
            //     "Thùy", "Hoàng", "Quốc", "Ngọc", "Thái", "Thành", "Bích", "Anh", "Ánh", "Đình",
            //     "Mỹ", "Xuân", "Việt"
            // };
            // string[] RandomNames =
            // {
            //     "Anh", "An", "Linh", "Diệp" ,"Diệu","Đào",
            //     "Cao", "Chinh", "Thanh", "Dung","Dũng",
            //     "Bình", "Ba", "Bích", "Vân",
            //     "Hương", "Thủy","Liên", "Quỳnh", "Quốc", "Bảo",
            //     "Nam", "Ninh", "Dinh", "Hà",
            //     "Hào", "Hiên","Hân","Ngọc","Phong","Phú","Thương","Toàn",
            //     "Chiến","Thắng","Thu","Thiên","Lệ","Mạnh","Hằng","Liễu","Tú","Tuấn","Thuận","Đông","Thiết","Trang","Tran",
            //     "Tùng","Nga","Quang","Quân","Chung","Trung","Trà","Tiên","Trinh","Nhi","Yến","Thảo","Giao",
            //     "Hoàng","Long","Ngân", "Nga", "Chính", "Ánh", "Bách", "Thúy", "Kiều", "Liễu", "Vinh", "Hiếu",
            //     "Hiệu", "Hằng", "Công", "Hùng", "Bạch", "Hạnh","Hường", "Việt", "Doanh"
            // };
            // string[] RandomGender =
            // {
            //     "Nam", "Nữ"
            // };
            // {
            //
            //     string[] RandomAddress = {
            //         "Hà Nội", "Hồ Chí Minh", "Hải Phòng", "Cần Thơ","Biên Hòa","Đà Nẵng","Nha Trang","Buôn Ma thuột","Huế"," Vinh",
            //         "Vũng Tàu","Quy Nhơn","Long xuyên","Thái Nguyên","Nam Định","Rạch Giá","Thủ Dầu Một","Hạ Long","Phan Thiết","Khánh Hòa","Sầm Sơn","Sóc Trăng","Sơn La","Sông Công","Tam Điệp","Tam Kỳ","Tân An","Tây Ninh","Thanh Hoá","Trà Vinh","Tuy Hòa","Tuyên Quang","Uông Bí","Vị Thanh","Việt Trì"
            //     };
            //     int[] RandomDate =
            //     {
            //         0, -1, -2, -3, -7, -14, -30, -60, -180, -365, -400, -287, -700 , -800 , -30*2, -30*3,-30*4,-30*6,-600,-888
            //     };
            //     
            //     DateTime start = new DateTime(1950, 1, 1);
            //     DateTime end = new DateTime(2005, 1, 1);
            //
            //
            //
            //     for (int i = 1; i <= 1000; i++)
            //     {
            //         int indexName = random.Next(0, RandomNames.Length);
            //         int indexMidName = random.Next(0, RandomMiddleName.Length);
            //         int indexLastName = random.Next(0, RandomLastName.Length);
            //         int indexAddress = random.Next(RandomAddress.Length);
            //         int randomPhone = random.Next(1000000, 9000000);
            //         int indexGender = random.Next(RandomGender.Length);
            //         int range = (end - start).Days;
            //         int indexDate = random.Next(RandomDate.Length);
            //         var cus = new Customer()
            //         {
            //             ID = "KH" + DateTime.Now.Millisecond + i +DateTime.Now.Hour,
            //             Name = RandomLastName[indexLastName] + " " + RandomMiddleName[indexMidName] +" "+ RandomNames[indexName] ,
            //             Address = RandomAddress[indexAddress],
            //             Birthday = start.AddDays(random.Next(range)),
            //             Phonenumber = String.Concat("0", random.Next(9), random.Next(5), randomPhone),
            //             Gender = RandomGender[indexGender],
            //             Email = RandomLastName[indexLastName].Substring(0,1) + RandomMiddleName[indexMidName].Substring(0, 1)+ RandomNames[indexName].Substring(0, 1) + random.Next(10,1000)+ "@gmail.com",
            //             CreateAt = DateTime.Now.AddDays(RandomDate[indexDate]),
            //         };
            //         cus.Age = DateTime.Now.Year - cus.Birthday.Value.Year;
            //         if (cus.Age >= 25 && cus.Age <= 45)
            //         {
            //             cus.CustomerTypeID = 2;
            //         }
            //         else
            //         {
            //             cus.CustomerTypeID = random.Next(1, 5);
            //         }
            //         context.Customers.AddOrUpdate(cus);
            //     }
            // }

            // // // ---------------------------------- Seeding Order  ---------------------------------- //
             Random random = new Random();
             
            
             int[] RandomDateNow =
             {
                 0 , -1,-2,-3,-7,-14,-30,-60,-180
             };
            var ListCustomer = context.Customers.ToList();
            var listProduct = context.Products.ToList();
            var listOrderStatus = context.OrderStatus.ToList();
            var listPaymentType = context.PaymentTypes.ToList();
            for (int i = 1; i <= 100; i++)
            {
                // int indexDate = random.Next(RandomDate.Length);
                int c = random.Next(0, ListCustomer.Count);
                int os = random.Next(5, listOrderStatus.Count);
                int pt = random.Next(0, listPaymentType.Count);
                var order = new Order()
                {
                    ID = "Order" + i+ DateTime.Now.Millisecond+DateTime.Now.Year,
                    Create_At = DateTime.Now.AddDays(random.Next(-6*30,0)).AddHours(random.Next(-10,-1)).AddMinutes(random.Next(-300,-100)),
                    OrderStatusID = listOrderStatus[os].ID,
                    CustomerID = ListCustomer[c].ID,
                    PaymentTypeID = listPaymentType[pt].PaymentTypeID,
                    Total_Price = 0,
                    Total_Quantity = 0,
                    Discount = 0
            };
            
                for (int j = 1; j <= random.Next(1,4); j++)
                {
                    int p = random.Next(0, listProduct.Count);
                    // List<int> unique = new List<int>();
                    // int p = 0;
                    // void randomP()
                    // {
                    //     p = random.Next(listProduct.Count);
                    //     foreach (var p1 in unique)
                    //     {
                    //         if (p1 == p)
                    //         {
                    //             randomP();
                    //         }
                    //     }
                    // }
                    // randomP();
                    // unique.Add(p);
                    var orderDetails = new OrderDetail()
                    {
                        OrderID = order.ID,
                        ProductID = listProduct[p].ID,
                        Quantity = 1,
                        UnitPrice = listProduct[p].AfterPrice
                    };
                    
                    order.AddOrderDetails(orderDetails);
                    context.OrderDetails.Add(orderDetails);
                 
                }
            
               
                context.Orders.AddOrUpdate(order);
            }
        }
    }
}
