using MedTest.Database;
using System.Configuration;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MedTest
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start ()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var connStr = ConfigurationManager.ConnectionStrings["TestDB"];
            DBMigrations dBMigrations = new DBMigrations(connStr.ConnectionString);
            dBMigrations.Apply();
        }
    }
}
