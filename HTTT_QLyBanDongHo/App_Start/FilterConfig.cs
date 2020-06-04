using System.Web;
using System.Web.Mvc;

namespace HTTT_QLyBanDongHo
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
