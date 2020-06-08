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

            var listAge = db.Customers.GroupBy(o => o.YearOld).Select(group => new
            {
                YearOld = group.Key,
                Total = group.Count()
            }).OrderBy(x => x.YearOld);



            string n = "";
            Double t1 = 0;
            Double t2 = 0;
            Double t3 = 0;
            Dictionary<String, Double> listAgePercent1 = new Dictionary<string, double>();

            foreach (var i in listAge)
            {
                if (i.YearOld < 25)
                {
                    t1 += i.Total;
                }

                if (i.YearOld >= 25 && i.YearOld <= 40)
                {
                    t2 += i.Total;
                }
                if (i.YearOld > 40)
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

            var DataGender = db.Customers.GroupBy(o => o.Gender).Select(group => new
            {
                Gender = group.Key,
                Total = group.Count()
            }).OrderBy(x => x.Gender);

            List<ChartModel.ChartModel.DataPoint> dataPoints2 = new List<ChartModel.ChartModel.DataPoint>();

            List<ModelData2> listTableDataGender = new List<ModelData2>();
            foreach (var i in DataGender)
            {
                dataPoints2.Add(new ChartModel.ChartModel.DataPoint(i.Gender, i.Total));
                var k = new ModelData2()
                {
                    gender = i.Gender,
                    z = i.Total
                };
                listTableDataGender.Add(k);
            }

            mymodel.ModelData = listTableDataAge;
            mymodel.ModelData2 = listTableDataGender;
            ViewBag.DataPoints2 = JsonConvert.SerializeObject(dataPoints2);
            ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);

            return View(mymodel);
        }
        public ActionResult ProductChart()
        {

            //-------------------------------------------- AfterPrice Product Statical ---------------------------------------


            var mymodel = new MultipleModelData();

            

            string n = "";
            Double t1 = 0;
            Double t2 = 0;
            Double t3 = 0;
            Double t4 = 0;
            Dictionary<String, Double> listPricePercent = new Dictionary<string, double>();

            foreach (var i in db.Products)
            {
                if (i.AfterPrice < 10000000)
                {
                    t1 += 1;
                }

                if (i.AfterPrice >= 10000000 && i.AfterPrice < 20000000)

                {
                    t2 += 1;
                }
                if (i.AfterPrice >= 20000000 && i.AfterPrice < 30000000)
                {
                    t3 += 1;
                }
                if (i.AfterPrice >= 30000000)
                {
                    t4 += 1;
                }
            }
            List<ModelData> listTableDataPriceProduct = new List<ModelData>();

            listPricePercent.Add("Thấp hơn 10tr ", t1);
            listPricePercent.Add("Từ 10tr đến 20tr", t2);
            listPricePercent.Add("Từ 20tr đến 30tr ", t3);
            listPricePercent.Add("Hơn 30tr ", t4);
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
            Double k5 = 0;
            Double k6 = 0;
            Dictionary<String, Double> listDiscountPercent = new Dictionary<string, double>();

            foreach (var i in db.Products.ToList())
            {
                if (i.Discount == 0)
                {
                    k1 =+ 1;
                }

                if (i.Discount == 10)
                {
                    k2 =+ 1;
                }
                if (i.Discount == 20)
                {
                    k3 = +1;
                }
                if (i.Discount == 30)
                {
                    k4 = +1;
                }

            }
            List<ModelData> listTableDataDiscount = new List<ModelData>();

            listDiscountPercent.Add("0%", k1);
            listDiscountPercent.Add("Khuyến mãi 10% ", k2);
            listDiscountPercent.Add("Khuyến mãi 20% ", k3);
            listDiscountPercent.Add("Khuyến mãi 30% ", k4);

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