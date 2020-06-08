using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HTTT_QLyBanDongHo.ChartModel
{
    public class MultipleModelData
    {
        public IEnumerable<ModelData> ModelData { get; set; }
        public IEnumerable<ModelData> ModelData1 { get; set; }
        public IEnumerable<ModelData2> ModelData2 { get; set; }
       
    }
}