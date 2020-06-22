using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HTTT_QLyBanDongHo.ChartModel;
using HTTT_QLyBanDongHo.Models;
using Newtonsoft.Json;
using OfficeOpenXml;

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

            //-------------------------------------------- Quantity Product Statical ---------------------------------------


            int k1 = 0;
            int k2 = 0;
            int k3 = 0;


            Dictionary<String, int> listQuantityPercent = new Dictionary<string, int>();

            int totalQuantity = db.Products.Sum(x => x.Quantity);
            int totalRemain = db.Products.Sum(x => (int)x.Remain);

            List<ModelData> listTableDataQuantity = new List<ModelData>();

            listQuantityPercent.Add("Số lượng nhập ", totalQuantity);
            listQuantityPercent.Add("Số lượng tồn kho", totalRemain);
            listQuantityPercent.Add("Số lượng bán ra", totalQuantity - totalRemain);

            List<ChartModel.ChartModel.DataPoint> dataPoints1 = new List<ChartModel.ChartModel.DataPoint>();


            foreach (var i in listQuantityPercent)
            {
                dataPoints1.Add(new ChartModel.ChartModel.DataPoint(i.Key, (double)i.Value));
                var k = new ModelData()
                {
                    label = i.Key,
                    y = i.Value
                };
                listTableDataQuantity.Add(k);
            }



            ViewBag.DataPoints1 = JsonConvert.SerializeObject(dataPoints1);
            mymodel.ModelData1 = listTableDataQuantity;


            return View(mymodel);
        }

        public ActionResult RevenueChart(int? YearChart)
        {
            var listyear = db.Orders.Select(x => x.Create_At.Value.Year).Distinct();

            SelectList listYearSelect = new SelectList(listyear, "Year");
            ViewBag.listYear = listYearSelect;

            var mymodel = new MultipleModelData();
            double t1 = 0;
            double t2 = 0;
            double t3 = 0;
            double t4 = 0;

            if (!YearChart.HasValue)
            {
                YearChart = 2020;
            }


            var RevenueYearNow = db.Orders.Where(x => x.Create_At.Value.Year == YearChart).GroupBy(o => o.Create_At.Value.Month).Select(group => new
            {
                createAt = group.Key,
                Revenue = group.Sum(o => o.Total_Price)
            }).OrderBy(x => x.createAt);
            List<ChartModel.ChartModel.DataPoint3> dataPoints2 = new List<ChartModel.ChartModel.DataPoint3>();
            List<ModelData3> listTableDataRevenueNow = new List<ModelData3>();

            foreach (var l in RevenueYearNow)
            {
                dataPoints2.Add(new ChartModel.ChartModel.DataPoint3(l.createAt, (double)l.Revenue));
                var k1 = new ModelData3()
                {
                    date = l.createAt,
                    y = l.Revenue
                };
                listTableDataRevenueNow.Add(k1);
            }

            //-------------------------------------------- Revenue Year Statical ---------------------------------------

            Dictionary<String, double> listRevenuePercent = new Dictionary<string, double>();

            foreach (var i in db.Orders.ToList())
            {
                if (i.Create_At.Value.Year == 2017)
                {
                    t1 += (double)i.Total_Price;
                }
                if (i.Create_At.Value.Year == 2018)
                {
                    t2 += (double)i.Total_Price;
                }

                if (i.Create_At.Value.Year == 2019)
                {
                    t3 += (double)i.Total_Price;
                }

                if (i.Create_At.Value.Year == 2020)
                {
                    t4 += (double)i.Total_Price;
                }
            }

            List<ModelData> listTableDataRevenue = new List<ModelData>();

            listRevenuePercent.Add("Năm 2017", t1);
            listRevenuePercent.Add("Năm 2018", t2);
            listRevenuePercent.Add("Năm 2019 ", t3);
            listRevenuePercent.Add("Năm 2020", t4);

            List<ChartModel.ChartModel.DataPoint> dataPoints1 = new List<ChartModel.ChartModel.DataPoint>();

            foreach (var l in listRevenuePercent)
            {
                dataPoints1.Add(new ChartModel.ChartModel.DataPoint(l.Key, (double)l.Value));
                var k = new ModelData()
                {
                    label = l.Key,
                    y = l.Value
                };
                listTableDataRevenue.Add(k);
            }

            List<ChartModel.ChartModel.DataPoint3> dataPoints7 = new List<ChartModel.ChartModel.DataPoint3>();
            List<ChartModel.ChartModel.DataPoint3> dataPoints8 = new List<ChartModel.ChartModel.DataPoint3>();
            List<ChartModel.ChartModel.DataPoint3> dataPoints9 = new List<ChartModel.ChartModel.DataPoint3>();
            List<ChartModel.ChartModel.DataPoint3> dataPoints20 = new List<ChartModel.ChartModel.DataPoint3>();



            var R2017 = db.Orders.Where(x => x.Create_At.Value.Year == 2017).GroupBy(o => o.Create_At.Value.Month).Select(group => new
            {
                createAt = group.Key,
                Revenue = group.Sum(o => o.Total_Price)
            }).OrderBy(x => x.createAt);
            var R2018 = db.Orders.Where(x => x.Create_At.Value.Year == 2018).GroupBy(o => o.Create_At.Value.Month).Select(group => new
            {
                createAt = group.Key,
                Revenue = group.Sum(o => o.Total_Price)
            }).OrderBy(x => x.createAt);

            var R2019 = db.Orders.Where(x => x.Create_At.Value.Year == 2019).GroupBy(o => o.Create_At.Value.Month).Select(group => new
            {
                createAt = group.Key,
                Revenue = group.Sum(o => o.Total_Price)
            }).OrderBy(x => x.createAt);

            var R2020 = db.Orders.Where(x => x.Create_At.Value.Year == 2020).GroupBy(o => o.Create_At.Value.Month).Select(group => new
            {
                createAt = group.Key,
                Revenue = group.Sum(o => o.Total_Price)
            }).OrderBy(x => x.createAt);


            foreach (var l in R2017)
            {
                dataPoints7.Add(new ChartModel.ChartModel.DataPoint3(l.createAt, (double)l.Revenue));
            }
            foreach (var l in R2018)
            {
                dataPoints8.Add(new ChartModel.ChartModel.DataPoint3(l.createAt, (double)l.Revenue));
            }
            foreach (var l in R2019)
            {
                dataPoints9.Add(new ChartModel.ChartModel.DataPoint3(l.createAt, (double)l.Revenue));
            }
            foreach (var l in R2020)
            {
                dataPoints20.Add(new ChartModel.ChartModel.DataPoint3(l.createAt, (double)l.Revenue));
            }
            ViewBag.DataPoints7 = JsonConvert.SerializeObject(dataPoints7);
            ViewBag.DataPoints8 = JsonConvert.SerializeObject(dataPoints8);
            ViewBag.DataPoints9 = JsonConvert.SerializeObject(dataPoints9);
            ViewBag.DataPoints20 = JsonConvert.SerializeObject(dataPoints20);


            ViewBag.DataPoints1 = JsonConvert.SerializeObject(dataPoints1);
            ViewBag.DataPoints2 = JsonConvert.SerializeObject(dataPoints2);
            mymodel.ModelData1 = listTableDataRevenue;
            mymodel.ModelData3 = listTableDataRevenueNow;
            return View(mymodel);
        }

        
        public void ExportToExcel(string ex)
        {
            var expE = new List<ExportExcelModel>();
            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Report");
            int rowStart = 8;
            switch (ex)
            {
                case "2017":
                    var R2017 = db.Orders.Where(x => x.Create_At.Value.Year == 2017).GroupBy(o => o.Create_At.Value.Month).Select(group => new
                    {
                        createAt = group.Key,
                        Revenue = group.Sum(o => o.Total_Price),
                        totalQuanity = group.Sum(o=>o.Total_Quantity),
                        totalRemain = group.Sum(o => o.OrderDetails.Sum(x=>x.Product.Remain)),
                    }).OrderBy(x => x.createAt);

                  
                    foreach (var i in R2017)
                    {
                        var e = new ExportExcelModel();
                        e.Month = i.createAt;
                        e.TotalQuantity = (int) i.totalQuanity;
                        e.Revenue = (double) i.Revenue;
                      expE.Add(e);
                    }

                    ExportVoid(expE, Convert.ToInt32(ex));
                    break;
                case "2018":
                    var R2018 = db.Orders.Where(x => x.Create_At.Value.Year == 2018).GroupBy(o => o.Create_At.Value.Month).Select(group => new
                    {
                        createAt = group.Key,
                        Revenue = group.Sum(o => o.Total_Price),
                        totalQuanity = group.Sum(o => o.Total_Quantity),
                        totalRemain = group.Sum(o => o.OrderDetails.Sum(x => x.Product.Remain)),
                    }).OrderBy(x => x.createAt);

                 
                    foreach (var i in R2018)
                    {
                        var e = new ExportExcelModel();
                        e.Month = i.createAt;
                        e.TotalQuantity = (int)i.totalQuanity;
                        e.Revenue = (double)i.Revenue;
                        expE.Add(e);
                    }

                    ExportVoid(expE, Convert.ToInt32(ex));

                    break;
                case "2019":
                    var R2019 = db.Orders.Where(x => x.Create_At.Value.Year == 2019).GroupBy(o => o.Create_At.Value.Month).Select(group => new
                    {
                        createAt = group.Key,
                        Revenue = group.Sum(o => o.Total_Price),
                        totalQuanity = group.Sum(o => o.Total_Quantity),
                        totalRemain = group.Sum(o => o.OrderDetails.Sum(x => x.Product.Remain)),
                    }).OrderBy(x => x.createAt);


                    foreach (var i in R2019)
                    {
                        var e = new ExportExcelModel();
                        e.Month = i.createAt;
                        e.TotalQuantity = (int)i.totalQuanity;
                        e.Revenue = (double)i.Revenue;
                        expE.Add(e);
                    }

                    ExportVoid(expE, Convert.ToInt32(ex));

                    break;
                case "2020":
                    var R2020 = db.Orders.Where(x => x.Create_At.Value.Year == 2020).GroupBy(o => o.Create_At.Value.Month).Select(group => new
                    {
                        createAt = group.Key,
                        Revenue = group.Sum(o => o.Total_Price),
                        totalQuanity = group.Sum(o => o.Total_Quantity),
                        totalRemain = group.Sum(o => o.OrderDetails.Sum(x => x.Product.Remain)),
                    }).OrderBy(x => x.createAt);


                    foreach (var i in R2020)
                    {
                        var e = new ExportExcelModel();
                        e.Month = i.createAt;
                        e.TotalQuantity = (int)i.totalQuanity;
                        e.Revenue = (double)i.Revenue;
                        expE.Add(e);
                    }

                    ExportVoid(expE, Convert.ToInt32(ex));

                    break;

            }
            

            
        }

        public void ExportVoid(List<ExportExcelModel> e,int year)
        {
            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Report");
            int rowStart = 8;

           
            ws.Cells["A2"].Value = "Danh sách doanh thu năm " + year;

            ws.Cells["H3"].Value = "Tổng số  doanh thu ";
            ws.Cells["I3"].Value = String.Format("{0:N0}", (e.Sum(x=>x.Revenue))) + "VNĐ"; 

            ws.Cells["A3"].Value = "Ngày xuất";
            ws.Cells["B3"].Value = string.Format("{0:dd/MM/yyyy HH:mm}", DateTimeOffset.Now);

            ws.Cells["A7"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            ws.Cells["B7"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            ws.Cells["C7"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            ws.Cells["A7"].Value = "Tháng";
            ws.Cells["B7"].Value = "Số lượng bán";

            ws.Cells["C7"].Value = "Doanh thu";

            foreach (var i in e)
            {
                if (i.Revenue >= 4000000000)
                {
                    ws.Row(rowStart).Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    ws.Row(rowStart).Style.Fill.BackgroundColor
                        .SetColor(ColorTranslator.FromHtml(string.Format("yellow")));
                }
                ws.Cells[string.Format("A{0}", rowStart)].Value = i.Month;
                ws.Cells[string.Format("B{0}", rowStart)].Value = i.TotalQuantity;
                ws.Cells[string.Format("C{0}", rowStart)].Value = String.Format("{0:N0}", (i.Revenue)) + "VNĐ";

                rowStart++;
            }


            ws.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment; filename=DoanhThuNam"+year+".xlsx");
            Response.BinaryWrite(pck.GetAsByteArray());
            Response.End();
        }

        
    }
}

