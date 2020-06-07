using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace HTTT_QLyBanDongHo.Controllers
{
    public class ChartController : Controller
    {
        // GET: Chart
        public ActionResult Index()
        {

            List<ChartModel.ChartModel.DataPoint> dataPoints = new List<ChartModel.ChartModel.DataPoint>();

            dataPoints.Add(new ChartModel.ChartModel.DataPoint("Economics", 1));
            dataPoints.Add(new ChartModel.ChartModel.DataPoint("Physics", 2));
            dataPoints.Add(new ChartModel.ChartModel.DataPoint("Literature", 4));
            dataPoints.Add(new ChartModel.ChartModel.DataPoint("Chemistry", 4));
            dataPoints.Add(new ChartModel.ChartModel.DataPoint("Literature", 9));
            dataPoints.Add(new ChartModel.ChartModel.DataPoint("Physiology or Medicine", 11));
            dataPoints.Add(new ChartModel.ChartModel.DataPoint("Peace", 13));

            ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);
            return View();
        }
    }
}