using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HTTT_QLyBanDongHo.ChartModel;
using HTTT_QLyBanDongHo.Models;
using Newtonsoft.Json;

namespace HTTT_QLyBanDongHo.Controllers
{
    public class ChartController : Controller
    {
        private QLBanDongHoDBContext db = new QLBanDongHoDBContext();
        // GET: Chart
        public ActionResult CustomerChart()
        {

            var mymodel = new MultipleModelData();

            var listAge = db.Customers.GroupBy(o => o.Age).Select(group => new
            {
                Age = group.Key,
                Total = group.Count()
            }).OrderBy(x => x.Age);



            string n = "";
            Double t1 = 0;
            Double t2 = 0;
            Double t3 = 0;
            Dictionary<String, Double> listAgePercent1 = new Dictionary<string, double>();

            foreach (var i in listAge)
            {
                if (i.Age < 25)
                {
                    t1 += i.Total;
                }

                if (i.Age >= 25 && i.Age <= 40)
                {
                    t2 += i.Total;
                }
                if (i.Age > 40)
                {
                    t3 += i.Total;
                }
            }
            List<ModelData> listTableDataAge = new List<ModelData>();

            listAgePercent1.Add("Tuổi từ 22 trở xuống ", t1);
            listAgePercent1.Add("Tuổi từ 22 đến 40", t2);
            listAgePercent1.Add("Tuổi từ 40 trở lên ", t3);
            List<ChartModel.ChartModel.DataPoint> dataPoints = new List<ChartModel.ChartModel.DataPoint>();

            foreach (var i in listAgePercent1)
            {
                dataPoints.Add(new ChartModel.ChartModel.DataPoint(i.Key, (double)i.Value));
                var k = new ModelData()
                {
                    label = i.Key,
                    y = i.Value
                };
                listTableDataAge.Add(k);
            }

            Double k1 = 0;
            Double k2 = 0;
            Double k3 = 0;
            //-------------------------------------------- Sale Customer Statical ---------------------------------------
            Dictionary<String, Double> listSaleCustomerYears = new Dictionary<string, double>();
            foreach (var i in db.Customers)
            {
                if (i.CreateAt.Value.Year == 2018)
                {
                    k1 = k1 + 1;
                }

                if (i.CreateAt.Value.Year == 2019)
                {
                    k2 = k2 + 1;
                }
                if (i.CreateAt.Value.Year == 2020)
                {
                    k3 = k3 + 1;
                }
            };

            listSaleCustomerYears.Add("Năm 2019 ", k2);
            listSaleCustomerYears.Add("Năm 2020", k3);
            List<ChartModel.ChartModel.DataPoint> dataPoints1 = new List<ChartModel.ChartModel.DataPoint>();
            List<ModelData> listTableSaleCustomerYears = new List<ModelData>();

            foreach (var i in listSaleCustomerYears)
            {
                dataPoints1.Add(new ChartModel.ChartModel.DataPoint(i.Key, (double)i.Value));
                var k = new ModelData()
                {
                    label = i.Key,
                    y = i.Value
                };
                listTableSaleCustomerYears.Add(k);
            }

            mymodel.ModelData1 = listTableSaleCustomerYears;
            mymodel.ModelData = listTableDataAge;
    


            ViewBag.DataPoints1 = JsonConvert.SerializeObject(dataPoints1);
       
            ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);

            return View(mymodel);
        }
        public ActionResult ProductChart()
        {

            //-------------------------------------------- AfterPrice Product Statical ---------------------------------------


            var mymodel = new MultipleModelData();

            

        
            Double t1 = 0;
            Double t2 = 0;
            Double t3 = 0;
            Double t4 = 0;
            Double t5 = 0;
            Dictionary<String, Double> listPricePercent = new Dictionary<string, double>();

            foreach (var i in db.Products)
            {
                if (i.AfterPrice < 1000000)
                {
                    t1 += 1;
                }

                if (i.AfterPrice >= 1000000 && i.AfterPrice < 5000000)

                {
                    t2 += 1;
                }
                if (i.AfterPrice >= 5000000 && i.AfterPrice < 10000000)
                {
                    t3 += 1;
                }
                if (i.AfterPrice >= 10000000 && i.AfterPrice < 20000000)
                {
                    t4 += 1;
                }
                if (i.AfterPrice >= 20000000)
                {
                    t5 += 1;
                }
            }
            List<ModelData> listTableDataPriceProduct = new List<ModelData>();

            listPricePercent.Add("Thấp hơn 1tr ", t1);
            listPricePercent.Add("Từ 1tr đến 5tr", t2);
            listPricePercent.Add("Từ 5tr đến 10tr ", t3);
            listPricePercent.Add("Từ 10tr đến 20tr ", t4);
            listPricePercent.Add("Hơn 20tr  ", t5);
            List<ChartModel.ChartModel.DataPoint> dataPoints = new List<ChartModel.ChartModel.DataPoint>();

            foreach (var i in listPricePercent)
            {
                dataPoints.Add(new ChartModel.ChartModel.DataPoint(i.Key, (double)i.Value));
                var k = new ModelData()
                {
                    label = i.Key,
                    y = i.Value
                };
                listTableDataPriceProduct.Add(k);
            }
            mymodel.ModelData = listTableDataPriceProduct;
            ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);

            //-------------------------------------------- Discount Product Statical ---------------------------------------
           

            Double k1 = 0;
            Double k2 = 0;
            Double k3 = 0;
            Double k4 = 0;
         
            Dictionary<String, Double> listDiscountPercent = new Dictionary<string, double>();

            foreach (var i in db.Products.ToList())
            {
                if (i.CreateAt.Value.Year  == 2018 )
                {
                    k1 = k1 + 1;
                }

                if (i.CreateAt.Value.Year == 2019)
                {
                    k2 = k2 + 1;
                }
                if (i.CreateAt.Value.Year == 2020)
                {
                    k3 = k3 + 1;
                }
              

            }
            List<ModelData> listTableDataDiscount = new List<ModelData>();

            listDiscountPercent.Add("Năm 2018", k1);
            listDiscountPercent.Add("Năm 2019 ", k2);
            listDiscountPercent.Add("Năm 2020", k3);

            List<ChartModel.ChartModel.DataPoint> dataPoints1 = new List<ChartModel.ChartModel.DataPoint>();


            foreach (var i in listDiscountPercent)
            {
                dataPoints1.Add(new ChartModel.ChartModel.DataPoint(i.Key, (double)i.Value));
                var k = new ModelData()
                {
                    label = i.Key,
                    y = i.Value
                };
                listTableDataDiscount.Add(k);
            }





            
            ViewBag.DataPoints1 = JsonConvert.SerializeObject(dataPoints1);
            mymodel.ModelData1 = listTableDataDiscount;
           

            return View(mymodel);
        }

    }
   
}