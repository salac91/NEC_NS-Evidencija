using System.Web;
using System.Web.Mvc;

namespace NetsNS_Evidencija
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}