using Pushup.BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pushup.Controllers
{
    public class HomeController : Controller
    {
        [Route("view/{slug}")]
        public ActionResult Index(string slug)
        {
            var acct = new DataPointManager().GetOrCreateAccount(slug);
            return View(acct);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        public ActionResult FakePush(string slug)
        {
            ViewData["slug"] = slug;
            return View();
        }

        [HttpPost]
        public ActionResult FakePush(string slug, Pushup.ApiModels.VariableDataPush model)
        {
            new DataPointManager().AddRawDataPoint(slug, model);
            return View();
        }
    }
}