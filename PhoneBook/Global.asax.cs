using System;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using PhoneBook.Common;

namespace PhoneBook
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            Session["UserId"] = "User123";
            Logging.Info("Sukurta nauja sesija");
        }

        protected void Session_End(object sender, EventArgs e)
        {
            Session["UserId"] = string.Empty;
            Logging.Info("Sesija uždaryta");
        }
    }
    
}
