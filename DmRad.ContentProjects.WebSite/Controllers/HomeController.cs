using System.Web;
using System.Web.Mvc;
using DmRad.ContentProjects.Common.Models;
using DmRad.ContentProjects.Common.Services;

namespace DmRad.ContentProjects.WebSite.Controllers
{
    public class HomeController : Controller
    {
        [OutputCache(Duration = int.MaxValue)]
        public ActionResult Index(int page = 1)
        {
            var model = Endpoint.Instance.TestDataService.GetHeadersByPage(page, errors => ModelState.AddModelError(string.Empty, errors));
            return View(model);
        }

        [OutputCache(Duration = int.MaxValue)]
        public ActionResult GetRecords(int page)
        {
            var model = Endpoint.Instance.TestDataService.GetHeadersByPage(page, errors => ModelState.AddModelError(string.Empty, errors));
            return PartialView("_RecordsListPartial", model);
        }

        [OutputCache(Duration = int.MaxValue)]
        public ActionResult Record(int id)
        {
            var model = Endpoint.Instance.TestDataService.GetRecordById(id, errors => ModelState.AddModelError(string.Empty, errors));
            return View(model);
        }

        public ActionResult NewRecord()
        {
            var model = new RecordModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult NewRecord(RecordModel model)
        {
            if (ModelState.IsValid)
            {
                if (Endpoint.Instance.TestDataService.AddNewRecord(model, errors => ModelState.AddModelError(string.Empty, errors)))
                {
                    model.AddedSuccess = true;
                    ClearCachePolicies();
                }
            }
            
            return View(model);
        }

        [HttpPost]
        public ActionResult DeleteRecord(int recordId)
        {
            var outErrors = string.Empty;
            var res = Endpoint.Instance.TestDataService.DeleteRecord(recordId, errors => outErrors = errors);
            //В случае успешного удаления записи очищаем кеш по выдаче записей по страницам
            if (res)
                ClearCachePolicies();

            return Json(new { result = res, errors = outErrors });
        }

        [HttpPost]
        public ActionResult ClearCache()
        {
            HttpResponse.RemoveOutputCacheItem(Url.Action("Index", "Home"));
            HttpResponse.RemoveOutputCacheItem(Url.Action("GetRecords", "Home"));
            HttpResponse.RemoveOutputCacheItem(Url.Action("Record", "Home"));

            return Json(new { result = true });
        }

        /// <summary>
        /// Очистить кеш страниц
        /// </summary>
        private void ClearCachePolicies()
        {
            HttpResponse.RemoveOutputCacheItem(Url.Action("Index", "Home"));
            HttpResponse.RemoveOutputCacheItem(Url.Action("GetRecords", "Home"));
            HttpResponse.RemoveOutputCacheItem(Url.Action("Record", "Home"));
        }
    }
}