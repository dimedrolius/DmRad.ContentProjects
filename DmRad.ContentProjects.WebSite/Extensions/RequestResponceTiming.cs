using System.Diagnostics;
using System.Web;
using System.Web.Mvc;

namespace DmRad.ContentProjects.WebSite.Extensions
{
    public class RequestResponceTiming : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var stopwatch = new Stopwatch();
            HttpContext.Current.Items["Stopwatch"] = stopwatch;
            stopwatch.Start();

            base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var stopwatch = (Stopwatch)HttpContext.Current.Items["Stopwatch"];
            stopwatch.Stop();
            HttpContext.Current.Session["ElapsedTime"] = stopwatch.Elapsed.TotalMilliseconds;

            base.OnActionExecuted(filterContext);
        }
    }
}