using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Data.Entity;


namespace CentralSecurityProject
{
    /// <summary>
    /// کلاس آغازین برنامه های تحت وب
    /// </summary>
    public class MvcApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// تظیمات مربوط به شروع برنامه
        /// </summary>
        protected void Application_Start()
        {
            // شروع اجرای برنامه تحت وب : قدم اول
            ViewEngines.Engines.Add(new App_Start.ViewConfig()); // View اضافه کردن تنظیمات مربوط به ساختار فایل های

            // تنظیمات مربوط به بانک اطلاعاتی مورد استفاده
            //Database.SetInitializer(new DropCreateDatabaseIfModelChanges<Models.ApplicationDbContext>()); // ساخت بانک اطلاعاتی به همراه جداول در زمان تغییرات : استراتژی 1
            //Database.SetInitializer(new DropCreateDatabaseAlways<Models.DataBaseContext>()); // ساخت بانک اطلاعاتی به همراه جداول بصورت همیشگی : استراتژی 2
            Database.SetInitializer(new Models.DataBaseContextInitializer()); // ساخت بانک اطلاعاتی به همراه جداول و دادن مقدار اولیه به جداول در زمان تغییرات : استراتژی 3
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<Models.ApplicationDbContext, Migrations.Configuration>()); // ساخت بانک اطلاعاتی به کمک میگریشن : استراتژی 4

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes); // تنظیمات مربوط به مسیریاب پروژه
            BundleConfig.RegisterBundles(BundleTable.Bundles); // تنظیمات مربوط به دسته بندی کردن
        }

        /// <summary>
        /// تنظیمات مربوط به قبل از درخواست اجرای برنامه
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies["Language"];
            if (cookie != null && cookie.Value != null)
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(cookie.Value);
                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(cookie.Value);
            }
            else
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("fa");
                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("fa");
            }
        }
    }
}
