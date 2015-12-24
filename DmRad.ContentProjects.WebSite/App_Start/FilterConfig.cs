using System.Web;
using System.Web.Mvc;
using DmRad.ContentProjects.WebSite.Extensions;

namespace DmRad.ContentProjects.WebSite
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new RequestResponceTiming());
        }
    }
}
