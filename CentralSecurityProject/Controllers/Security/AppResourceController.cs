using System.Linq;
using System.Web.Mvc;

namespace CentralSecurityProject.Controllers.Security
{
    /// <summary>
    /// کلاس کنترلر مربوط به منابع زیر سیستم
    /// </summary>
    public class AppResourceController : BaseController<Models.Security.AppResourceModel>
    {
        /// <summary>
        /// ایجاد کلاس سازنده پیش فرض
        /// </summary>
        public AppResourceController()
        {
            MyInitialize();
        }

        /// <summary>
        /// متد مربوط به تنظیمات کنترلر منابع زیر سیستم
        /// </summary>
        private void MyInitialize()
        {
            ViewBag.Title = "منابع زیر سیستم";
            ViewBag.Applications = new SelectList(_context.ApplicationModels.Where(x => x.IsActive), "ApplicationId", "ApplicationName");
            ViewBag.AppResources = new SelectList(_context.AppResourceModels, "AppResource", "ResourceDesc");
        }
    }
}