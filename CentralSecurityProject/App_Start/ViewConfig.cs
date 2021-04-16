using System.Collections.Generic;

namespace CentralSecurityProject.App_Start
{
    /// <summary>
    /// View کلاس تنظیمات مربوط به ساختار بندی فایل ها
    /// </summary>
    public class ViewConfig : System.Web.Mvc.RazorViewEngine
    {
        public ViewConfig() : base()
        {
            List<string> viewRoutes = new List<string>();

            // Method 1 : روش اول اضافه کردن یک مسیر جدید به ساختار فایل ها
            // viewRoutes.Add("~/Views/Security/{1}/{0}.cshtml");

            // Method 2 : روش دوم اضافه کردن یک مسیر جدید به ساختار فایل ها
            /*
            string[] schemas = new string[] { "Security" , "etc" };
            foreach (string schema in schemas)
            {
                viewRoutes.Add($"~/Views/{schema}/{{1}}/{{0}}.cshtml");
            }
            */

            // Method 3 : روش سوم اضافه کردن یک مسیر جدید به ساختار فایل ها
            ApplicationConfig.GetSchemas().ForEach(f => viewRoutes.Add($"~/Views/{f}/{{1}}/{{0}}.cshtml"));
            

            ViewLocationFormats = PartialViewLocationFormats = viewRoutes.ToArray();
        }
    }
}