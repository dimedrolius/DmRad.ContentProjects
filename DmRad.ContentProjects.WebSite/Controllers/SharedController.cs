using System.Web.Mvc;

namespace DmRad.ContentProjects.WebSite.Controllers
{
    public class SharedController : Controller
    {
        public ActionResult NotFound()
        {
            return View();
        }
    }
}