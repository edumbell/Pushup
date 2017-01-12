using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Http;
using System.Net;
using System.Threading;


namespace Pushup
{
    public class MvcApplication : System.Web.HttpApplication
    {

        private static string FirstUrl { get; set; }
  

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            if (FirstUrl == null)
            {
                FirstUrl = HttpContext.Current.Request.Url.ToString();
                KeepAlive();
            }
        }

        public void KeepAlive()
        {
            var client = new WebClient();
            var content = client.DownloadString(FirstUrl);
            var timer = new System.Threading.Timer(x =>
            {
                this.KeepAlive();
            }, null, new TimeSpan(0,10,0), Timeout.InfiniteTimeSpan);
        }
    }
}
